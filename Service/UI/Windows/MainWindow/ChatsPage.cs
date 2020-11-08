namespace UI.Windows.MainWindow
{
    using System.Windows;

    public partial class Main
    {
        private void Lb_chats_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            HiddenReqestsInfo();
            HidenContactInfo();
            gr_chatInfo.Visibility = Visibility.Visible;
            chat_lb.Visibility = Visibility.Visible;
        }
    }
}
