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

        public string[] LoadUserInfo(string login)
        {
            int id = repositories.UserRepository.GetUserId(login);
            IEnumerable<UserInfo> userInfo = repositories
                .UserInfoRepository.Get(ui => ui.UserId == id);
            string[] infoes = new string[4];
            infoes[0] = userInfo.First().Nickname;
            infoes[1] = userInfo.First().Online.ToString();
            infoes[2] = userInfo.First().LastOnline.ToString();
            infoes[3] = Encoding.Default.GetString(userInfo.First().Photo);
            return infoes;
        }
    }
}
