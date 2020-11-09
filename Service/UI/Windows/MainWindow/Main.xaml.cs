namespace UI.Windows.MainWindow
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;

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

            callbackHandler.ReceiveRequestEvent += ReceiveRequest;
            callbackHandler.NewContactEvent += NewContact;
            callbackHandler.RejectRequest_Event += RejectRequest_;
            callbackHandler.DeleteContactEvent += DeleteContact;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            programServiceClient.UpdateOnline(login, true);

            if (!programServiceClient.CheckLogin(login))
                insomable.OpenWindow(new SignUp(), this);

            Task.Run(() => LoadInfo(login, password, nickname, photo));
        }

        private void DeleteContact(string toDeleteLogin)
            => Lists.contacts.Remove(toDeleteLogin);

        private void RejectRequest_(string receiverLogin)
            => Lists.sendRequests.Remove(receiverLogin);

        private void NewContact(string contactLogin)
        {
            Lists.sendRequests.Remove(contactLogin);
            Lists.contacts.Add(contactLogin);
        }

        private void ReceiveRequest(string senderLogin)
            => Lists.receivedRequests.Add(senderLogin);

        private void Window_Closed(object sender, System.EventArgs e)
            => programServiceClient.UpdateOnlineAsync(login_, false);

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
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => Close();
    }
}
