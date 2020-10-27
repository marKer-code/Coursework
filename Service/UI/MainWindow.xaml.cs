namespace UI
{
    using System;
    using System.ServiceModel;
    using System.Windows;
    using UI.ServiceReference;

    public class CallbackHandler : IProgramServiceCallback
    {
        public event Action<string> UserExistEvent;

        public void UserExist(string exists)
            => UserExistEvent?.Invoke(exists);
    }

    public partial class MainWindow : Window
    {
        ProgramServiceClient programServiceClient;

        public MainWindow()
        {
            InitializeComponent();

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.UserExistEvent += UserExist;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));

            //programServiceClient.CheckUser("MarKer", "qq4");
        }
        private void UserExist(string exists)
        {
            //MessageBox.Show(exists);
        }
    }
}
