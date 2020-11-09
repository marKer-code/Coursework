namespace UI
{
    public class Close
    {
        public bool ProgramClose { get; set; }
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
                default: break;
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
                default: break;
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
