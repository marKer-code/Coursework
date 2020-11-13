namespace Service
{
    using DAL;
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

    public partial class ProgramService
    {
        readonly static Dictionary<string, UserMessage> sub =
           new Dictionary<string, UserMessage>();

        public void AddUser(string login, string nickname,
            string password, byte[] img, bool online, DateTime lastOnline)
        {
            repositories.UserRepository.Insert(new User()
            {
                Login = login,
                HashPassword = new Utils().ComputeSha256Hash(password)
            });
            repositories.Save();

            int id = repositories.UserRepository
                        .Get(u => u.Login == login)
                        .First()
                        .Id;

            repositories.UserInfoRepository.Insert(new UserInfo()
            {
                Nickname = nickname,
                LastOnline = lastOnline,
                Online = online,
                Photo = img,
                UserId = id
            });
            repositories.Save();
        }

        public void SaveUserInfo(string lastLogin, string login,
            string nickname, string password, byte[] img)
        {
            User newUser = repositories.UserRepository
                           .Get(u => u.Login == lastLogin)
                           .First();

            newUser.Login = login;
            newUser.HashPassword = new Utils()
                .ComputeSha256Hash(password);

            UserInfo info = repositories.UserInfoRepository
                            .Get(i => i.UserId == newUser.Id)
                            .First();

            info.Nickname = nickname;
            info.Photo = img;

            repositories.Save();
        }

        public void UpdateOnline(string login, string @do)
        {
            UserInfo userInfo = repositories.UserInfoRepository
                                .Get(u => u.User.Login == login)
                                .First();

            int idUser = userInfo.UserId;

            switch (@do)
            {
                case "NewCallback":
                    userInfo.Online = true;
                    UserMessage user = new UserMessage()
                    {
                        Message = "",
                        Callback = OperationContext.Current.GetCallbackChannel<ICallback>()
                    };
                    sub.Add(login, user);
                    break;
                case "ChangeCallback":
                    try
                    {
                        userInfo.Online = true;
                        if (sub.Count != 0 && sub.ContainsKey(sub.First(s => s.Key == login).Key))
                            sub.Remove(sub.First(s => s.Key == login).Key);
                    }
                    catch
                    {

                    }
                    UserMessage user_ = new UserMessage()
                    {
                        Message = "",
                        Callback = OperationContext.Current.GetCallbackChannel<ICallback>()
                    };
                    sub.Add(login, user_);
                    break;
                default:
                    userInfo.Online = false;
                    userInfo.LastOnline = DateTime.Now;
                    sub.Remove(login);
                    break;
            }
            repositories.Save();
        }

        public void AddRequest(string sender, string receiver)
        {
            int idSender = repositories.UserRepository
                           .Get(u => u.Login == sender)
                           .First()
                           .Id,

                idReceiver = repositories.UserRepository
                             .Get(u => u.Login == receiver)
                             .First()
                             .Id;

            repositories.RequestRepository.Insert
            (
                new Request
                {
                    SenderId = idSender,
                    ReceiverId = idReceiver,
                    SendTime = DateTime.Now
                }
            );

            repositories.Save();

            foreach (var item in sub)
                if (item.Key == receiver)
                {
                    item.Value.Message = sender;
                    item.Value.Callback.ReceiveRequest(item.Value.Message);
                    return;
                }
        }

        public void AcceptRequest(string sender, string receiver)
        {
            int idSender = repositories.UserRepository
                           .Get(u => u.Login == sender)
                           .First()
                           .Id,

                idReceiver = repositories.UserRepository
                             .Get(u => u.Login == receiver)
                             .First()
                             .Id;

            repositories.RequestRepository.Delete
            (
                repositories.RequestRepository
                            .Get(r => r.SenderId == idSender &&
                             r.ReceiverId == idReceiver)
                            .First()
            );

            repositories.CoupleRepository.Insert(
                new Couple
                {
                    UserId1 = idSender,
                    UserId2 = idReceiver
                });

            repositories.Save();

            foreach (var item in sub)
                if (item.Key == sender)
                {
                    item.Value.Message = receiver;
                    item.Value.Callback.NewContact(item.Value.Message);
                    return;
                }
        }

        public void RejectRequest(string sender, string receiver)
        {
            int idSender = repositories.UserRepository
                           .Get(u => u.Login == sender)
                           .First()
                           .Id,

                idReceiver = repositories.UserRepository
                             .Get(u => u.Login == receiver)
                             .First()
                             .Id;

            repositories.RequestRepository.Delete
            (
                repositories.RequestRepository
                            .Get(r => r.SenderId == idSender &&
                             r.ReceiverId == idReceiver)
                            .First()
            );

            repositories.Save();

            foreach (var item in sub)
                if (item.Key == sender)
                {
                    item.Value.Message = receiver;
                    item.Value.Callback.RejectRequest_(item.Value.Message);
                    return;
                }
        }

        public void RemoveContact(string sender, string receiver)
        {
            int senderId = repositories.UserRepository
                .Get(u => u.Login == sender)
                .First()
                .Id,

                receiverId = repositories.UserRepository
                .Get(u => u.Login == receiver)
                .First()
                .Id;

            Couple couple = repositories.CoupleRepository
                            .Get(c => c.UserId1 == senderId &&
                                c.UserId2 == receiverId ||
                                c.UserId1 == receiverId &&
                                c.UserId2 == senderId).First();

            repositories.CoupleRepository.Delete(couple);

            List<DAL.Entities.Message> messages = repositories.MessageRepository
               .Get(m => m.SenderId == senderId &&
               m.ReceiverId == receiverId ||
               m.SenderId == receiverId &&
               m.ReceiverId == senderId)
               .ToList();

            foreach (var item in messages)
                repositories.MessageRepository.Delete(item);

            repositories.Save();

            foreach (var item in sub)
                if (item.Key == receiver)
                {
                    item.Value.Message = sender;
                    item.Value.Callback.DeleteContact(item.Value.Message);
                    return;
                }
        }

        public void AddChat(string senderLogin, string receiverLogin)
        {
            int senderId = repositories.UserRepository
                 .Get(u => u.Login == senderLogin)
                 .First()
                 .Id;
            int receiverId = repositories.UserRepository
                .Get(u => u.Login == receiverLogin)
                .First()
                .Id;

            repositories.MessageRepository.Insert
                (new DAL.Entities.Message()
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    SendTime = DateTime.Now,
                    ReadTime = new DateTime(2020, 1, 1)
                });
            repositories.Save();

            foreach (var item in sub)
                if (item.Key == receiverLogin)
                {
                    item.Value.Message = senderLogin;
                    item.Value.Callback.NewChat_(item.Value.Message);
                    return;
                }
        }

        public void RemoveChat(string sender, string receiver)
        {
            int senderId = repositories.UserRepository
               .Get(u => u.Login == sender)
               .First()
               .Id,

               receiverId = repositories.UserRepository
                .Get(u => u.Login == receiver)
                .First()
                .Id;

            List<DAL.Entities.Message> messages = repositories.MessageRepository
                .Get(m => m.SenderId == senderId &&
                m.ReceiverId == receiverId ||
                m.SenderId == receiverId &&
                m.ReceiverId == senderId)
                .ToList();

            foreach (var item in messages)
                repositories.MessageRepository.Delete(item);

            repositories.Save();

            foreach (var item in sub)
                if (item.Key == receiver)
                {
                    item.Value.Message = sender;
                    item.Value.Callback.DeleteChat(item.Value.Message);
                    return;
                }
        }

        public void SendMessage(string sender, string receiver, string message)
        {
            int senderId = repositories.UserRepository
              .Get(u => u.Login == sender)
              .First()
              .Id,

            receiverId = repositories.UserRepository
                .Get(u => u.Login == receiver)
                .First()
                .Id;

            DAL.Entities.Message m = new DAL.Entities.Message()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Text = message,
                SendTime = DateTime.Now,
            };

            repositories.MessageRepository.Insert(m);
            repositories.Save();

            foreach (var item in sub)
                if (item.Key == receiver)
                {
                    item.Value.Message =
                        sender + " " +
                        m.SenderId + " " +
                        m.ReceiverId + " " + m.Text;
                    item.Value.Callback.ReciveMessage(item.Value.Message);
                    return;
                }
        }
    }
}
