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


        readonly static Dictionary<string, UserMessage> sub =
           new Dictionary<string, UserMessage>();
        public void UpdateOnline(string login, bool loginIn)
        {
            UserInfo userInfo = repositories.UserInfoRepository
                                .Get(u => u.User.Login == login)
                                .First();

            int idUser = userInfo.UserId;

            userInfo.Online = loginIn;

            switch (loginIn.ToString())
            {
                case "True":
                    {
                        UserMessage user = new UserMessage()
                        {
                            Message = "",
                            Callback = OperationContext.Current.GetCallbackChannel<ICallback>()
                        };
                        sub.Add(login, user);
                        break;
                    }
                case "False":
                    {
                        userInfo.LastOnline = DateTime.Now;

                        //foreach (var item in sub.Keys)
                        //{
                        //    UserMessage um = new UserMessage()
                        //    {
                        //        Message = "pidor exit",
                        //        Callback = sub[item].Callback
                        //    };
                        //    um.Callback.Message_(um.Message);
                        //}

                        sub.Remove(login);
                        break;
                    }
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
                .Id;
            int receiverId = repositories.UserRepository
                .Get(u => u.Login == receiver)
                .First()
                .Id;
            Couple couple = repositories.CoupleRepository
                            .Get(c => c.UserId1 == senderId &&
                                c.UserId2 == receiverId ||
                                c.UserId1 == receiverId &&
                                c.UserId2 == senderId).First();

            repositories.CoupleRepository.Delete(couple);

            repositories.Save();

            foreach (var item in sub)
                if (item.Key == receiver)
                {
                    item.Value.Message = sender;
                    item.Value.Callback.DeleteContact(item.Value.Message);
                    return;
                }
        }
    }
}
