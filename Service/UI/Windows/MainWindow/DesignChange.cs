namespace UI.Windows.MainWindow
{
    using System.Windows;

    public partial class Main
    {
        private void Hiden(BUTTON btn)
        {
            switch (btn)
            {
                case BUTTON.PROFILE:
                    {
                        if (flipper.Visibility == Visibility.Visible &&
                            Avatar.Visibility == Visibility.Visible)
                            HiddenElement();
                        else
                        {
                            HiddenElement();
                            tb_login.Text = login_;
                            Flipper_b.Content = "Save";

                            l_nicname.Visibility = Visibility.Visible;
                            l_pass.Visibility = Visibility.Visible;

                            gr_requestInfo.Visibility = Visibility.Hidden;

                            lb_requests.Visibility = Visibility.Hidden;

                            tb_password.Visibility = Visibility.Visible;
                            tb_nickname.Visibility = Visibility.Visible;
                            Avatar.Visibility = Visibility.Visible;
                            HiddenElement_();
                        }
                        break;
                    }
                case BUTTON.CHATS:
                    {
                        HiddenElement();
                        lb_chats.Visibility = Visibility.Visible;
                        break;
                    }
                case BUTTON.ALLCONTACTS:
                    {
                        HiddenElement();
                        HidenChatInfo();
                        lb_contacts.Visibility = Visibility.Visible;
                        lb_requests.Visibility = Visibility.Hidden;
                        gr_requestInfo.Visibility = Visibility.Hidden;

                        flipper.IsEnabled = false;
                        flipper.Visibility = Visibility.Hidden;

                        break;
                    }
                case BUTTON.ADDFRIENDS:
                    {
                        if (flipper.Visibility == Visibility.Visible && Avatar.Visibility == Visibility.Hidden)
                            HiddenElement();
                        else
                        {
                            HiddenReqestsInfo();

                            tb_login.Text = null;
                            Flipper_b.Content = "Search";

                            l_receipt.Visibility = Visibility.Visible;
                            l_send.Visibility = Visibility.Visible;

                            l_nicname.Visibility = Visibility.Hidden;
                            l_pass.Visibility = Visibility.Hidden;

                            sp_Requests.Visibility = Visibility.Visible;

                            lb_requests.Visibility = Visibility.Visible;
                            lb_requests_Send.Visibility = Visibility.Visible;
                            gr_requestInfo.Visibility = Visibility.Visible;

                            tb_password.Visibility = Visibility.Hidden;
                            tb_nickname.Visibility = Visibility.Hidden;
                            Avatar.Visibility = Visibility.Hidden;
                            HiddenElement_();
                        }
                        break;
                    }
            }
        }

        private void HiddenElement()
        {
            lb_requests.ItemsSource = null;
            HidenContactInfo();
            HidenChatInfo();
            lb_contacts.Visibility = Visibility.Hidden;

            sp_Requests.Visibility = Visibility.Hidden;
            HiddenReqestsInfo();

            l_receipt.Visibility = Visibility.Hidden;
            l_send.Visibility = Visibility.Hidden;

            lb_requests.Visibility = Visibility.Hidden;
            gr_requestInfo.Visibility = Visibility.Hidden;
            flipper.IsEnabled = false;
            flipper.Visibility = Visibility.Collapsed;
        }

        private void HiddenElement_()
        {
            HidenContactInfo();
            HidenChatInfo();
            lastLogin = login_;
            lb_contacts.Visibility = Visibility.Hidden;
            flipper.IsEnabled = true;
            flipper.Visibility = Visibility.Visible;

            lb_chats.Visibility = Visibility.Hidden;
        }

        private void HidenContactInfo()
            => gr_contactInfo.Visibility = Visibility.Hidden;

        private void HidenChatInfo()
        {
            gr_chatInfo.Visibility = Visibility.Hidden;
            chat_lb.Visibility = Visibility.Hidden;
        }

        private void HiddenReqestsInfo()
        {
            gr_requestInfo.Visibility = Visibility.Hidden;
            bt_accept_r.Visibility = Visibility.Hidden;
            bt_reject_r.Visibility = Visibility.Hidden;
            avatar_r.Visibility = Visibility.Hidden;

            nickname_r.Visibility = Visibility.Hidden;
            login_r.Visibility = Visibility.Hidden;
            status_r.Visibility = Visibility.Hidden;

            l_login_r.Visibility = Visibility.Hidden;
            l_nickname_r.Visibility = Visibility.Hidden;
            l_status_r.Visibility = Visibility.Hidden;

            lb_requests.SelectedItem = 0;
            lb_requests_Send.SelectedItem = 0;
        }
    }
}
