namespace UI.Windows
{
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using System.ServiceModel;
    using UI.InsomableMethods_;
    using UI.ServiceReference;
    public partial class SignUp : Window
    {
        ProgramServiceClient programServiceClient;

        bool passwordBoxActive = true;
        IInsomableMethods insomable;

        bool ing = false;
        string path = "../../Windows/Icons/Me.jpg";
        byte[] img;

        public SignUp()
        {
            InitializeComponent();

            insomable = new InsomableMethods();
            Tb_Password.Visibility = Visibility.Hidden;

            CallbackHandler callbackHandler = new CallbackHandler();

            callbackHandler.UserExistEvent += UserExist;
            callbackHandler.LoginExistEvent += LoginExist;

            programServiceClient = new ProgramServiceClient
                (new InstanceContext(callbackHandler));
        }

        string loginToAdd, passwordToAdd, nicknameToAdd;
        private void LoginExist(string exists)
        {
            if (!Convert.ToBoolean(exists))
            {
                programServiceClient.AddUserAsync(loginToAdd,
                    nicknameToAdd, passwordToAdd, img, true, DateTime.Now);
                // the main window open
                MessageBox.Show("Login up");
            }
            else MessageBox.Show("< Login already busy >");
        }

        private void UserExist(string obj) { }

        private void B_Close_MouseDown(object sender, MouseButtonEventArgs e)
            => insomable.OpenWindow(new Initial(), this);

        private void B_Ruby_MouseDown(object sender, MouseButtonEventArgs e)
            => insomable.PasswordAppearance(Tb_Password, Pb_Password, ref passwordBoxActive);

        private void B_AddPhoto_MouseDown(object sender, MouseButtonEventArgs e)
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
            {
                SetPhoto(ofd);

                path = ofd.FileName;
            }
        }

        private void SetPhoto(OpenFileDialog ofd)
        {
            img = File.ReadAllBytes(ofd.FileName);
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(img.ToArray());
            var handle = bitmap1.GetHbitmap();
            i_photo.Source = Imaging.CreateBitmapSourceFromHBitmap(handle,
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ing = true;
            path = ofd.FileName;
        }

        private void B_SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (Tb_Login.Text.Contains(' '))
            {
                MessageBox.Show("Login mustn't include spacebar char", "< Error >",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Tb_Login.Text.Length < 2)
            {
                MessageBox.Show("< Short login >");
                return;
            }
            if (passwordBoxActive)
                passwordToAdd = Pb_Password.Password;
            else
                passwordToAdd = Tb_Password.Text;
            if (passwordToAdd.Length < 8)
            {
                MessageBox.Show("< Short password >");
                passwordToAdd = null;
                return;
            }
            if (Tb_Nickname.Text.Length < 2)
            {
                MessageBox.Show("< Short nickname >");
                return;
            }

            if (!String.IsNullOrWhiteSpace(Tb_Login.Text) &&
                !String.IsNullOrWhiteSpace(Tb_Nickname.Text) &&
                !String.IsNullOrWhiteSpace(passwordToAdd))
            {
                loginToAdd = Tb_Login.Text;
                nicknameToAdd = Tb_Nickname.Text;

                if (!ing) img = File.ReadAllBytes(path);

                byte[] fileData;
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    if (!ing) img = File.ReadAllBytes(path);

                    fileData = new byte[fs.Length];
                    fs.Read(fileData, 0, fileData.Length);
                }
                programServiceClient.CheckLoginAsync(loginToAdd);
            }
            else
            {
                MessageBox.Show("< Enter all data >");
                passwordToAdd = null;
            }
        }
    }
}
