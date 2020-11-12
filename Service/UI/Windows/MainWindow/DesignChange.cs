namespace UI
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    public static class Lists
    {
        public static Collection<string> contacts = new ObservableCollection<string>();
        public static Collection<string> receivedRequests = new ObservableCollection<string>();
        public static Collection<string> sendRequests = new ObservableCollection<string>();
        public static Collection<string> noChat = new ObservableCollection<string>();
        public static Collection<string> chats = new ObservableCollection<string>();
        public static Dictionary<List<string>, string> messages = new Dictionary<List<string>, string>();
        public static string chatOn;
    }

    public partial class Profile
    {
        private void Hiden(BUTTON btn)
        {
            switch (btn)
            {
                case BUTTON.CHATS:
                    {
                        insomable.OpenWindow(new Chats(login_, password_, nickname_, photo_), this);
                        break;
                    }
                case BUTTON.ALLCONTACTS:
                    {
                        insomable.OpenWindow(new Contact(login_, password_, nickname_, photo_), this);
                        break;
                    }
                case BUTTON.ADDFRIENDS:
                    {
                        insomable.OpenWindow(new Requests(login_, password_, nickname_, photo_), this);
                        break;
                    }
                default:
                    {
                        if (flipper.Visibility == Visibility.Visible &&
                            Avatar.Visibility == Visibility.Visible)
                        {
                            flipper.IsEnabled = false;
                            flipper.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            flipper.IsEnabled = true;
                            flipper.Visibility = Visibility.Visible;
                            tb_login.Text = login_;
                            Flipper_b.Content = "Save";
                        }
                        break;
                    }
            }
        }
    }

    public partial class Requests
    {
        private void Hiden(BUTTON btn)
        {
            switch (btn)
            {
                case BUTTON.PROFILE:
                    {
                        insomable.OpenWindow(new Profile(login_, password_, nickname_, photo_), this);
                        break;
                    }
                case BUTTON.CHATS:
                    {
                        insomable.OpenWindow(new Chats(login_, password_, nickname_, photo_), this);
                        break;
                    }
                case BUTTON.ALLCONTACTS:
                    {
                        insomable.OpenWindow(new Contact(login_, password_, nickname_, photo_), this);
                        break;
                    }
                default:
                    {
                        if (flipper.Visibility == Visibility.Visible)
                        {
                            flipper.IsEnabled = false;
                            flipper.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            gr_requestInfo.Visibility = Visibility.Hidden;

                            avatar_r.Visibility = Visibility.Hidden;
                            login_r.Visibility = Visibility.Hidden;
                            nickname_r.Visibility = Visibility.Hidden;
                            status_r.Visibility = Visibility.Hidden;

                            l_login_r.Visibility = Visibility.Hidden;
                            l_nickname_r.Visibility = Visibility.Hidden;
                            l_status_r.Visibility = Visibility.Hidden;

                            bt_accept_r.Visibility = Visibility.Hidden;
                            bt_reject_r.Visibility = Visibility.Hidden;

                            flipper.IsEnabled = true;
                            flipper.Visibility = Visibility.Visible;
                            Flipper_b.Content = "Search";
                        }
                        break;
                    }
            }
        }
    }

    public partial class Contact
    {
        private void Hiden(BUTTON btn)
        {
            switch (btn)
            {
                case BUTTON.PROFILE:
                    {
                        insomable.OpenWindow(new Profile(login_, password_, nickname_, photo_), this);
                        break;
                    }
                case BUTTON.CHATS:
                    {
                        insomable.OpenWindow(new Chats(login_, password_, nickname_, photo_), this);
                        break;
                    }
                case BUTTON.ADDFRIENDS:
                    {
                        insomable.OpenWindow(new Requests(login_, password_, nickname_, photo_), this);
                        break;
                    }
                default: break;
            }
        }
    }

    public partial class Chats
    {
        private void Hiden(BUTTON btn)
        {
            switch (btn)
            {
                case BUTTON.PROFILE:
                    {
                        insomable.OpenWindow(new Profile(login_, password_, nickname_, photo_), this);
                        Lists.chatOn = null;
                        break;
                    }
                case BUTTON.ALLCONTACTS:
                    {
                        insomable.OpenWindow(new Contact(login_, password_, nickname_, photo_), this);
                        Lists.chatOn = null;
                        break;
                    }
                case BUTTON.ADDFRIENDS:
                    {
                        insomable.OpenWindow(new Requests(login_, password_, nickname_, photo_), this);
                        Lists.chatOn = null;
                        break;
                    }
                default: break;
            }
        }
    }
}
namespace UI.Windows.MainWindow
{
    public partial class Main
    {
        private void Hiden(BUTTON btn)
        {
            switch (btn)
            {
                case BUTTON.PROFILE:
                    {
                        insomable.OpenWindow(new Profile(login_, password_, nickname_, photo_), this);
                        break;
                    }
                case BUTTON.CHATS:
                    {
                        insomable.OpenWindow(new Chats(login_, password_, nickname_, photo_), this);
                        break;
                    }
                case BUTTON.ALLCONTACTS:
                    {
                        insomable.OpenWindow(new Contact(login_, password_, nickname_, photo_), this);
                        break;
                    }
                case BUTTON.ADDFRIENDS:
                    {
                        insomable.OpenWindow(new Requests(login_, password_, nickname_, photo_), this);
                        break;
                    }
            }
        }
    }
}
