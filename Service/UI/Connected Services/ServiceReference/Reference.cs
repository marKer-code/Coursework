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
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Request", Namespace="http://schemas.datacontract.org/2004/07/DAL.Entities")]
    [System.SerializableAttribute()]
    public partial class Request : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ReceiverIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime SendTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SenderIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ReceiverId {
            get {
                return this.ReceiverIdField;
            }
            set {
                if ((this.ReceiverIdField.Equals(value) != true)) {
                    this.ReceiverIdField = value;
                    this.RaisePropertyChanged("ReceiverId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime SendTime {
            get {
                return this.SendTimeField;
            }
            set {
                if ((this.SendTimeField.Equals(value) != true)) {
                    this.SendTimeField = value;
                    this.RaisePropertyChanged("SendTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SenderId {
            get {
                return this.SenderIdField;
            }
            set {
                if ((this.SenderIdField.Equals(value) != true)) {
                    this.SenderIdField = value;
                    this.RaisePropertyChanged("SenderId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IProgramService")]
    public interface IProgramService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/CheckUser", ReplyAction="http://tempuri.org/IProgramService/CheckUserResponse")]
        bool CheckUser(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/CheckUser", ReplyAction="http://tempuri.org/IProgramService/CheckUserResponse")]
        System.Threading.Tasks.Task<bool> CheckUserAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/CheckLogin", ReplyAction="http://tempuri.org/IProgramService/CheckLoginResponse")]
        bool CheckLogin(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/CheckLogin", ReplyAction="http://tempuri.org/IProgramService/CheckLoginResponse")]
        System.Threading.Tasks.Task<bool> CheckLoginAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddUser")]
        void AddUser(string login, string nickname, string password, byte[] img, bool online, System.DateTime lastOnline);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddUser")]
        System.Threading.Tasks.Task AddUserAsync(string login, string nickname, string password, byte[] img, bool online, System.DateTime lastOnline);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/SaveUserInfo")]
        void SaveUserInfo(string lastLogin, string login, string nickname, string password, byte[] img);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/SaveUserInfo")]
        System.Threading.Tasks.Task SaveUserInfoAsync(string lastLogin, string login, string nickname, string password, byte[] img);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/LoadUserInfo", ReplyAction="http://tempuri.org/IProgramService/LoadUserInfoResponse")]
        System.Collections.Generic.List<byte[]> LoadUserInfo(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/LoadUserInfo", ReplyAction="http://tempuri.org/IProgramService/LoadUserInfoResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<byte[]>> LoadUserInfoAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddRequest")]
        void AddRequest(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddRequest")]
        System.Threading.Tasks.Task AddRequestAsync(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/DeletedRequest")]
        void DeletedRequest(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/DeletedRequest")]
        System.Threading.Tasks.Task DeletedRequestAsync(string sender, string receiver);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddCouple")]
        void AddCouple(string user1, string ser2);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddCouple")]
        System.Threading.Tasks.Task AddCoupleAsync(string user1, string ser2);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/SaveUserPhoto")]
        void SaveUserPhoto(string login, byte[] img);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/SaveUserPhoto")]
        System.Threading.Tasks.Task SaveUserPhotoAsync(string login, byte[] img);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetAllContact", ReplyAction="http://tempuri.org/IProgramService/GetAllContactResponse")]
        System.Collections.Generic.List<int> GetAllContact(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetAllContact", ReplyAction="http://tempuri.org/IProgramService/GetAllContactResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<int>> GetAllContactAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetAllRequests", ReplyAction="http://tempuri.org/IProgramService/GetAllRequestsResponse")]
        System.Collections.Generic.List<UI.ServiceReference.Request> GetAllRequests(string login, bool isSend);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetAllRequests", ReplyAction="http://tempuri.org/IProgramService/GetAllRequestsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<UI.ServiceReference.Request>> GetAllRequestsAsync(string login, bool isSend);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetLoginUserById", ReplyAction="http://tempuri.org/IProgramService/GetLoginUserByIdResponse")]
        string GetLoginUserById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProgramService/GetLoginUserById", ReplyAction="http://tempuri.org/IProgramService/GetLoginUserByIdResponse")]
        System.Threading.Tasks.Task<string> GetLoginUserByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/UpdateOnline")]
        void UpdateOnline(string login, bool loginIn);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/UpdateOnline")]
        System.Threading.Tasks.Task UpdateOnlineAsync(string login, bool loginIn);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProgramServiceChannel : UI.ServiceReference.IProgramService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProgramServiceClient : System.ServiceModel.ClientBase<UI.ServiceReference.IProgramService>, UI.ServiceReference.IProgramService {
        
        public ProgramServiceClient() {
        }
        
        public ProgramServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProgramServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProgramServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProgramServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
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
        
        public System.Collections.Generic.List<byte[]> LoadUserInfo(string login) {
            return base.Channel.LoadUserInfo(login);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<byte[]>> LoadUserInfoAsync(string login) {
            return base.Channel.LoadUserInfoAsync(login);
        }
        
        public void AddRequest(string sender, string receiver) {
            base.Channel.AddRequest(sender, receiver);
        }
        
        public System.Threading.Tasks.Task AddRequestAsync(string sender, string receiver) {
            return base.Channel.AddRequestAsync(sender, receiver);
        }
        
        public void DeletedRequest(string sender, string receiver) {
            base.Channel.DeletedRequest(sender, receiver);
        }
        
        public System.Threading.Tasks.Task DeletedRequestAsync(string sender, string receiver) {
            return base.Channel.DeletedRequestAsync(sender, receiver);
        }
        
        public void AddCouple(string user1, string ser2) {
            base.Channel.AddCouple(user1, ser2);
        }
        
        public System.Threading.Tasks.Task AddCoupleAsync(string user1, string ser2) {
            return base.Channel.AddCoupleAsync(user1, ser2);
        }
        
        public void SaveUserPhoto(string login, byte[] img) {
            base.Channel.SaveUserPhoto(login, img);
        }
        
        public System.Threading.Tasks.Task SaveUserPhotoAsync(string login, byte[] img) {
            return base.Channel.SaveUserPhotoAsync(login, img);
        }
        
        public System.Collections.Generic.List<int> GetAllContact(string login) {
            return base.Channel.GetAllContact(login);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<int>> GetAllContactAsync(string login) {
            return base.Channel.GetAllContactAsync(login);
        }
        
        public System.Collections.Generic.List<UI.ServiceReference.Request> GetAllRequests(string login, bool isSend) {
            return base.Channel.GetAllRequests(login, isSend);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<UI.ServiceReference.Request>> GetAllRequestsAsync(string login, bool isSend) {
            return base.Channel.GetAllRequestsAsync(login, isSend);
        }
        
        public string GetLoginUserById(int id) {
            return base.Channel.GetLoginUserById(id);
        }
        
        public System.Threading.Tasks.Task<string> GetLoginUserByIdAsync(int id) {
            return base.Channel.GetLoginUserByIdAsync(id);
        }
        
        public void UpdateOnline(string login, bool loginIn) {
            base.Channel.UpdateOnline(login, loginIn);
        }
        
        public System.Threading.Tasks.Task UpdateOnlineAsync(string login, bool loginIn) {
            return base.Channel.UpdateOnlineAsync(login, loginIn);
        }
    }
}
