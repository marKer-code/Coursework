namespace UI
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    public partial class Contact
    {
        private void ShowContactInfo()
        {
            l_login_r.Visibility = Visibility.Visible;
            l_nickname_r.Visibility = Visibility.Visible;
            l_status_r.Visibility = Visibility.Visible;
            gr_contactInfo.Visibility = Visibility.Visible;

            if (lb_contacts.SelectedItems != null)
            {
                login.Text = lb_contacts.SelectedItem.ToString();

                List<byte[]> infoes = programServiceClient.LoadUserInfo(login.Text);

                if (Encoding.Default.GetString(infoes[3]) == "true")
                    status.Text = Encoding.Default.GetString(infoes[3]);
                else
                    status.Text = Encoding.Default.GetString(infoes[1]);

                nickname.Text = Encoding.Default.GetString(infoes[0]);
                byte[] ph = infoes[2];
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(ph.ToArray());
                var handle = bitmap1.GetHbitmap();
                avatar_fr.Source = Imaging.CreateBitmapSourceFromHBitmap(handle,
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void Lb_contacts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lb_contacts.SelectedItem != null)
            {
                ShowContactInfo();

                lb_contacts.SelectedItem = null;
            }
        }

        private void Bt_remove_fr_Click(object sender, RoutedEventArgs e)
        {
            programServiceClient.RemoveContactAsync(login_, login.Text);
            Lists.contacts.Remove(login.Text);
            gr_contactInfo.Visibility = Visibility.Hidden;

            foreach (var item in Lists.messages)
                if(item.Value == login_)
                    Lists.messages.Remove(item.Key);

            MessageBox.Show("< Removed >");
        }
    }
}
