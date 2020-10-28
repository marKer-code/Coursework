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
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/CheckUser")]
        void CheckUser(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/CheckUser")]
        System.Threading.Tasks.Task CheckUserAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/CheckLogin")]
        void CheckLogin(string login);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/CheckLogin")]
        System.Threading.Tasks.Task CheckLoginAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddUser")]
        void AddUser(string login, string nickname, string password, byte[] img, bool online, System.DateTime lastOnline);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/AddUser")]
        System.Threading.Tasks.Task AddUserAsync(string login, string nickname, string password, byte[] img, bool online, System.DateTime lastOnline);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProgramServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/UserExist")]
        void UserExist(string exists);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IProgramService/LoginExist")]
        void LoginExist(string exists);
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
        
        public void CheckUser(string login, string password) {
            base.Channel.CheckUser(login, password);
        }
        
        public System.Threading.Tasks.Task CheckUserAsync(string login, string password) {
            return base.Channel.CheckUserAsync(login, password);
        }
        
        public void CheckLogin(string login) {
            base.Channel.CheckLogin(login);
        }
        
        public System.Threading.Tasks.Task CheckLoginAsync(string login) {
            return base.Channel.CheckLoginAsync(login);
        }
        
        public void AddUser(string login, string nickname, string password, byte[] img, bool online, System.DateTime lastOnline) {
            base.Channel.AddUser(login, nickname, password, img, online, lastOnline);
        }
        
        public System.Threading.Tasks.Task AddUserAsync(string login, string nickname, string password, byte[] img, bool online, System.DateTime lastOnline) {
            return base.Channel.AddUserAsync(login, nickname, password, img, online, lastOnline);
        }
    }
}
