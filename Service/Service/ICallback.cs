namespace Service
{
    using System.ServiceModel;

    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveRequest(string senderLogin);
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
