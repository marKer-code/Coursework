using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI.InsomableMethods_
{
    interface IInsomableMethods
    {
        //void OpenWindow(Window toOpen, Window toClose);
        void PasswordAppearance(TextBox textBox, PasswordBox passwordBox, ref bool passwordBoxActive);
        //IPAddress GetLocalIPAddress();
        //int FreeTcpPort();
        //void SendMSG(Send_ send);
        //bool SendMSG_(Send_ send);
        //string CatchAMessage(int freePort);
        //Message CatchAMessage_2(int freePort);
        //Send_ CatchAMessage_(int freePort);
    }
}
