namespace Service
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IProgramService
    {
        [OperationContract]
        bool CheckUser(string login, string password);
        [OperationContract]
        bool CheckLogin(string login);
        [OperationContract(IsOneWay = true)]
        void AddUser(string login, string nickname,
            string password, byte[] img,
            bool online, DateTime lastOnline);
        [OperationContract(IsOneWay = true)]
        void SaveUserInfo(string lastLogin, string login, string nickname,
            string password, byte[] img);
        [OperationContract]
        List<byte[]> LoadUserInfo(string login);
        [OperationContract(IsOneWay = true)]
        void AddRequest(string sender, string receiver);

        [OperationContract(IsOneWay = true)]
        void SaveUserPhoto(string login, byte[] img);
        [OperationContract]
        List<int> GetAllContact(string login);
        [OperationContract]
        IEnumerable<Request> GetAllRequests(string login, bool isSend);
        [OperationContract]
        string GetLoginUserById(int id);
        [OperationContract(IsOneWay = true)]
        void UpdateOnline(string login, bool loginIn);
        [OperationContract(IsOneWay = true)]
        void AceptRequest(string sender, string receiver);
        [OperationContract(IsOneWay = true)]
        void RejectRequest(string sender, string receiver);
    }
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
