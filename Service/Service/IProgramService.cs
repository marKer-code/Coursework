namespace Service
{
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IProgramService
    {
        [OperationContract(IsOneWay = true)]
        void CheckUser(string login, string password);
        [OperationContract(IsOneWay = true)]
        void CheckLogin(string login);
    }
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void UserExist(string exists);
        [OperationContract(IsOneWay = true)]
        void LoginExist(string exists);
    }

    class UserMessage
    {
        public string Message { get; set; }
        public ICallback Callback { get; set; }
    }

    class Message
    {
        public UserMessage UserMessage { get; set; }
    }
}
