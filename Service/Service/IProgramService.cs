namespace Service
{
    using DAL.Entities;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(ICallback))]
    public partial interface IProgramService
    {
        [OperationContract]
        bool CheckUser(string login, string password);

        [OperationContract]
        bool CheckLogin(string login);

        [OperationContract]
        List<byte[]> LoadUserInfo(string login);

        [OperationContract]
        List<int> GetAllContact(string login);

        [OperationContract]
        List<Request> GetAllRequests
            (string login, bool isSend);

        [OperationContract]
        string GetLoginUserById(int id);

        [OperationContract]
        List<int> GetNoChat(string login);

        [OperationContract]
        List<DAL.Entities.Message> GetAllChats(string sender);
        [OperationContract]
        int GetId(string login);
    }
}
