namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    using UI.Windows;

    public partial class Contact : Window
    {
        #region Fields

        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_;
        byte[] photo_;

        #endregion

        public Contact(string login, string password, string nickname, byte[] photo)
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

            Dispatcher.BeginInvoke(new Action(() =>
            {
                lb_contacts.ItemsSource = Lists.contacts;
            }));
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => Close();
    }
}
