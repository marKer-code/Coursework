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

    public partial class Requests
    {
        private void Search()
        {
            if (Lists.contacts.Contains(tb_login.Text))
            {
                MessageBox.Show("< You already have such a friend >");
                return;
            }
            if (tb_login.Text == lastLogin)
            {
                MessageBox.Show("< You can't add yourself  >");
                return;
            }
            if (tb_login.Text.Length < 2)
            {
                MessageBox.Show("< There is no user with this login >");
                return;
            }

            switch (programServiceClient.CheckLogin(tb_login.Text).ToString())
            {
                case "True":
                    {
                        switch (Lists.sendRequests.Contains(tb_login.Text).ToString())
                        {
                            case "False":
                                {
                                    Lists.sendRequests.Add(tb_login.Text);
                                    programServiceClient.AddRequestAsync(login_, tb_login.Text);
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show("< Request already send >");
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        MessageBox.Show("< No user with such login >");
                        break;
                    }
            }
        }

        private void ShowInfoRequests(System.Windows.Controls.ListBox listBox)
        {
            sp_Requests.Visibility = Visibility.Visible;
            gr_requestInfo.Visibility = Visibility.Visible;
            bt_reject_r.Visibility = Visibility.Visible;
            bt_accept_r.Visibility = Visibility.Visible;

            l_login_r.Visibility = Visibility.Visible;
            l_nickname_r.Visibility = Visibility.Visible;
            l_status_r.Visibility = Visibility.Visible;

            nickname_r.Visibility = Visibility.Visible;
            login_r.Visibility = Visibility.Visible;
            status_r.Visibility = Visibility.Visible;

            avatar_r.Visibility = Visibility.Visible;
            flipper.IsEnabled = false;
            flipper.Visibility = Visibility.Collapsed;

            if (listBox.SelectedItems != null)
            {
                login_r.Text = listBox.SelectedItem.ToString();

                List<byte[]> infoes = programServiceClient.LoadUserInfo(login_r.Text);

                switch (Encoding.Default.GetString(infoes[3]))
                {
                    case "True":
                        {
                            status_r.Text = "Online";
                            break;
                        }
                    default:
                        {
                            status_r.Text = Encoding.Default.GetString(infoes[1]);
                            break;
                        }
                }

                nickname_r.Text = Encoding.Default.GetString(infoes[0]);
                byte[] ph = infoes[2];
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(ph.ToArray());
                var handle = bitmap1.GetHbitmap();
                avatar_r.Source = Imaging.CreateBitmapSourceFromHBitmap(handle,
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void Lb_requests_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lb_requests.SelectedItem != null)
            {
                bt_accept_r.IsEnabled = true;
                bt_reject_r.IsEnabled = true;

                ShowInfoRequests(lb_requests);

                lb_requests.SelectedItem = null;
            }
        }

        private void Lb_requests_Send_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lb_requests_Send.SelectedItem != null)
            {
                bt_accept_r.IsEnabled = false;
                bt_reject_r.IsEnabled = false;

                ShowInfoRequests(lb_requests_Send);

                lb_requests_Send.SelectedItem = null;
            }
        }

        private void Bt_accept_r_Click(object sender, RoutedEventArgs e)
        {
            switch (login_r.Text)
            {
                case null:
                    {
                        MessageBox.Show("< Select Request >");
                        break;
                    }
                default:
                    {
                        Lists.receivedRequests.Remove(login_r.Text);
                        Lists.contacts.Add(login_r.Text);
                        Lists.noChat.Add(login_r.Text);
                        programServiceClient.AcceptRequestAsync(login_r.Text, login_);
                        break;
                    }
            }
        }

        private void Bt_reject_r_Click(object sender, RoutedEventArgs e)
        {
            switch (login_r.Text)
            {
                case null:
                    {
                        MessageBox.Show("< Select Request >");
                        break;
                    }
                default:
                    {
                        programServiceClient.RejectRequestAsync(login_r.Text, login_);
                        Lists.receivedRequests.Remove(login_r.Text);
                        break;
                    }
            }
        }

        private void Flipper_b_Click(object sender, RoutedEventArgs e)
            => Search();
    }
}
