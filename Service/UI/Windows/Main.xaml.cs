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
    using System.Text;
    using System.Threading;
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

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.MessageEvent += GetMessage;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

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
                List<byte[]> infoes = programServiceClient.LoadUserInfo(login_);

                nickname_ = Encoding.Default.GetString(infoes[0]);
                photo_ = infoes[2];

                Inizialize();
            }

            List<int> users = programServiceClient.GetAllContact(login_);

            foreach (var u in users)
            {
                lb_contacts.Items.Add(programServiceClient.GetLoginUserByIdAsync(u).Result);
            }

            foreach (var request in programServiceClient.GetAllRequests(login_, false))
                lb_requests.Items.Add(programServiceClient.GetLoginUserByIdAsync(request.SenderId).Result);
            foreach (var request in programServiceClient.GetAllRequests(login_, true))
                lb_requests_Send.Items.Add(programServiceClient.GetLoginUserByIdAsync(request.ReceiverId).Result);

            programServiceClient.UpdateOnlineAsync(login, true);
            lastLogin = login_;
        }

        private void GetMessage(string obj)
        {
            MessageBox.Show("");
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

            if (tb_login.Text != login_)
            {
                if (programServiceClient.CheckLogin(tb_login.Text))
                {
                    MessageBox.Show("< Login already busy >");
                    return;
                }
                lastLogin = login_;
                login_ = tb_login.Text;
            }
            if (tb_password.Text != password_)
                password_ = tb_password.Text;
            if (tb_nickname.Text != nickname_)
                nickname_ = tb_nickname.Text;

            if (ing)
                programServiceClient.SaveUserPhotoAsync(lastLogin, photo_);
            else
                programServiceClient.SaveUserInfoAsync(lastLogin,
                    login_, nickname_, password_, photo_);

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

            if (programServiceClient.CheckLogin(tb_login.Text))
            {
                if (!lb_requests_Send.Items.Contains(tb_login.Text))
                {
                    programServiceClient.AddRequestAsync(login_, tb_login.Text);
                    lb_requests_Send.Items.Add(tb_login.Text);
                }
                else MessageBox.Show("< Request already send >");
            }
            else MessageBox.Show("< No user with such login >");
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

            ing = true;
        }

        enum SELECTION { ACCOUNTINFO = 0, ALLCHATS = 1, CONTACTS = 2 };
        enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        private void Hiden(BUTTON btn)
        {
            switch (btn)
            {
                case BUTTON.PROFILE:
                    {
                        if (flipper.Visibility == Visibility.Visible && Avatar.Visibility == Visibility.Visible)
                            hiddenElement();
                        else
                        {
                            hiddenElement();
                            tb_login.Text = login_;
                            Flipper_b.Content = "Save";

                            l_nicname.Visibility = Visibility.Visible;
                            l_pass.Visibility = Visibility.Visible;

                            gr_requestInfo.Visibility = Visibility.Hidden;

                            lb_requests.Visibility = Visibility.Hidden;

                            tb_password.Visibility = Visibility.Visible;
                            tb_nickname.Visibility = Visibility.Visible;
                            Avatar.Visibility = Visibility.Visible;
                            hiddenElement_();
                        }
                        break;
                    }
                case BUTTON.CHATS:
                    {
                        hiddenElement();
                        lb_chats.Visibility = Visibility.Visible;
                        break;
                    }
                case BUTTON.ALLCONTACTS:
                    {
                        hiddenElement();
                        hidenChatInfo();
                        lb_contacts.Visibility = Visibility.Visible;
                        lb_requests.Visibility = Visibility.Hidden;
                        gr_requestInfo.Visibility = Visibility.Hidden;

                        flipper.IsEnabled = false;
                        flipper.Visibility = Visibility.Hidden;

                        break;
                    }
                case BUTTON.ADDFRIENDS:
                    {
                        if (flipper.Visibility == Visibility.Visible && Avatar.Visibility == Visibility.Hidden)
                            hiddenElement();
                        else
                        {
                            hiddenReqestsInfo();

                            tb_login.Text = null;
                            Flipper_b.Content = "Search";

                            l_receipt.Visibility = Visibility.Visible;
                            l_send.Visibility = Visibility.Visible;

                            l_nicname.Visibility = Visibility.Hidden;
                            l_pass.Visibility = Visibility.Hidden;

                            sp_Requests.Visibility = Visibility.Visible;

                            lb_requests.Visibility = Visibility.Visible;
                            lb_requests_Send.Visibility = Visibility.Visible;
                            gr_requestInfo.Visibility = Visibility.Visible;

                            tb_password.Visibility = Visibility.Hidden;
                            tb_nickname.Visibility = Visibility.Hidden;
                            Avatar.Visibility = Visibility.Hidden;
                            hiddenElement_();
                        }
                        break;
                    }
            }
        }

        private void hiddenElement()
        {
            lb_requests.ItemsSource = null;
            hidenContactInfo();
            hidenChatInfo();
            lb_contacts.Visibility = Visibility.Hidden;

            sp_Requests.Visibility = Visibility.Hidden;
            hiddenReqestsInfo();

            l_receipt.Visibility = Visibility.Hidden;
            l_send.Visibility = Visibility.Hidden;

            lb_requests.Visibility = Visibility.Hidden;
            gr_requestInfo.Visibility = Visibility.Hidden;
            flipper.IsEnabled = false;
            flipper.Visibility = Visibility.Collapsed;
        }

        private void hiddenElement_()
        {
            hidenContactInfo();
            hidenChatInfo();
            lastLogin = login_;
            lb_contacts.Visibility = Visibility.Hidden;
            flipper.IsEnabled = true;
            flipper.Visibility = Visibility.Visible;

            lb_chats.Visibility = Visibility.Hidden;
        }
        private void hidenContactInfo()
        {
            gr_contactInfo.Visibility = Visibility.Hidden;
            bt_remove_fr.Visibility = Visibility.Hidden;
            avatar_fr.Visibility = Visibility.Hidden;

            nickname.Visibility = Visibility.Hidden;
            login.Visibility = Visibility.Hidden;
            status.Visibility = Visibility.Hidden;

            l_login.Visibility = Visibility.Hidden;
            l_nickname.Visibility = Visibility.Hidden;
            l_status.Visibility = Visibility.Hidden;

            lb_contacts.SelectedItem = 0;
        }

        private void hidenChatInfo()
        {
            gr_chatInfo.Visibility = Visibility.Hidden;
            chat_lb.Visibility = Visibility.Hidden;
        }

        private void hiddenReqestsInfo()
        {
            gr_requestInfo.Visibility = Visibility.Hidden;
            bt_accept_r.Visibility = Visibility.Hidden;
            bt_reject_r.Visibility = Visibility.Hidden;
            avatar_r.Visibility = Visibility.Hidden;

            nickname_r.Visibility = Visibility.Hidden;
            login_r.Visibility = Visibility.Hidden;
            status_r.Visibility = Visibility.Hidden;

            l_login_r.Visibility = Visibility.Hidden;
            l_nickname_r.Visibility = Visibility.Hidden;
            l_status_r.Visibility = Visibility.Hidden;

            lb_requests.SelectedItem = 0;
            lb_requests_Send.SelectedItem = 0;
        }

        private void I_Profile_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => Hiden(BUTTON.PROFILE);

        private void chat_badged_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Hiden(BUTTON.CHATS);
            programServiceClient.UpdateOnline(login_, false);
        }

        private void allContacts_badget_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => Hiden(BUTTON.ALLCONTACTS);

        private void addFriend_badget_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => Hiden(BUTTON.ADDFRIENDS);


        private void ShowInfoRequests(System.Windows.Controls.ListBox listBox)
        {
            hidenChatInfo();
            hidenContactInfo();
            sp_Requests.Visibility = Visibility.Visible;
            gr_requestInfo.Visibility = Visibility.Visible;
            bt_reject_r.Visibility = Visibility.Visible;
            bt_accept_r.Visibility = Visibility.Visible;

            l_login_r.Visibility = Visibility.Visible;
            l_nickname_r.Visibility = Visibility.Visible;
            l_status_r.Visibility = Visibility.Visible;

            nickname_r.Visibility = Visibility.Visible;
            login_r.Visibility = Visibility.Visible;
            status_r.Visibility = Visibility.Visible;


            avatar_r.Visibility = Visibility.Visible;
            flipper.IsEnabled = false;
            flipper.Visibility = Visibility.Collapsed;

            if (listBox.SelectedItems != null)
            {
                login_r.Text = listBox.SelectedItem.ToString();

                List<byte[]> infoes = programServiceClient.LoadUserInfo(login_r.Text);

                if (Encoding.Default.GetString(infoes[3]) == "true")
                    status_r.Text = Encoding.Default.GetString(infoes[3]);
                else
                    status_r.Text = Encoding.Default.GetString(infoes[1]);

                nickname_r.Text = Encoding.Default.GetString(infoes[0]);
                byte[] ph = infoes[2];
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(ph.ToArray());
                var handle = bitmap1.GetHbitmap();
                avatar_r.Source = Imaging.CreateBitmapSourceFromHBitmap(handle,
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void lb_requests_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lb_requests.SelectedItem != null)
            {
                bt_accept_r.IsEnabled = true;
                bt_reject_r.IsEnabled = true;

                ShowInfoRequests(lb_requests);

                lb_requests.SelectedItem = null;
            }
        }


        private void lb_requests_Send_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lb_requests_Send.SelectedItem != null)
            {
                bt_accept_r.IsEnabled = false;
                bt_reject_r.IsEnabled = false;

                ShowInfoRequests(lb_requests_Send);

                lb_requests_Send.SelectedItem = null;
            }
        }


        private void bt_accept_r_Click(object sender, RoutedEventArgs e)
        {
            if (login_r.Text != null)
                programServiceClient.AceptRequestAsync(login_r.Text, login_);
            else MessageBox.Show("< Select Request >");
        }

        private void bt_reject_r_Click(object sender, RoutedEventArgs e)
        {
            if (login_r.Text != null)
                programServiceClient.RejectRequestAsync(login_r.Text, login_);
            else MessageBox.Show("< Select Request >");
        }

        private void lb_chats_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            hiddenReqestsInfo();
            hidenContactInfo();
            gr_chatInfo.Visibility = Visibility.Visible;
            chat_lb.Visibility = Visibility.Visible;
        }

        private void bt_remove_fr_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text != null)
                programServiceClient.RemoveContactAsync(login.Text);
            else MessageBox.Show("< Select Request >");
        }

        private void ShowContactInfo()
        {
            hidenChatInfo();
            hidenContactInfo();

            gr_contactInfo.Visibility = Visibility.Visible;
            bt_remove_fr.Visibility = Visibility.Visible;

            l_login.Visibility = Visibility.Visible;
            l_nickname.Visibility = Visibility.Visible;
            l_status.Visibility = Visibility.Visible;

            nickname.Visibility = Visibility.Visible;
            login.Visibility = Visibility.Visible;
            status.Visibility = Visibility.Visible;


            avatar_fr.Visibility = Visibility.Visible;
            flipper.IsEnabled = false;
            flipper.Visibility = Visibility.Collapsed;

            if (lb_contacts.SelectedItems != null)
            {
                login.Text = lb_contacts.SelectedItem.ToString();

                List<byte[]> infoes = programServiceClient.LoadUserInfo(login.Text);

                if (Encoding.Default.GetString(infoes[3]) == "true")
                    status.Text = Encoding.Default.GetString(infoes[3]);
                else
                    status.Text = Encoding.Default.GetString(infoes[1]);

                nickname.Text = Encoding.Default.GetString(infoes[0]);
                byte[] ph = infoes[2];
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(ph.ToArray());
                var handle = bitmap1.GetHbitmap();
                avatar_fr.Source = Imaging.CreateBitmapSourceFromHBitmap(handle,
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void lb_contacts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lb_contacts.SelectedItem != null)
            {
                ShowContactInfo();

                lb_contacts.SelectedItem = null;
            }
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => Close();

    }
}
