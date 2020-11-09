namespace Service
{
    using System.ServiceModel;

    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveRequest(string senderLogin);
        [OperationContract(IsOneWay = true)]
        void NewContact(string contactLogin);
        [OperationContract(IsOneWay = true)]
        void RejectRequest_(string senderLogin);
        [OperationContract(IsOneWay = true)]
        void DeleteContact(string toDeleteLogin);
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
