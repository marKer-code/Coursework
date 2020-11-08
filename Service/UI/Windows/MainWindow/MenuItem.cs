namespace UI.Windows.MainWindow
{
    using System.Windows.Input;

    public partial class Main
    {
        private void Chat_badged_MouseDown(object sender, MouseButtonEventArgs e)
            => Hiden(BUTTON.CHATS);

        private void AllContacts_badget_MouseDown(object sender, MouseButtonEventArgs e)
            => Hiden(BUTTON.ALLCONTACTS);

        private void AddFriend_badget_MouseDown(object sender, MouseButtonEventArgs e)
            => Hiden(BUTTON.ADDFRIENDS);

        private void I_Profile_MouseDown(object sender, MouseButtonEventArgs e)
            => Hiden(BUTTON.PROFILE);
    }
}
