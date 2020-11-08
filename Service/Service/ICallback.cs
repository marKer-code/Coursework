namespace Service
{
    using System.ServiceModel;

    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void Message_(string message);
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
