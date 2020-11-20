namespace UI
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    public partial class Chats
    {
        private void ShowContactInfo()
        {

            if (lb_chats.SelectedItems != null)
            {
                login_ct.Visibility = Visibility.Visible;

                login_ct.Text = lb_chats.SelectedItem.ToString();
                List<byte[]> info = new List<byte[]>();

                try
                {
                    info = programServiceClient.LoadUserInfo(login_ct.Text);
                }
                catch { return; }

                l_login_r.Visibility = Visibility.Visible;
                l_nickname_r.Visibility = Visibility.Visible;
                l_status_r.Visibility = Visibility.Visible;
                gr_chatInfo.Visibility = Visibility.Visible;

                nickname_ct.Visibility = Visibility.Visible;
                status_ct.Visibility = Visibility.Visible;

                avatar_ct.Visibility = Visibility.Visible;

                if (Encoding.Default.GetString(info[3]) == "True")
                    status_ct.Text = "Online";
                else
                    status_ct.Text = Encoding.Default.GetString(info[1]);

                nickname_ct.Text = Encoding.Default.GetString(info[0]);
                byte[] ph = info[2];
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(ph.ToArray());
                var handle = bitmap1.GetHbitmap();
                avatar_ct.Source = Imaging.CreateBitmapSourceFromHBitmap(handle,
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void Bt_add_ct_Click(object sender, RoutedEventArgs e)
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

        private void Bt_Plus_Click(object sender, RoutedEventArgs e)
        {
            if (flipper_attachFile.Visibility == Visibility.Visible)
            {
                flipper_attachFile.IsEnabled = false;
                flipper_attachFile.Visibility = Visibility.Collapsed;

                chat_lb.Visibility = Visibility.Visible;

                sp_chats.IsEnabled = true;

                lb_attachFile.Items.Clear();

                bt_remove_ct.IsEnabled = true;
                bt_send.IsEnabled = true;
                tb_message.IsEnabled = true;
            }
            else
            {
                flipper_attachFile.IsEnabled = true;
                flipper_attachFile.Visibility = Visibility.Visible;

                chat_lb.Visibility = Visibility.Hidden;

                sp_chats.IsEnabled = false;

                bt_remove_ct.IsEnabled = false;
                bt_send.IsEnabled = false;
                tb_message.IsEnabled = false;

                f_bt_add.IsEnabled = true;

                flipper.IsEnabled = false;
                flipper.Visibility = Visibility.Collapsed;
            }
        }

        private void Flipper_b_Click(object sender, RoutedEventArgs e)
        {
            if (cb_contact.SelectedIndex != -1)
            {
                programServiceClient.AddChat(login_, cb_contact.SelectedItem.ToString());
                Lists.chats.Add(cb_contact.SelectedItem.ToString());
                Lists.noChat.Remove(cb_contact.SelectedItem.ToString());
            }
        }

        private void Bt_send_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tb_message.Text))
            {
                programServiceClient.SendMessageAsync(login_, login_ct.Text, tb_message.Text, null, null);
                Lists.messages.Add(new List<string>()
                {
                    programServiceClient.GetId(login_).ToString(),
                    programServiceClient.GetId(login_ct.Text).ToString(),
                    tb_message.Text
                },
                login_ct.Text);
                chat_lb.Items.Clear();

                foreach (var item in Lists.messages)
                    if (item.Value == login_ct.Text)
                    {
                        List<string> r = item.Key;
                        chat_lb.Items.Add(r[2]);
                    }
            }
            else MessageBox.Show("< Enter Message >");
        }

        private void Bt_remove_ct_Click(object sender, RoutedEventArgs e)
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

                        chat_lb.Visibility = Visibility.Hidden;

                        foreach (var item in Lists.messages)
                            if (item.Value == login_)
                                Lists.messages.Remove(item.Key);

                        break;
                    }
            }
        }

        private void chat_lb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (chat_lb.SelectedItem != null)
                    if (chat_lb.SelectedItem.ToString().Contains(">"))
                    {
                        var res = MessageBox.Show("Download File", " ", MessageBoxButton.OKCancel);
                        if (res == MessageBoxResult.OK)
                        {
                            DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                            string q = chat_lb.SelectedItem.ToString().Split(' ').Last();
                            foreach (var item in Lists.messages)
                            {
                                if (item.Value == Lists.chatOn &&
                                    item.Key[2] == chat_lb.SelectedItem.ToString())
                                {
                                    MessageBox.Show(item.Key[3]);
                                    using (FileStream fs = new FileStream(dir + "\\" + q, FileMode.Create))
                                        fs.Write(Encoding.Default.GetBytes(item.Key[3]), 0, Encoding.Default.GetBytes(item.Key[3]).Length);
                                }
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            chat_lb.SelectedItem = null;
        }
        private void Lb_chats_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lb_chats.SelectedItem != null)
            {
                Lists.chatOn = lb_chats.SelectedItem.ToString();

                chat_lb.Items.Clear();
                chat_lb.Visibility = Visibility.Visible;
                gr_chatInfo.Visibility = Visibility.Visible;

                flipper.Visibility = Visibility.Collapsed;

                bt_remove_ct.Visibility = Visibility.Visible;

                bt_remove_ct.IsEnabled = true;
                bt_Plus.IsEnabled = true;
                bt_send.IsEnabled = true;

                ShowContactInfo();

                foreach (var item in Lists.messages)
                    if (item.Value == lb_chats.SelectedItem.ToString())
                    {
                        List<string> r = item.Key;
                        chat_lb.Items.Add(r[2]);
                    }
                lb_chats.SelectedItem = null;
                return;
            }
        }
        private void f_bt_add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                //Title = "Browse photo",

                CheckFileExists = true,
                CheckPathExists = true,

                //DefaultExt = "jpg",
                //Filter = "jpg (*.jpg)|*.jpg|png (*.png)|*.png",
                //FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (ofd.ShowDialog() == true)
            {
                fName = Path.GetFileName(ofd.FileName);
                lb_attachFile.Items.Add(Path.GetFileName(ofd.FileName));
                SetFile(ofd);
                f_bt_add.IsEnabled = false;
            }
        }

        byte[] file = null;

        private void SetFile(OpenFileDialog ofd)
        {
            file = File.ReadAllBytes(ofd.FileName);
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(photo_.ToArray());
            //var handle = bitmap1.GetHbitmap();
        }

        private void f_bt_send_Click(object sender, RoutedEventArgs e)
        {
            programServiceClient.SendMessageAsync(login_, login_ct.Text, tb_message.Text, fName, file);
            foreach (var item in lb_attachFile.Items)
            {
                Lists.messages.Add(new List<string>()
                    {
                    programServiceClient.GetId(login_).ToString(),
                    programServiceClient.GetId(login_ct.Text).ToString(),
                    "File > " + item.ToString()
                    }, login_ct.Text);
            }
            chat_lb.Items.Clear();

            foreach (var item in Lists.messages)
                if (item.Value == login_ct.Text)
                {
                    List<string> r = item.Key;
                    chat_lb.Items.Add(r[2]);
                }
            lb_attachFile.Items.Clear();
            flipper_attachFile.Visibility = Visibility.Collapsed;
            Bt_Plus_Click(sender, e);
        }
    }
}
