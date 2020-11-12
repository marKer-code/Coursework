﻿namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    using UI.Windows;

    public partial class Chats : Window
    {
        #region Fields

        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_;
        byte[] photo_;

        #endregion

        public Chats(string login, string password, string nickname, byte[] photo)
        {
            InitializeComponent();
            insomable = new InsomableMethods();

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.ReceiveRequestEvent += new Events_CallbackHandler().ReceiveRequest;
            callbackHandler.NewContactEvent += new Events_CallbackHandler().NewContact;
            callbackHandler.RejectRequest_Event += new Events_CallbackHandler().RejectRequest_;
            callbackHandler.DeleteContactEvent += new Events_CallbackHandler().DeleteContact;
            callbackHandler.NewChatEvent += new Events_CallbackHandler().NewChat;

            callbackHandler.DeleteChatEvent += new Events_CallbackHandler().DeleteChat;
            callbackHandler.ReceiveMessageEvent += CallbackHandler_ReceiveMessageEvent;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            Task.Run(() => LoadInfo(login, password, nickname, photo));
        }

        private void CallbackHandler_ReceiveMessageEvent(string obj)
        {
            string[] mes = obj.Split(' ');
            new Events_CallbackHandler().ReceiveMessage(obj);
            if (Lists.chatOn == mes[0])
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    lb_chats.SelectedItem = mes[0];
                }));
        }

        private void Window_Closed(object sender, System.EventArgs e)
            => programServiceClient.UpdateOnline(login_, "Remove");

        private void LoadInfo(string login, string password, string nickname, byte[] photo)
        {
            login_ = login;
            password_ = password;
            programServiceClient.UpdateOnline(login, "ChangeCallback");

            switch (nickname)
            {
                case null:
                    {
                        Thread.Sleep(100);
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
            Dispatcher.BeginInvoke(new Action(() =>
            {
                cb_contact.ItemsSource = Lists.noChat;
                lb_chats.ItemsSource = Lists.chats;
            }));
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => Close();
    }
}
