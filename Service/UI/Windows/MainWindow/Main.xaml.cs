namespace UI.Windows.MainWindow
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using UI.InsomableMethods_;
    using UI.ServiceReference;

    public partial class Main : Window
    {
        #region Fields

        private enum SELECTION { ACCOUNTINFO = 0, ALLCHATS = 1, CONTACTS = 2 };
        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_, lastLogin;
        byte[] photo_;
        bool ing = false;

        #endregion

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

            LoadInfo(login, password, nickname, photo);
        }

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

                        Inizialize();
                        break;
                    }
                default:
                    {
                        photo_ = photo;
                        nickname_ = nickname;

                        Inizialize();
                        break;
                    }
            }

            List<int> users = programServiceClient.GetAllContact(login_);
            foreach (var u in users)
                lb_contacts.Items
                    .Add(programServiceClient
                    .GetLoginUserByIdAsync(u)
                    .Result);

            foreach (var request in programServiceClient.GetAllRequests(login_, false))
                lb_requests.Items
                    .Add(programServiceClient
                    .GetLoginUserByIdAsync(request.SenderId)
                    .Result);
            foreach (var request in programServiceClient.GetAllRequests(login_, true))
                lb_requests_Send.Items
                    .Add(programServiceClient
                    .GetLoginUserByIdAsync(request.ReceiverId)
                    .Result);

            programServiceClient.UpdateOnlineAsync(login, true);
            lastLogin = login_;
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


        private void Window_Closed(object sender, EventArgs e)
            => programServiceClient.UpdateOnlineAsync(login_, false);


        private void GetMessage(string obj)
        {
            MessageBox.Show("");
        }


        private void Flipper_b_Click(object sender, RoutedEventArgs e)
        {
            switch (Flipper_b.Content)
            {
                case "Save":
                    {
                        SaveAcc();
                        break;
                    }
                default:
                    {
                        Search();
                        break;
                    }
            }
        }
    }
}
