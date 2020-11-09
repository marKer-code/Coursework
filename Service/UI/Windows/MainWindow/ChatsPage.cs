namespace UI
{
    using System.Windows;

    public partial class Chats
    {
        private void Lb_chats_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            gr_chatInfo.Visibility = Visibility.Visible;
            chat_lb.Visibility = Visibility.Visible;
        }
    }
}
