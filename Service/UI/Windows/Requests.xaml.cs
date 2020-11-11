﻿namespace UI
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    using UI.Windows;

    public partial class Requests : Window
    {
        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_, lastLogin;
        byte[] photo_;

        public Requests(string login, string password, string nickname, byte[] photo)
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

            LoadInfo(login, password, nickname, photo);
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
        private void DeleteChat(string toDeleteLogin)
        {
            Lists.chats.Remove(toDeleteLogin);
            foreach (var item in Lists.messages)
                if (item.Value == toDeleteLogin)
                    Lists.messages.Remove(item.Key);
        }

        private void NewChat(string senderLogin)
        {
            Lists.chats.Add(senderLogin);
            if (Lists.noChat.Contains(senderLogin))
                Lists.noChat.Remove(senderLogin);
        }
        private void DeleteContact(string toDeleteLogin)
        {
            Lists.contacts.Remove(toDeleteLogin);
            Lists.chats.Remove(toDeleteLogin);
            foreach (var item in Lists.messages)
                if (item.Value == toDeleteLogin)
                    Lists.messages.Remove(item.Key);
        }

        private void RejectRequest_(string receiverLogin)
            => Lists.sendRequests.Remove(receiverLogin);

        private void NewContact(string contactLogin)
        {
            Lists.sendRequests.Remove(contactLogin);
            Lists.contacts.Add(contactLogin);
            Lists.noChat.Add(contactLogin);
        }

        private void Window_Closed(object sender, System.EventArgs e)
            => programServiceClient.UpdateOnline(login_, "Remove");

        private void ReceiveRequest(string senderLogin)
            => Lists.receivedRequests.Add(senderLogin);

        private void LoadInfo(string login, string password, string nickname, byte[] photo)
        {
            login_ = login;
            password_ = password;
            programServiceClient.UpdateOnline(login, "ChangeCallback");

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

            lastLogin = login_;

            lb_requests.ItemsSource = Lists.receivedRequests;
            lb_requests_Send.ItemsSource = Lists.sendRequests;
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => Close();
    }
}
