namespace UI
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

    public partial class Profile
    {
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

            programServiceClient.SaveUserInfoAsync(lastLogin,
                login_, nickname_, password_, photo_);

            MessageBox.Show("< Saved >");
        }

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

        private void Flipper_b_Click(object sender, RoutedEventArgs e)
            => SaveAcc();
    }
}
