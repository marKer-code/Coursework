namespace UI.Windows
{
    using System;
    using System.ServiceModel;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    using UI.Windows.MainWindow;

    public class CallbackHandler : IProgramServiceCallback
    {
        public event Action<string> ReceiveRequestEvent;
        public event Action<string> NewContactEvent;
        public event Action<string> RejectRequest_Event;
        public event Action<string> DeleteContactEvent;
        public event Action<string> NewChatEvent;

        public void DeleteContact(string toDeleteLogin)
            => DeleteContactEvent?.Invoke(toDeleteLogin);

        public void NewChat_(string senderLogin)
            => NewChatEvent.Invoke(senderLogin);

        public void NewContact(string contactLogin)
            => NewContactEvent?.Invoke(contactLogin);

        public void ReceiveRequest(string senderLogin)
            => ReceiveRequestEvent?.Invoke(senderLogin);

        public void RejectRequest_(string receiverLogin)
            => RejectRequest_Event?.Invoke(receiverLogin);
    }

    public partial class Initial : Window
    {
        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        bool passwordBoxActive = true;
        string wantedLogin, wantedPassword;

        public Initial()
        {
            InitializeComponent();

            insomable = new InsomableMethods();

            CallbackHandler callbackHandler = new CallbackHandler();

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            Tb_Password.Visibility = Visibility.Hidden;
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => Close();

        private void B_Ruby_MouseDown(object sender, MouseButtonEventArgs e)
            => insomable.PasswordAppearance(Tb_Password, Pb_Password, ref passwordBoxActive);

        private void B_SignIn_Click(object sender, RoutedEventArgs e)
        {
            wantedLogin = Tb_Login.Text;

            switch (passwordBoxActive.ToString())
            {
                case "True":
                    {
                        wantedPassword = Pb_Password.Password;
                        break;
                    }
                case "False":
                    {
                        wantedPassword = Tb_Password.Text;
                        break;
                    }
            }

            if (!String.IsNullOrWhiteSpace(wantedLogin) &&
                !String.IsNullOrWhiteSpace(wantedPassword))
                switch (programServiceClient.CheckLogin(wantedLogin).ToString())
                {
                    case "True":
                        {
                            switch (programServiceClient.CheckUser(wantedLogin, wantedPassword).ToString())
                            {
                                case "True":
                                    {
                                        insomable.OpenWindow(
                                            new Main(wantedLogin, wantedPassword,
                                            null, Encoding.Default.GetBytes("0")), this);
                                        break;
                                    }
                                default:
                                    {
                                        MessageBox.Show("< Uncorrect password >");
                                        break;
                                    }
                            }
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("< No user with such login >");
                            break;
                        }
                }
            else MessageBox.Show("< Enter all data >");
        }

        private void B_SignUp_Click(object sender, RoutedEventArgs e)
            => insomable.OpenWindow(new SignUp(), this);
    }
}
