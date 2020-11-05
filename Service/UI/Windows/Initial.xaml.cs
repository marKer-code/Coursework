namespace UI.Windows
{
    using System;
    using System.ServiceModel;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;

    public class CallbackHandler : IProgramServiceCallback
    {
        public event Action<string> UserExistEvent;
        public event Action<string> LoginExistEvent;
        public event Action<string, bool, DateTime, byte[]> UserInfoEvent;

        public void LoginExist(string exists)
            => LoginExistEvent?.Invoke(exists);

        public void UserExist(string exists)
            => UserExistEvent?.Invoke(exists);

        public void UserInfo(string nickname, bool online, DateTime lastOnline, byte[] photo)
            => UserInfoEvent?.Invoke(nickname, online, lastOnline, photo);
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
        }
        string wantedLogin, wantedPassword;
        private void LoginExist(string exists)
        {
            if (Convert.ToBoolean(exists))
                programServiceClient.CheckUserAsync(wantedLogin, wantedPassword);
            else MessageBox.Show("< No user with such login >");
        }

        private void UserExist(string exists)
        {
            if (Convert.ToBoolean(exists))
                insomable.OpenWindow(new Main(wantedLogin, wantedPassword, null, Encoding.Default.GetBytes("0")), this);
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
            => insomable.OpenWindow(new SignUp(), this);
    }
}
