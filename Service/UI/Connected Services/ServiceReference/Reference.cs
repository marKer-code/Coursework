﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UI.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IProgramService", CallbackContract=typeof(UI.ServiceReference.IProgramServiceCallback))]
    public interface IProgramService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/CheckUser", ReplyAction="http://tempuri.org/IProgramService/CheckUserResponse")]
        bool CheckUser(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/CheckUser", ReplyAction="http://tempuri.org/IProgramService/CheckUserResponse")]
        System.Threading.Tasks.Task<bool> CheckUserAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/CheckLogin", ReplyAction="http://tempuri.org/IProgramService/CheckLoginResponse")]
        bool CheckLogin(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/CheckLogin", ReplyAction="http://tempuri.org/IProgramService/CheckLoginResponse")]
        System.Threading.Tasks.Task<bool> CheckLoginAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/LoadUserInfo", ReplyAction="http://tempuri.org/IProgramService/LoadUserInfoResponse")]
        System.Collections.Generic.List<byte[]> LoadUserInfo(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/LoadUserInfo", ReplyAction="http://tempuri.org/IProgramService/LoadUserInfoResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<byte[]>> LoadUserInfoAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetAllContact", ReplyAction="http://tempuri.org/IProgramService/GetAllContactResponse")]
        System.Collections.Generic.List<int> GetAllContact(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetAllContact", ReplyAction="http://tempuri.org/IProgramService/GetAllContactResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<int>> GetAllContactAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetAllRequests", ReplyAction="http://tempuri.org/IProgramService/GetAllRequestsResponse")]
        System.Collections.Generic.List<DAL.Entities.Request> GetAllRequests(string login, bool isSend);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetAllRequests", ReplyAction="http://tempuri.org/IProgramService/GetAllRequestsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<DAL.Entities.Request>> GetAllRequestsAsync(string login, bool isSend);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetLoginUserById", ReplyAction="http://tempuri.org/IProgramService/GetLoginUserByIdResponse")]
        string GetLoginUserById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetLoginUserById", ReplyAction="http://tempuri.org/IProgramService/GetLoginUserByIdResponse")]
        System.Threading.Tasks.Task<string> GetLoginUserByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetNoChat", ReplyAction="http://tempuri.org/IProgramService/GetNoChatResponse")]
        System.Collections.Generic.List<int> GetNoChat(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetNoChat", ReplyAction="http://tempuri.org/IProgramService/GetNoChatResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<int>> GetNoChatAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetAllChats", ReplyAction="http://tempuri.org/IProgramService/GetAllChatsResponse")]
        System.Collections.Generic.List<DAL.Entities.Message> GetAllChats(string sender);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetAllChats", ReplyAction="http://tempuri.org/IProgramService/GetAllChatsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<DAL.Entities.Message>> GetAllChatsAsync(string sender);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetId", ReplyAction="http://tempuri.org/IProgramService/GetIdResponse")]
        int GetId(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetId", ReplyAction="http://tempuri.org/IProgramService/GetIdResponse")]
        System.Threading.Tasks.Task<int> GetIdAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/RemoveChat", ReplyAction="http://tempuri.org/IProgramService/RemoveChatResponse")]
        void RemoveChat(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/RemoveChat", ReplyAction="http://tempuri.org/IProgramService/RemoveChatResponse")]
        System.Threading.Tasks.Task RemoveChatAsync(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddUser")]
        void AddUser(string login, string nickname, string password, byte[] img, bool online, System.DateTime lastOnline);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddUser")]
        System.Threading.Tasks.Task AddUserAsync(string login, string nickname, string password, byte[] img, bool online, System.DateTime lastOnline);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/SaveUserInfo")]
        void SaveUserInfo(string lastLogin, string login, string nickname, string password, byte[] img);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/SaveUserInfo")]
        System.Threading.Tasks.Task SaveUserInfoAsync(string lastLogin, string login, string nickname, string password, byte[] img);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddRequest")]
        void AddRequest(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddRequest")]
        System.Threading.Tasks.Task AddRequestAsync(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/UpdateOnline")]
        void UpdateOnline(string login, string @do);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/UpdateOnline")]
        System.Threading.Tasks.Task UpdateOnlineAsync(string login, string @do);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AcceptRequest")]
        void AcceptRequest(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AcceptRequest")]
        System.Threading.Tasks.Task AcceptRequestAsync(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/RejectRequest")]
        void RejectRequest(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/RejectRequest")]
        System.Threading.Tasks.Task RejectRequestAsync(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/RemoveContact")]
        void RemoveContact(string login, string otherLogin);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/RemoveContact")]
        System.Threading.Tasks.Task RemoveContactAsync(string login, string otherLogin);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddChat")]
        void AddChat(string senderLogin, string receiverLogin);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddChat")]
        System.Threading.Tasks.Task AddChatAsync(string senderLogin, string receiverLogin);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/SendMessage")]
        void SendMessage(string sender, string receiver, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(string sender, string receiver, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProgramServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/ReceiveRequest")]
        void ReceiveRequest(string senderLogin);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/NewContact")]
        void NewContact(string contactLogin);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/RejectRequest_")]
        void RejectRequest_(string senderLogin);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/DeleteContact")]
        void DeleteContact(string toDeleteLogin);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/NewChat_")]
        void NewChat_(string senderLogin);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/DeleteChat")]
        void DeleteChat(string senderLogin);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/ReciveMessage")]
        void ReciveMessage(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProgramServiceChannel : UI.ServiceReference.IProgramService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProgramServiceClient : System.ServiceModel.DuplexClientBase<UI.ServiceReference.IProgramService>, UI.ServiceReference.IProgramService {
        
        public ProgramServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ProgramServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ProgramServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ProgramServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ProgramServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool CheckUser(string login, string password) {
            return base.Channel.CheckUser(login, password);
        }
        
        public System.Threading.Tasks.Task<bool> CheckUserAsync(string login, string password) {
            return base.Channel.CheckUserAsync(login, password);
        }
        
        public bool CheckLogin(string login) {
            return base.Channel.CheckLogin(login);
        }
        
        public System.Threading.Tasks.Task<bool> CheckLoginAsync(string login) {
            return base.Channel.CheckLoginAsync(login);
        }
        
        public System.Collections.Generic.List<byte[]> LoadUserInfo(string login) {
            return base.Channel.LoadUserInfo(login);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<byte[]>> LoadUserInfoAsync(string login) {
            return base.Channel.LoadUserInfoAsync(login);
        }
        
        public System.Collections.Generic.List<int> GetAllContact(string login) {
            return base.Channel.GetAllContact(login);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<int>> GetAllContactAsync(string login) {
            return base.Channel.GetAllContactAsync(login);
        }
        
        public System.Collections.Generic.List<DAL.Entities.Request> GetAllRequests(string login, bool isSend) {
            return base.Channel.GetAllRequests(login, isSend);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<DAL.Entities.Request>> GetAllRequestsAsync(string login, bool isSend) {
            return base.Channel.GetAllRequestsAsync(login, isSend);
        }
        
        public string GetLoginUserById(int id) {
            return base.Channel.GetLoginUserById(id);
        }
        
        public System.Threading.Tasks.Task<string> GetLoginUserByIdAsync(int id) {
            return base.Channel.GetLoginUserByIdAsync(id);
        }
        
        public System.Collections.Generic.List<int> GetNoChat(string login) {
            return base.Channel.GetNoChat(login);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<int>> GetNoChatAsync(string login) {
            return base.Channel.GetNoChatAsync(login);
        }
        
        public System.Collections.Generic.List<DAL.Entities.Message> GetAllChats(string sender) {
            return base.Channel.GetAllChats(sender);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<DAL.Entities.Message>> GetAllChatsAsync(string sender) {
            return base.Channel.GetAllChatsAsync(sender);
        }
        
        public int GetId(string login) {
            return base.Channel.GetId(login);
        }
        
        public System.Threading.Tasks.Task<int> GetIdAsync(string login) {
            return base.Channel.GetIdAsync(login);
        }
        
        public void RemoveChat(string sender, string receiver) {
            base.Channel.RemoveChat(sender, receiver);
        }
        
        public System.Threading.Tasks.Task RemoveChatAsync(string sender, string receiver) {
            return base.Channel.RemoveChatAsync(sender, receiver);
        }
        
        public void AddUser(string login, string nickname, string password, byte[] img, bool online, System.DateTime lastOnline) {
            base.Channel.AddUser(login, nickname, password, img, online, lastOnline);
        }
        
        public System.Threading.Tasks.Task AddUserAsync(string login, string nickname, string password, byte[] img, bool online, System.DateTime lastOnline) {
            return base.Channel.AddUserAsync(login, nickname, password, img, online, lastOnline);
        }
        
        public void SaveUserInfo(string lastLogin, string login, string nickname, string password, byte[] img) {
            base.Channel.SaveUserInfo(lastLogin, login, nickname, password, img);
        }
        
        public System.Threading.Tasks.Task SaveUserInfoAsync(string lastLogin, string login, string nickname, string password, byte[] img) {
            return base.Channel.SaveUserInfoAsync(lastLogin, login, nickname, password, img);
        }
        
        public void AddRequest(string sender, string receiver) {
            base.Channel.AddRequest(sender, receiver);
        }
        
        public System.Threading.Tasks.Task AddRequestAsync(string sender, string receiver) {
            return base.Channel.AddRequestAsync(sender, receiver);
        }
        
        public void UpdateOnline(string login, string @do) {
            base.Channel.UpdateOnline(login, @do);
        }
        
        public System.Threading.Tasks.Task UpdateOnlineAsync(string login, string @do) {
            return base.Channel.UpdateOnlineAsync(login, @do);
        }
        
        public void AcceptRequest(string sender, string receiver) {
            base.Channel.AcceptRequest(sender, receiver);
        }
        
        public System.Threading.Tasks.Task AcceptRequestAsync(string sender, string receiver) {
            return base.Channel.AcceptRequestAsync(sender, receiver);
        }
        
        public void RejectRequest(string sender, string receiver) {
            base.Channel.RejectRequest(sender, receiver);
        }
        
        public System.Threading.Tasks.Task RejectRequestAsync(string sender, string receiver) {
            return base.Channel.RejectRequestAsync(sender, receiver);
        }
        
        public void RemoveContact(string login, string otherLogin) {
            base.Channel.RemoveContact(login, otherLogin);
        }
        
        public System.Threading.Tasks.Task RemoveContactAsync(string login, string otherLogin) {
            return base.Channel.RemoveContactAsync(login, otherLogin);
        }
        
        public void AddChat(string senderLogin, string receiverLogin) {
            base.Channel.AddChat(senderLogin, receiverLogin);
        }
        
        public System.Threading.Tasks.Task AddChatAsync(string senderLogin, string receiverLogin) {
            return base.Channel.AddChatAsync(senderLogin, receiverLogin);
        }
        
        public void SendMessage(string sender, string receiver, string message) {
            base.Channel.SendMessage(sender, receiver, message);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(string sender, string receiver, string message) {
            return base.Channel.SendMessageAsync(sender, receiver, message);
        }
    }
}
