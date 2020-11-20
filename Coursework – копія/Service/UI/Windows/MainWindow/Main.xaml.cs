﻿namespace UI.Windows.MainWindow
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    using UI.Windows;

    public partial class Main : Window
    {
        #region Fields

        private enum SELECTION { ACCOUNTINFO = 0, ALLCHATS = 1, CONTACTS = 2 };
        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_;
        byte[] photo_;

        #endregion

        public Main(string login, string password, string nickname, byte[] photo)
        {
            InitializeComponent();

            insomable = new InsomableMethods();

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.NewContactEvent += NewContact;
            callbackHandler.ReceiveRequestEvent += ReceiveRequest;
            callbackHandler.RejectRequest_Event += RejectRequest_;
            callbackHandler.DeleteContactEvent += DeleteContact;
            callbackHandler.NewChatEvent += NewChat;

            callbackHandler.ReceiveMessageEvent += CallbackHandler_ReceiveMessageEvent;
            callbackHandler.DeleteChatEvent += DeleteChat;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            if (!programServiceClient.CheckLogin(login))
                insomable.OpenWindow(new SignUp(), this);

            Task.Run(() => LoadInfo(login, password, nickname, photo));
        }

        private void DeleteChat(string toDeleteLogin)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Lists.chats.Remove(toDeleteLogin);
                foreach (var item in Lists.messages)
                    if (item.Value == toDeleteLogin)
                        Lists.messages.Remove(item.Key);
            }));
        }

        private void NewChat(string senderLogin)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Lists.chats.Add(senderLogin);
                if (Lists.noChat.Contains(senderLogin))
                    Lists.noChat.Remove(senderLogin);
            }));
        }

        private void DeleteContact(string toDeleteLogin)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Lists.contacts.Remove(toDeleteLogin);
                Lists.chats.Remove(toDeleteLogin);
                foreach (var item in Lists.messages)
                    if (item.Value == toDeleteLogin)
                        Lists.messages.Remove(item.Key);
            }
            ));
        }

        private void RejectRequest_(string receiverLogin)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Lists.sendRequests.Remove(receiverLogin);
            }));
        }

        private void ReceiveRequest(string senderLogin)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Lists.receivedRequests.Add(senderLogin);
            }));
        }

        private void NewContact(string contactLogin)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Lists.sendRequests.Remove(contactLogin);
                Lists.contacts.Add(contactLogin);
                Lists.noChat.Add(contactLogin);
            }));
        }
        private void CallbackHandler_ReceiveMessageEvent(string obj)
        {
            string[] mes = obj.Split(' ');
            Lists.messages.Add(new List<string>()
            {
                mes[1],
                mes[2],
                mes[3]
            },
            mes[0]);
        }

        private void Window_Closed(object sender, System.EventArgs e)
            => programServiceClient.UpdateOnline(login_, "Remove");

        private void LoadInfo(string login, string password, string nickname, byte[] photo)
        {
            login_ = login;
            password_ = password;

            switch (nickname)
            {
                case null:
                    {
                        List<byte[]> infoes = programServiceClient.LoadUserInfo(login_);

                        nickname_ = Encoding.Default.GetString(infoes[0]);
                        photo_ = infoes[2];

                        break;
                    }
                default:
                    {
                        photo_ = photo;
                        nickname_ = nickname;

                        break;
                    }
            }

            foreach (var u in programServiceClient.GetAllContact(login_))
                Lists.contacts
                     .Add(programServiceClient
                     .GetLoginUserByIdAsync(u)
                     .Result);

            foreach (var request in programServiceClient.GetAllRequests(login_, false))
                Lists.receivedRequests
                    .Add(programServiceClient
                    .GetLoginUserByIdAsync(request.SenderId)
                    .Result);

            foreach (var request in programServiceClient.GetAllRequests(login_, true))
                Lists.sendRequests
                    .Add(programServiceClient
                    .GetLoginUserByIdAsync(request.ReceiverId)
                    .Result);


            int id = programServiceClient.GetId(login_);

            List<Message> messages = programServiceClient.GetAllChats(login);

            foreach (var message in messages)
            {
                if (message.SenderId == id)
                    continue;

                if (message.ReceiverId != id &&
                !Lists.chats.Contains(
                    programServiceClient.GetLoginUserById(message.ReceiverId)))
                    Lists.chats
                        .Add(programServiceClient
                        .GetLoginUserByIdAsync(message.ReceiverId)
                        .Result);
                else if (!Lists.chats.Contains(
                        programServiceClient.GetLoginUserById(message.SenderId)))
                    Lists.chats
                        .Add(programServiceClient
                        .GetLoginUserByIdAsync(message.SenderId)
                        .Result);
            }

            foreach (var item in Lists.chats)
            {
                if (item == login_)
                    continue;

                List<Message> messages_ =
                    messages
                        .Where(m => m.SenderId == programServiceClient.GetId(item) ||
                            m.ReceiverId == programServiceClient.GetId(item))
                        .ToList();

                foreach (var item2 in messages_)
                {
                    if (item2.File == null)
                    {
                        List<string> q = new List<string>()
                        {
                            item2.SenderId.ToString(),
                            item2.ReceiverId.ToString(),
                            item2.Text
                        };
                        Lists.messages.Add(q, item);
                    }
                    else
                    {
                        List<string> q = new List<string>()
                        {
                            item2.SenderId.ToString(),
                            item2.ReceiverId.ToString(),
                            "File > " + item2.Text
                        };
                        Lists.messages.Add(q, item);
                    }
                }
            }

            foreach (var noChat in programServiceClient.GetNoChat(login))
                if (!Lists.noChat.Contains(programServiceClient.GetLoginUserById(noChat)))
                    if (!Lists.chats.Contains(programServiceClient.GetLoginUserById(noChat)))
                        Lists.noChat
                        .Add(programServiceClient
                        .GetLoginUserByIdAsync(noChat)
                        .Result);
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => Parallel.Invoke(() => this.Close());
    }
}
