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

    public partial class Contact : Window
    {
        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_;
        byte[] photo_;

        public Contact(string login, string password, string nickname, byte[] photo)
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
            callbackHandler.ReceiveMessageEvent += CallbackHandler_ReceiveMessageEvent;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            LoadInfo(login, password, nickname, photo);
        }
        private void CallbackHandler_ReceiveMessageEvent(string obj)
        {
            MessageBox.Show(login_ + " " + obj + "Contact");
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
            Lists.noChat.Add(contactLogin);
            Lists.contacts.Add(contactLogin);
        }

        private void ReceiveRequest(string senderLogin)
            => Lists.receivedRequests.Add(senderLogin);

        private void Window_Closed(object sender, System.EventArgs e)
        {

        }

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

            lb_contacts.ItemsSource = Lists.contacts;
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            programServiceClient.UpdateOnline(login_, "Remove");
            Close();
        }
    }
}
