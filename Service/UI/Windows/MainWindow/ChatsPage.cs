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

            login_ct.Visibility = Visibility.Visible;
            nickname_ct.Visibility = Visibility.Visible;
            status_ct.Visibility = Visibility.Visible;
            avatar_ct.Visibility = Visibility.Visible;

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

        private void bt_add_ct_Click(object sender, RoutedEventArgs e)
        {
            if (flipper.Visibility == Visibility.Visible)
            {
                flipper.IsEnabled = false;
                flipper.Visibility = Visibility.Collapsed;
            }
            else
            {
                flipper.IsEnabled = true;
                flipper.Visibility = Visibility.Visible;

                gr_chatInfo.Visibility = Visibility.Hidden;
                avatar_ct.Visibility = Visibility.Hidden;
                login_ct.Visibility = Visibility.Hidden;
                nickname_ct.Visibility = Visibility.Hidden;
                status_ct.Visibility = Visibility.Hidden;
                bt_remove_ct.Visibility = Visibility.Hidden;
            }
        }

        private void bt_Plus_Click(object sender, RoutedEventArgs e)
        {
            if (flipper_attachFile.Visibility == Visibility.Visible)
            {
                flipper_attachFile.IsEnabled = false;
                flipper_attachFile.Visibility = Visibility.Collapsed;

                sp_chats.IsEnabled = true;

                bt_remove_ct.IsEnabled = true;
                bt_send.IsEnabled = true;
                tb_message.IsEnabled = true;
            }
            else
            {
                flipper_attachFile.IsEnabled = true;
                flipper_attachFile.Visibility = Visibility.Visible;

                sp_chats.IsEnabled = false;

                bt_remove_ct.IsEnabled = false;
                bt_send.IsEnabled = false;
                tb_message.IsEnabled = false;

                flipper.IsEnabled = false;
                flipper.Visibility = Visibility.Collapsed;
            }
        }

        private void Flipper_b_Click(object sender, RoutedEventArgs e)
        {
            if (cb_contact.SelectedIndex != -1)
            {
                MessageBox.Show(cb_contact.SelectedItem.ToString());
                programServiceClient.AddChat(login_, cb_contact.SelectedItem.ToString());
                Lists.chats.Add(cb_contact.SelectedItem.ToString());
                Lists.noChat.Remove(cb_contact.SelectedItem.ToString());
            }
        }

        private void bt_remove_ct_Click(object sender, RoutedEventArgs e)
        {
            switch (login_ct.Text)
            {
                case null:
                    {
                        MessageBox.Show("< Select Request >");
                        break;
                    }
                default:
                    {
                        Lists.chats.Remove(login_ct.Text);
                        programServiceClient.RemoveChatAsync(login_, login_ct.Text);
                        break;
                    }
            }
        }




        private void Lb_chats_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lb_chats.SelectedItem != null)
            {
                chat_lb.Items.Clear();
                gr_chatInfo.Visibility = Visibility.Visible;
                chat_lb.Visibility = Visibility.Visible;

                ShowContactInfo();

                foreach (var item in Lists.messages)
                {
                    if (item.Value == lb_chats.SelectedItem.ToString())
                    {
                        List<string> r = item.Key;
                        chat_lb.Items.Add(r[2]);
                    }
                }
                lb_chats.SelectedItem = null;
            }
        }
    }
}
