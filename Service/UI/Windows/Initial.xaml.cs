namespace UI.Windows
{
    using System;
    using System.ServiceModel;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;

    public class CallbackHandler : IProgramServiceCallback
    {
        public event Action<string> UserExistEvent;
        public event Action<string> LoginExistEvent;

        public void LoginExist(string exists)
            => LoginExistEvent?.Invoke(exists);

        public void UserExist(string exists)
            => UserExistEvent?.Invoke(exists);
    }

    public partial class Initial : Window
    {
        ProgramServiceClient programServiceClient;

        bool passwordBoxActive = true;
        IInsomableMethods insomable;

        public Initial()
        {
            InitializeComponent();

            insomable = new InsomableMethods();
            Tb_Password.Visibility = Visibility.Hidden;

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.UserExistEvent += UserExist;
            callbackHandler.LoginExistEvent += LoginExist;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            //programServiceClient.CheckUser("MarKer", "qq4");
        }
        string wantedLogin, wantedPassword;
        private void LoginExist(string exists)
        {
            if (Convert.ToBoolean(exists))
                programServiceClient.CheckUser(wantedLogin, wantedPassword);
            else MessageBox.Show("< No user with such login >");
        }

        private void UserExist(string exists)
        {
            if (Convert.ToBoolean(exists))
            {
                // the main window open
            }
            else MessageBox.Show("< Uncorrect password >");
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => Close();

        private void B_Ruby_MouseDown(object sender, MouseButtonEventArgs e)
            => insomable.PasswordAppearance(Tb_Password, Pb_Password, ref passwordBoxActive);

        private void B_SignIn_Click(object sender, RoutedEventArgs e)
        {
            wantedLogin = Tb_Login.Text;
            if (passwordBoxActive)
                wantedPassword = Pb_Password.Password;
            else wantedPassword = Tb_Password.Text;

            if (!String.IsNullOrWhiteSpace(wantedLogin) &&
                !String.IsNullOrWhiteSpace(wantedPassword))
                programServiceClient.CheckLogin(wantedLogin);
            else MessageBox.Show("< Enter all data >");
        }

        private void B_SignUp_Click(object sender, RoutedEventArgs e)
        {
            // the sign up window open
        }
    }
}
