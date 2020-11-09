using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for Contact.xaml
    /// </summary>
    public partial class Contact : Window
    {
        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_, lastLogin;
        byte[] photo_;
        //bool ing = false;

        public Contact(string login, string password, string nickname, byte[] photo)
        {
            InitializeComponent();

            insomable = new InsomableMethods();

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.MessageEvent += GetMessage;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));


            LoadInfo(login, password, nickname, photo);

            List<int> users = programServiceClient.GetAllContact(login_);
            foreach (var u in users)
                lb_contacts.Items
                    .Add(programServiceClient
                    .GetLoginUserByIdAsync(u)
                    .Result);
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

            programServiceClient.UpdateOnlineAsync(login, true);
            lastLogin = login_;
        }



        private void Window_Closed(object sender, EventArgs e)
            => programServiceClient.UpdateOnlineAsync(login_, false);


        private void GetMessage(string obj)
        {
            MessageBox.Show("");
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
           => Close();

    }
}
