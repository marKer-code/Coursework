namespace Service
{
    using System;
    using System.ServiceModel;

    public partial interface IProgramService
    {
        [OperationContract(IsOneWay = true)]
        void AddUser(string login, string nickname, string password,
            byte[] img, bool online, DateTime lastOnline);

        [OperationContract(IsOneWay = true)]
        void SaveUserInfo(string lastLogin, string login,
            string nickname, string password, byte[] img);

        [OperationContract(IsOneWay = true)]
        void AddRequest(string sender, string receiver);

        [OperationContract(IsOneWay = true)]
        void UpdateOnline(string login, string @do);
        [OperationContract(IsOneWay = true)]
        void AcceptRequest(string sender, string receiver);

        [OperationContract(IsOneWay = true)]
        void RejectRequest(string sender, string receiver);

        [OperationContract(IsOneWay = true)]
        void RemoveContact(string login, string otherLogin);

        [OperationContract(IsOneWay = true)]
        void AddChat(string senderLogin, string receiverLogin);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string sender, string receiver, string message, string fileName, byte[] file);
    }
}
