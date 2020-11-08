namespace Service
{
    using DAL;
    using DAL.Entities;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
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
        }

        public void RemoveContact(string login, string otherLogin)
        {
            int idUser = repositories.UserRepository
                .Get(u => u.Login == login)
                .First()
                .Id;
            int idOtherUser = repositories.UserRepository
                .Get(u => u.Login == otherLogin)
                .First()
                .Id;
            Couple couple = repositories.CoupleRepository
                            .Get(c => c.UserId1 == idUser &&
                                c.UserId2 == idOtherUser ||
                                c.UserId1 == idOtherUser &&
                                c.UserId2 == idUser).First();

            repositories.CoupleRepository.Delete(couple);

            repositories.Save();
        }
    }
}
