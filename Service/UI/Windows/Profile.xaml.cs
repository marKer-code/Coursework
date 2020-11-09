namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    using UI.Windows;

    public partial class Profile : Window
    {
        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_, lastLogin;
        byte[] photo_;
        bool ing = false;

        public Profile(string login, string password, string nickname, byte[] photo)
        {
            InitializeComponent();

            insomable = new InsomableMethods();

            CallbackHandler callbackHandler = new CallbackHandler();
            callbackHandler.ReceiveRequestEvent += ReceiveRequest;


            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            LoadInfo(login, password, nickname, photo);
        }
        private void ReceiveRequest(string senderLogin)
            => Lists.receivedRequests.Add(senderLogin);

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
        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            programServiceClient.UpdateOnlineAsync(login_, false);
            Close();
        }
    }
}
