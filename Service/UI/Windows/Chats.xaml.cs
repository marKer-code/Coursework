namespace UI
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    using UI.Windows;

    public partial class Chats : Window
    {
        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_;
        byte[] photo_;

        public Chats(string login, string password, string nickname, byte[] photo)
        {
            InitializeComponent();
            insomable = new InsomableMethods();

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.ReceiveRequestEvent += ReceiveRequest;
            callbackHandler.NewContactEvent += NewContact;
            callbackHandler.RejectRequest_Event += RejectRequest_;
            callbackHandler.DeleteContactEvent += DeleteContact;
            callbackHandler.NewChatEvent += NewChat;



            callbackHandler.DeleteChatEvent += DeleteChat;


            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            LoadInfo(login, password, nickname, photo);
        }


        private void DeleteChat(string toDeleteLogin)

        {
            Lists.chats.Remove(toDeleteLogin);
            MessageBox.Show("");
        }

        private void NewChat(string senderLogin)
        {
            Lists.chats.Add(senderLogin);
            if (Lists.noChat.Contains(senderLogin))
                Lists.noChat.Remove(senderLogin);
        }

        private void DeleteContact(string toDeleteLogin)
            => Lists.contacts.Remove(toDeleteLogin);

        private void RejectRequest_(string receiverLogin)
            => Lists.sendRequests.Remove(receiverLogin);

        private void NewContact(string contactLogin)
        {
            Lists.sendRequests.Remove(contactLogin);
            Lists.noChat.Add(contactLogin);
            Lists.contacts.Add(contactLogin);
        }

        private void ReceiveRequest(string senderLogin)
            => Lists.receivedRequests.Add(senderLogin);

        private void Window_Closed(object sender, System.EventArgs e)
            => programServiceClient.UpdateOnlineAsync(login_, false);

        private void Window_Loaded(object sender, RoutedEventArgs e)
            => programServiceClient.UpdateOnlineAsync(login_, true);

       
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

            cb_contact.ItemsSource = Lists.noChat;
            lb_chats.ItemsSource = Lists.chats;
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => Close();
    }
}
