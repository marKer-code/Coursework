namespace Service
{
    using DAL;
    using DAL.Entities;
    using DAL.Interfaces;
    using DAL.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class ProgramService : IProgramService
    {
        private readonly IUnitOfWork repositories;
        readonly Dictionary<string, UserMessage> sub =
           new Dictionary<string, UserMessage>();

        public ProgramService()
            => repositories =
               new UnitOfWork(new ProgramDatabaseModel());

        public bool CheckUser(string login, string password)
        {
            string pass = new Utils().ComputeSha256Hash(password);
            return repositories.UserRepository
                             .Get(u => u.Login == login &&
                              u.HashPassword == pass)
                             .FirstOrDefault() != null;
        }

        public bool CheckLogin(string login)
            => repositories.UserRepository
                           .Get(u => u.Login == login)
                           .FirstOrDefault() != null;

        public List<byte[]> LoadUserInfo(string login)
        {
            int id = repositories.UserRepository
                                 .Get(u => u.Login == login)
                                 .First()
                                 .Id;

            List<UserInfo> userInfo = repositories
                           .UserInfoRepository
                           .Get(ui => ui.UserId == id)
                           .ToList();

            return new List<byte[]>
            {
                Encoding.Default.GetBytes(userInfo.First().Nickname),
                Encoding.Default.GetBytes(userInfo.First().LastOnline.ToString()),
                userInfo.First().Photo,
                Encoding.Default.GetBytes(userInfo.First().Online.ToString())
            };
        }

        public List<int> GetAllContact(string login)
        {
            int idUser = repositories.UserRepository
                         .Get(u => u.Login == login)
                         .First()
                         .Id;

            List<int> users = new List<int>();

            foreach (var c in repositories
                              .CoupleRepository
                              .Get(c => c.UserId1 == idUser ||
                               c.UserId2 == idUser)
                              .ToList())
            {
                if (c.UserId1 != idUser)
                    users.Add(c.UserId1);
                if (c.UserId2 != idUser)
                    users.Add(c.UserId2);
            }

            return users;
        }

        public List<Request> GetAllRequests(string login, bool isSend)
        {
            int idResiver = repositories.UserRepository
                            .Get(u => u.Login == login)
                            .First()
                            .Id;

            return isSend == true ?
                repositories.RequestRepository.Get(r => r.SenderId == idResiver).ToList() :
                repositories.RequestRepository.Get(r => r.ReceiverId == idResiver).ToList();
        }

        public string GetLoginUserById(int id)
            => repositories.UserRepository.GetById(id).Login;
    }
}
