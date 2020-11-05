namespace UI.Windows
{
    using System;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;

    public class CallbackHandler// : IProgramServiceCallback
    {
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
            programServiceClient = new ProgramServiceClient();

            Tb_Password.Visibility = Visibility.Hidden;
        }
        string wantedLogin, wantedPassword;

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
                if (programServiceClient.CheckLogin(wantedLogin))
                {
                    if (programServiceClient.CheckUser(wantedLogin, wantedPassword))
                        insomable.OpenWindow(
                            new Main(wantedLogin, wantedPassword,
                            null, Encoding.Default.GetBytes("0")), this);
                    else MessageBox.Show("< Uncorrect password >");
                }
                else MessageBox.Show("< No user with such login >");
            else MessageBox.Show("< Enter all data >");
        }

        private void B_SignUp_Click(object sender, RoutedEventArgs e)
            => insomable.OpenWindow(new SignUp(), this);
    }
}
