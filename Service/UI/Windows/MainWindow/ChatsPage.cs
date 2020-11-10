namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    public partial class Chats
    {
        private void ShowContactInfo()
        {
            l_login_r.Visibility = Visibility.Visible;
            l_nickname_r.Visibility = Visibility.Visible;
            l_status_r.Visibility = Visibility.Visible;
            gr_chatInfo.Visibility = Visibility.Visible;

            if (lb_chats.SelectedItems != null)
            {
                login_ct.Text = lb_chats.SelectedItem.ToString();

                List<byte[]> infoes = programServiceClient.LoadUserInfo(login_ct.Text);

                if (Encoding.Default.GetString(infoes[3]) == "true")
                    status_ct.Text = Encoding.Default.GetString(infoes[3]);
                else
                    status_ct.Text = Encoding.Default.GetString(infoes[1]);

                nickname_ct.Text = Encoding.Default.GetString(infoes[0]);
                byte[] ph = infoes[2];
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(ph.ToArray());
                var handle = bitmap1.GetHbitmap();
                avatar_ct.Source = Imaging.CreateBitmapSourceFromHBitmap(handle,
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void Lb_chats_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lb_chats.SelectedItem != null)
            {
                gr_chatInfo.Visibility = Visibility.Visible;
                chat_lb.Visibility = Visibility.Visible;

                ShowContactInfo();

                lb_chats.SelectedItem = null;
            }
        }
    }
}
