﻿namespace UI.Windows.MainWindow
{
    using DAL.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    using UI.Windows;

    public partial class Main : Window
    {
        #region Fields

        private enum SELECTION { ACCOUNTINFO = 0, ALLCHATS = 1, CONTACTS = 2 };
        private enum BUTTON { PROFILE = 0, CHATS = 1, ALLCONTACTS = 2, ADDFRIENDS = 3 };

        readonly ProgramServiceClient programServiceClient;
        readonly IInsomableMethods insomable;

        string login_, password_, nickname_;
        byte[] photo_;

        #endregion

        public Main(string login, string password, string nickname, byte[] photo)
        {
            InitializeComponent();

            insomable = new InsomableMethods();

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.ReceiveRequestEvent += new Events_CallbackHandler().ReceiveRequest;
            callbackHandler.NewContactEvent += new Events_CallbackHandler().NewContact;
            callbackHandler.RejectRequest_Event += new Events_CallbackHandler().RejectRequest_;
            callbackHandler.DeleteContactEvent += new Events_CallbackHandler().DeleteContact;
            callbackHandler.NewChatEvent += new Events_CallbackHandler().NewChat;
            callbackHandler.DeleteChatEvent += new Events_CallbackHandler().DeleteChat;
            callbackHandler.ReceiveMessageEvent += new Events_CallbackHandler().ReceiveMessage;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            if (!programServiceClient.CheckLogin(login))
                insomable.OpenWindow(new SignUp(), this);

            Task.Run(() => LoadInfo(login, password, nickname, photo));
        }

        private void Window_Closed(object sender, System.EventArgs e)
            => programServiceClient.UpdateOnline(login_, "Remove");

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

            foreach (var u in programServiceClient.GetAllContact(login_))
                Lists.contacts
                     .Add(programServiceClient
                     .GetLoginUserByIdAsync(u)
                     .Result);

            foreach (var request in programServiceClient.GetAllRequests(login_, false))
                Lists.receivedRequests
                    .Add(programServiceClient
                    .GetLoginUserByIdAsync(request.SenderId)
                    .Result);

            foreach (var request in programServiceClient.GetAllRequests(login_, true))
                Lists.sendRequests
                    .Add(programServiceClient
                    .GetLoginUserByIdAsync(request.ReceiverId)
                    .Result);

            foreach (var noChat in programServiceClient.GetNoChat(login))
                Lists.noChat
                    .Add(programServiceClient
                    .GetLoginUserByIdAsync(noChat)
                    .Result);

            int id = programServiceClient.GetId(login_);

            List<Message> messages = programServiceClient.GetAllChats(login);

            foreach (var message in messages)
            {
                if (message.ReceiverId != id &&
                    !Lists.chats.Contains(
                        programServiceClient.GetLoginUserById(message.ReceiverId)))
                    Lists.chats
                        .Add(programServiceClient
                        .GetLoginUserByIdAsync(message.ReceiverId)
                        .Result);
                else if (!Lists.chats.Contains(
                        programServiceClient.GetLoginUserById(message.SenderId)))
                    Lists.chats
                        .Add(programServiceClient
                        .GetLoginUserByIdAsync(message.SenderId)
                        .Result);
            }

            foreach (var item in Lists.chats)
            {
                if (item == login_)
                    continue;

                List<Message> messages_ =
                    messages
                        .Where(m => m.SenderId == programServiceClient.GetId(item) ||
                            m.ReceiverId == programServiceClient.GetId(item))
                        .ToList();

                foreach (var item2 in messages_)
                {
                    List<string> q = new List<string>()
                    {
                        item2.SenderId.ToString(),
                        item2.ReceiverId.ToString(),
                        item2.Text
                    };

                    Lists.messages.Add(q, item);
                }
            }
        }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => Close();
    }
}
