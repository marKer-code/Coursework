namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    using UI.Windows;

    public partial class Profile : Window
    {
        #region Fields

        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_, lastLogin;
        byte[] photo_;
        bool ing = false;

        #endregion

        public Profile(string login, string password, string nickname, byte[] photo)
        {
            InitializeComponent();

            insomable = new InsomableMethods();

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.ReceiveRequestEvent += new Events_CallbackHandler().ReceiveRequest;
            callbackHandler.NewContactEvent += new Events_CallbackHandler().NewContact;
            callbackHandler.RejectRequest_Event += new Events_CallbackHandler().RejectRequest_;
            callbackHandler.DeleteContactEvent += new Events_CallbackHandler().DeleteContact;
            callbackHandler.NewChatEvent += new Events_CallbackHandler().NewChat;

            callbackHandler.ReceiveMessageEvent += new Events_CallbackHandler().ReceiveMessage;
            callbackHandler.DeleteChatEvent += new Events_CallbackHandler().DeleteChat;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            Task.Run(() => LoadInfo(login, password, nickname, photo));
        }

        private void Window_Closed(object sender, EventArgs e)
            => programServiceClient.UpdateOnline(login_, "Remove");

        private void LoadInfo(string login, string password, string nickname, byte[] photo)
        {
            login_ = login;
            password_ = password;
            programServiceClient.UpdateOnline(login, "ChangeCallback");

            switch (nickname)
            {
                case null:
                    {
                        List<byte[]> infoes = programServiceClient.LoadUserInfo(login_);

                        nickname_ = Encoding.Default.GetString(infoes[0]);
                        photo_ = infoes[2];
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Initialize();
                        }));
                        break;
                    }
                default:
                    {
                        photo_ = photo;
                        nickname_ = nickname;

                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Initialize();
                        }));
                        break;
                    }
            }

            lastLogin = login_;
        }

        private void Initialize()
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
            => Close();
    }
}
