namespace Service
{
    using DAL;
    using DAL.Entities;
    using DAL.Interfaces;
    using DAL.Repositories;
    using System;
    using System.ServiceModel;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork repositories;
        private Message message;

        public ProgramService()
        {
            message = new Message();
            repositories = new UnitOfWork(new DAL.ProgramDatabaseModel());
        }

        public void CheckUser(string login, string password)
        {
            message.UserMessage = new UserMessage()
            {
                Message = repositories.UserRepository.CheckUser(login, new Utils().ComputeSha256Hash(password)).ToString(),
                Callback = OperationContext.Current.GetCallbackChannel<ICallback>()
            };
            message.UserMessage.Callback.UserExist(message.UserMessage.Message);
        }

        public void CheckLogin(string login)
        {
            message.UserMessage = new UserMessage()
            {
                Message = repositories.UserRepository.CheckLogin(login).ToString(),
                Callback = OperationContext.Current.GetCallbackChannel<ICallback>()
            };
            message.UserMessage.Callback.LoginExist(message.UserMessage.Message);
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
            repositories.UserInfoRepository.Insert(new UserInfo()
            {
                Nickname = nickname,
                LastOnline = lastOnline,
                Online = online,
                Photo = img,
                UserId = repositories.UserRepository.GetUserId(login)
            });
            repositories.Save();
        }
    }
}
