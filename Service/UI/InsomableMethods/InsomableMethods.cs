namespace UI.InsomableMethods_
{
    using System.Windows.Controls;
    using System.Windows;

    class InsomableMethods : IInsomableMethods
    {
        public void OpenWindow(Window toOpen, Window toClose)
        {
            toOpen.Show();
            toClose.Close();
        }

        public void PasswordAppearance(TextBox textBox, PasswordBox passwordBox, ref bool passwordBoxActive)
        {
            if (textBox.Visibility == Visibility.Hidden)
            {
                passwordBox.Visibility = Visibility.Hidden;
                textBox.Visibility = Visibility.Visible;
                textBox.Text = passwordBox.Password;
                textBox.Focus();
                passwordBoxActive = false;
            }
            else
            {
                passwordBox.Visibility = Visibility.Visible;
                textBox.Visibility = Visibility.Hidden;
                passwordBox.Password = textBox.Text;
                passwordBox.Focus();
                passwordBoxActive = true;
            }
        }
    }
}
