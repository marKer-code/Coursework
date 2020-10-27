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

        void NotifyUser(Message message)
        {
            message.UserMessage.Callback.UserExist(message.UserMessage.Message);
        }

        public void CheckUser(string login, string password)
        {
            bool exists = repositories.UserRepository.CheckUser(login, password);
            message.UserMessage = new UserMessage()
            {
                Message = exists.ToString(),
                Callback = OperationContext.Current.GetCallbackChannel<ICallback>()
            };
            NotifyUser(message);
        }
    }
}
