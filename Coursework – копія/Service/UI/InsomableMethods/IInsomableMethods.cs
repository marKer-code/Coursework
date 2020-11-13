namespace UI.InsomableMethods_
{
    using System.Windows;
    using System.Windows.Controls;

    interface IInsomableMethods
    {
        void OpenWindow(Window toOpen, Window toClose);
        void PasswordAppearance(TextBox textBox, PasswordBox passwordBox, ref bool passwordBoxActive);
    }
}
