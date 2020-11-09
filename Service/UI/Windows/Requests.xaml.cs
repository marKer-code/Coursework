namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    using UI.Windows;

    public partial class Requests : Window
    {
        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_, lastLogin;
        byte[] photo_;

        public Requests(string login, string password, string nickname, byte[] photo)
        {
            InitializeComponent();

            insomable = new InsomableMethods();

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.MessageEvent += GetMessage;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            LoadInfo(login, password, nickname, photo);
        }
        private void GetMessage(string obj)
        {
            MessageBox.Show(obj);
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

                        break;
                    }
                default:
                    {
                        photo_ = photo;
                        nickname_ = nickname;

                        break;
                    }
            }

            lastLogin = login_;

            lb_requests.ItemsSource = Lists.receivedRequests;
            lb_requests_Send.ItemsSource = Lists.sendRequests;
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            programServiceClient.UpdateOnlineAsync(login_, false);
            Close();
        }
    }
}
