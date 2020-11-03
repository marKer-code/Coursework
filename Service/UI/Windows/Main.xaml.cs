namespace UI.Windows
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Text;
    using System.Threading.Tasks;
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

        public Main(string login, string paswword, string nickname, byte[] photo)
        {
            InitializeComponent();

            login_ = login;
            password_ = paswword;
            nickname_ = nickname;
            photo_ = photo;

            insomable = new InsomableMethods();

            CallbackHandler callbackHandler = new CallbackHandler();

            //callbackHandler.UserExistEvent += UserExist;
            //callbackHandler.LoginExistEvent += LoginExist;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            Inizialize();

            //insomableMethods.SendMSG(new Send_()
            //{
            //    Command = "userip",
            //    Nickname = null,
            //    Login = send.Login,
            //    Password = null,
            //    LastOnline = null,
            //    Status = true,
            //    Image = Encoding.Default.GetBytes("0"),
            //    Port = port,
            //    Id_user = 0,
            //    IP = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString()
            //});

            //List<Send_> friends = new List<Send_>();
            //insomableMethods.SendMSG(new Send_()
            //{
            //    Command = "loadfriends",
            //    Login = send.Login,
            //    Password = null,
            //    Status = true,
            //    LastOnline = null,
            //    Image = Encoding.Default.GetBytes("0"),
            //    Port = port,
            //    Id_user = 0,
            //    IP = null
            //});
            //int friendsCount = Convert.ToInt32(insomableMethods.CatchAMessage(port)); // In the port field we will return the number of friends
            //for (int i = 0; i < friendsCount; i++)
            //    friends.Add(insomableMethods.CatchAMessage_(port));
            //foreach (var friend in friends)
            //    lb_contacts.Items.Add(friend.Login);

            ////lb_contacts.ItemsSource = 
            //Task.Run(() =>
            //{
            //    Message msg = new Message();
            //    msg = insomableMethods.CatchAMessage_2(listener);
            //    Task.Run(() => Update(msg));
            //});
        }

        private void Update(Message msg)
        {
            MessageBox.Show("Message");
        }

        private void Inizialize()
        {
            if (String.IsNullOrEmpty(nickname_))
            {
                tb_login.Text = login_;
                tb_password.Text = password_;
            }
            else
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
                {
                    //port = insomableMethods.FreeTcpPort();
                    //insomableMethods.SendMSG(new Send_()
                    //{
                    //    Command = "checklogin",
                    //    Login = tb_login.Text,
                    //    Password = null,
                    //    Status = true,
                    //    LastOnline = null,
                    //    Image = Encoding.Default.GetBytes("0"),
                    //    Port = port,
                    //    Id_user = 0,
                    //    IP = null
                    //});
                    //if (insomableMethods.CatchAMessage(port) == "yes")
                    //{
                    //    MessageBox.Show("< User with this login already exists >");
                    //    return;
                    //}
                    //else login_ = tb_login.Text;
                }
                if (tb_password.Text != password_)
                    password_ = tb_password.Text;
                if (tb_nickname.Text != nickname_)
                    nickname_ = tb_nickname.Text;
                if (ing) photo_ = img;
            }

            //port = insomableMethods.FreeTcpPort();
            //send.Status = true;
            //send.Port = 0;
            //send.Command = "add";
            //send.LastOnline = DateTime.Now;
            //insomableMethods.SendMSG(send);
            //insomableMethods.SendMSG(new Send_()
            //{
            //    Command = "remove",
            //    Login = lastLogin,
            //    Password = null,
            //    Status = true,
            //    LastOnline = null,
            //    Image = Encoding.Default.GetBytes("0"),
            //    Port = port,
            //    Id_user = 0,
            //    IP = null
            //});
            //MessageBox.Show(id.ToString());
            //insomableMethods.SendMSG(new Send_()
            //{
            //    Command = "getid",
            //    Nickname = null,
            //    Login = send.Login,
            //    Password = null,
            //    Status = true,
            //    LastOnline = DateTime.Now,
            //    Image = Encoding.Default.GetBytes("0"),
            //    Port = port,
            //    Id_user = 0,
            //    IP = null
            //});
            //id = Convert.ToInt32(insomableMethods.CatchAMessage(port));
            //MessageBox.Show(id.ToString());
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
                //if (tb_login.Text != send.Login)
                //{
                //    port = insomableMethods.FreeTcpPort();
                //    insomableMethods.SendMSG(new Send_()
                //    {
                //        Command = "checklogin",
                //        Login = tb_login.Text,
                //        Password = null,
                //        Status = true,
                //        LastOnline = null,
                //        Image = Encoding.Default.GetBytes("0"),
                //        Port = port,
                //        Id_user = 0,
                //        IP = null
                //    });
                //    if (insomableMethods.CatchAMessage(port) == "yes")
                //    {
                //        insomableMethods.SendMSG(new Send_()
                //        {
                //            Command = "addrequest",
                //            Login = send.Login,
                //            Password = tb_login.Text, // We transmit instead of nickname the login of the user who sends a request for friendship
                //            Status = true,
                //            LastOnline = DateTime.Now, // We transmit instead of the last online time sending time
                //            Image = Encoding.Default.GetBytes("0"),
                //            Port = port,
                //            Id_user = 0,
                //            IP = null
                //        });
                //        lb_requests.Items.Add(tb_login.Text);
                //        return;
                //    }
                //    else
                //    {
                //        MessageBox.Show("< There is no user with this login >");
                //        return;
                //    }
                //}
            }
        }

        byte[] img;
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
            img = File.ReadAllBytes(ofd.FileName);
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(img.ToArray());
            var handle = bitmap1.GetHbitmap();
            Avatar.Source = Imaging.CreateBitmapSourceFromHBitmap(handle,
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ing = true;
        }

        enum SELECTION { ACCOUNTINFO = 0, ALLCHATS = 1, CONTACTS = 2 };

        private void I_Profile_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lb_contacts.Visibility = Visibility.Hidden;
            lb_requests.Visibility = Visibility.Hidden;

            tb_login.Text = login_;
            tb_password.Visibility = Visibility.Visible;
            tb_nickname.Visibility = Visibility.Visible;
            Avatar.Visibility = Visibility.Visible;
            Flipper_b.Content = "Save";

            flipper.IsEnabled = true;
            flipper.Visibility = Visibility.Visible;
            lastLogin = login_;
        }

        private void chat_badged_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lb_contacts.Visibility = Visibility.Hidden;
            lb_requests.Visibility = Visibility.Hidden;
            flipper.IsEnabled = false;
            flipper.Visibility = Visibility.Hidden;

            //MessageBox.Show("");
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
