namespace UI.Windows
{
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using UI.InsomableMethods_;
    using UI.ServiceReference;

    public partial class Main : Window
    {
        ProgramServiceClient programServiceClient;

        IInsomableMethods insomable;

        string login_, password_, nickname_;
        byte[] photo_;

        public Main(string login, string password, string nickname, byte[] photo)
        {
            InitializeComponent();

            insomable = new InsomableMethods();
            programServiceClient = new ProgramServiceClient();

            if (!programServiceClient.CheckLogin(login))
                insomable.OpenWindow(new SignUp(), this);

            login_ = login;
            password_ = password;

            if (nickname != null)
            {
                photo_ = photo;
                nickname_ = nickname;

                Inizialize();
            }
            else
            {
                byte[][] infoes = programServiceClient.LoadUserInfo(login_);

                nickname_ = Encoding.Default.GetString(infoes[0]);
                photo_ = infoes[2];

                Inizialize();
            }
        }

        private void Inizialize()
        {
            tb_login.Text = login_;
            tb_password.Text = password_;
            tb_nickname.Text = nickname_;
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(photo_.ToArray());
            var handle = bitmap1.GetHbitmap();
            Avatar.Source = Imaging.CreateBitmapSourceFromHBitmap(handle,
            IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        string lastLogin;
        private void Flipper_b_Click(object sender, RoutedEventArgs e)
        {
            switch (Flipper_b.Content)
            {
                case "Save":
                    SaveAcc();
                    break;
                default:
                    Search();
                    break;
            }
        }

        private void SaveAcc()
        {
            if (tb_nickname.Text == nickname_ &&
                tb_login.Text == login_ &&
                tb_password.Text == password_ &&
                !ing)
                return;

            if (tb_login.Text.Length < 2)
            {
                MessageBox.Show("< Short login >");
                return;
            }
            if (tb_nickname.Text.Length < 2)
            {
                MessageBox.Show("< Short nickname >");
                return;
            }
            if (tb_password.Text.Length < 8)
            {
                MessageBox.Show("< Short password >");
                return;
            }

            if (!String.IsNullOrWhiteSpace(tb_login.Text) &&
                !String.IsNullOrEmpty(tb_nickname.Text) &&
                !String.IsNullOrEmpty(tb_password.Text))
            {
                if (tb_login.Text != login_)
                    login_ = tb_login.Text;
                if (tb_password.Text != password_)
                    password_ = tb_password.Text;
                if (tb_nickname.Text != nickname_)
                    nickname_ = tb_nickname.Text;

                //save
            }

            MessageBox.Show("< Saved >");
        }
        private void Search()
        {
            if (lb_contacts.Items.Contains(tb_login.Text))
            {
                MessageBox.Show("< You already have such a friend >");
                return;
            }
            if (tb_login.Text == lastLogin)
            {
                MessageBox.Show("< You can't add yourself  >");
                return;
            }
            if (tb_login.Text.Length < 2)
            {
                MessageBox.Show("< There is no user with this login >");
                return;
            }

            if (!String.IsNullOrWhiteSpace(tb_login.Text))
            {
                //Search for a user by >
                //send him a friend request > 
                //add his login to the list of requests for the window
            }
        }

        bool ing = false;
        private void Avatar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse photo",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "jpg",
                Filter = "jpg (*.jpg)|*.jpg|png (*.png)|*.png",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (ofd.ShowDialog() == true)
                SetPhoto(ofd);
        }
        private void SetPhoto(OpenFileDialog ofd)
        {
            photo_ = File.ReadAllBytes(ofd.FileName);
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(photo_.ToArray());
            var handle = bitmap1.GetHbitmap();
            Avatar.Source = Imaging.CreateBitmapSourceFromHBitmap(handle,
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        enum SELECTION { ACCOUNTINFO = 0, ALLCHATS = 1, CONTACTS = 2 };

        private void I_Profile_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (flipper.Visibility == Visibility.Visible && Avatar.Visibility == Visibility.Visible)
            {
                lb_contacts.Visibility = Visibility.Hidden;
                lb_requests.Visibility = Visibility.Hidden;
                flipper.IsEnabled = false;
                flipper.Visibility = Visibility.Collapsed;
            }
            else
            {
                l_nicname.Visibility = Visibility.Visible;
                l_pass.Visibility = Visibility.Visible;

                lb_contacts.Visibility = Visibility.Hidden;
                lb_requests.Visibility = Visibility.Hidden;

                tb_login.Text = login_;
                tb_password.Visibility = Visibility.Visible;
                tb_nickname.Visibility = Visibility.Visible;
                Avatar.Visibility = Visibility.Visible;
                Flipper_b.Content = "Save";


                lastLogin = login_;
                flipper.IsEnabled = true;
                flipper.Visibility = Visibility.Visible;
            }
        }

        private void chat_badged_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lb_contacts.Visibility = Visibility.Hidden;
            lb_requests.Visibility = Visibility.Hidden;
            flipper.IsEnabled = false;
            flipper.Visibility = Visibility.Hidden;
        }

        private void allContacts_badget_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lb_contacts.Visibility = Visibility.Visible;
            lb_requests.Visibility = Visibility.Hidden;
            flipper.IsEnabled = false;
            flipper.Visibility = Visibility.Hidden;
        }

        private void addFriend_badget_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (flipper.Visibility == Visibility.Visible &&
                Avatar.Visibility == Visibility.Hidden)
            {
                lb_contacts.Visibility = Visibility.Hidden;
                lb_requests.Visibility = Visibility.Hidden;
                flipper.IsEnabled = false;
                flipper.Visibility = Visibility.Collapsed;
            }
            else
            {
                l_nicname.Visibility = Visibility.Hidden;
                l_pass.Visibility = Visibility.Hidden;

                lb_contacts.Visibility = Visibility.Hidden;
                lb_requests.Visibility = Visibility.Visible;

                tb_login.Text = null;
                tb_password.Visibility = Visibility.Hidden;
                tb_nickname.Visibility = Visibility.Hidden;
                Avatar.Visibility = Visibility.Hidden;
                Flipper_b.Content = "Search";

                flipper.IsEnabled = true;
                flipper.Visibility = Visibility.Visible;
            }
        }
    }
}
