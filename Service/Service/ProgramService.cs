namespace Service
{
    using DAL;
    using DAL.Entities;
    using DAL.Interfaces;
    using DAL.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork repositories;

        public ProgramService()
            => repositories = new UnitOfWork(new DAL.ProgramDatabaseModel());

        public bool CheckUser(string login, string password)
        {
            string pass = new Utils().ComputeSha256Hash(password);
            try
            {
                User user = repositories.UserRepository
                            .Get(u => u.Login == login &&
                                 u.HashPassword == pass)
                            .First();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckLogin(string login)
        {
            try
            {
                User user = repositories.UserRepository
                                .Get(u => u.Login == login)
                                .First();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddUser(string login, string nickname,
            string password, byte[] img, bool online, DateTime lastOnline)
        {
            repositories.UserRepository.Insert(new User()
            {
                Login = login,
                HashPassword = new Utils().ComputeSha256Hash(password)
            });
            repositories.Save();

            User user = repositories.UserRepository
                .Get(u => u.Login == login).First();
            int id = user.Id;

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

        public List<byte[]> LoadUserInfo(string login)
        {
            User user = repositories.UserRepository
                .Get(u => u.Login == login).First();
            int id = user.Id;

            IEnumerable<UserInfo> userInfo = repositories
                .UserInfoRepository.Get(ui => ui.UserId == id);
            List<byte[]> infoes = new List<byte[]>();
            infoes.Add(Encoding.Default.GetBytes(userInfo.First().Nickname));
            infoes.Add(Encoding.Default.GetBytes(userInfo.First().LastOnline.ToString()));
            infoes.Add(userInfo.First().Photo);
            infoes.Add(Encoding.Default.GetBytes(userInfo.First().Online.ToString()));
            return infoes;
        }

        public void SaveUserPhoto(string login, byte[] img)
        {
            User newUser = repositories.UserRepository
               .Get(u => u.Login == login)
               .First();

            UserInfo info = repositories.UserInfoRepository
                .Get(i => i.UserId == newUser.Id)
                .First();

            info.Photo = img;
            repositories.Save();
        }

        public void SaveUserInfo(string lastLogin,
            string login, string nickname,
            string password, byte[] img)
        {
            User newUser = repositories.UserRepository
                .Get(u => u.Login == lastLogin)
                .First();

            UserInfo info = repositories.UserInfoRepository
               .Get(i => i.UserId == newUser.Id)
               .First();

            newUser.Login = login;
            newUser.HashPassword = new Utils().ComputeSha256Hash(password);
            info.Nickname = nickname;
            info.Photo = img;

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

            repositories.RequestRepository.Insert(
                new Request
                {
                    SenderId = idSender,
                    ReceiverId = idReceiver,
                    SendTime = DateTime.Now
                });

            repositories.Save();
        }

        public List<int> GetAllContact(string login)
        {
            List<int> users = new List<int>();
            int idUser = repositories.UserRepository
                .Get(u => u.Login == login)
                .First()
                .Id;

            List<Couple> couples
                = repositories.CoupleRepository
                .Get(c => c.UserId1 == idUser ||
                c.UserId2 == idUser)
                .ToList();

            foreach (var c in couples)
            {
                if (c.UserId1 != idUser)
                    users.Add(c.UserId1);
                if (c.UserId2 != idUser)
                    users.Add(c.UserId2);
            }

            return users;
        }

        public IEnumerable<Request> GetAllRequests(string login, bool isSend)
        {
            if (!isSend)
            {
                int idResiver = repositories.UserRepository
                    .Get(u => u.Login == login)
                    .First()
                    .Id;
                return repositories.RequestRepository.Get(r => r.ReceiverId == idResiver).ToList();
            }
            else
            {
                int idResiver = repositories.UserRepository
                    .Get(u => u.Login == login)
                    .First()
                    .Id;
                return repositories.RequestRepository.Get(r => r.SenderId == idResiver).ToList();
            }

        }

        public string GetLoginUserById(int id)
        {
            return repositories.UserRepository.GetById(id).Login;
        }

        public void DeletedRequest(string sender, string receiver)
        {
            throw new NotImplementedException();
        }

        public void AddCouple(string user1, string ser2)
        {
            throw new NotImplementedException();
        }

        public void UpdateOnline(string login, bool loginIn)
        {
            UserInfo userInfo = repositories.UserInfoRepository
                .Get(u => u.User.Login == login)
                .First();

            int idUser = userInfo.UserId;

            userInfo.Online = loginIn;
            if (!loginIn)
                userInfo.LastOnline = DateTime.Now;
        }
    }
}
