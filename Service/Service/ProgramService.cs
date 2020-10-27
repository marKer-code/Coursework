namespace Service
{
    using DAL.Interfaces;
    using DAL.Repositories;
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
                Message = repositories.UserRepository.CheckUser(login, password).ToString(),
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
    }
}
