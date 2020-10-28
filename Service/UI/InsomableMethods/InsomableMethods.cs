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

        //public void OpenWindow(Window toOpen, Window toClose)
        //{
        //    toOpen.Show();
        //    toClose.Close();
        //}
        //public string CatchAMessage(int freePort)
        //{
        //    UdpClient listener = new UdpClient(freePort);
        //    try
        //    {
        //        MSG msg = null;
        //        IPEndPoint remoteEndPoint_ = null;

        //        byte[] data = listener.Receive(ref remoteEndPoint_);

        //        XmlSerializer serializer = new XmlSerializer(typeof(MSG));
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            ms.Write(data, 0, data.Length);
        //            ms.Position = 0;

        //            msg = (MSG)serializer.Deserialize(ms);

        //            return msg.Message;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "< Error >",
        //        MessageBoxButton.OK, MessageBoxImage.Error);
        //        return "";
        //    }
        //    finally { listener.Close(); }
        //}
        //public Send_ CatchAMessage_(int freePort)
        //{
        //    UdpClient listener = new UdpClient(freePort);
        //    try
        //    {
        //        Send_ info = null;
        //        IPEndPoint remoteEndPoint_ = null;

        //        byte[] data = listener.Receive(ref remoteEndPoint_);
        //        info = new Send_();

        //        XmlSerializer serializer = new XmlSerializer(typeof(Send_));
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            ms.Write(data, 0, data.Length);
        //            ms.Position = 0;

        //            info = (Send_)serializer.Deserialize(ms);

        //            return info;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "< Error >",
        //        MessageBoxButton.OK, MessageBoxImage.Error);
        //        return null;
        //    }
        //    finally { listener.Close(); }
        //}
        //public Message CatchAMessage_2(int freePort)
        //{
        //    UdpClient listener = new UdpClient(freePort);
        //    try
        //    {
        //        Message msg = null;
        //        IPEndPoint remoteEndPoint_ = null;

        //        byte[] data = listener.Receive(ref remoteEndPoint_);
        //        msg = new Message();

        //        XmlSerializer serializer = new XmlSerializer(typeof(Message));
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            ms.Write(data, 0, data.Length);
        //            ms.Position = 0;

        //            msg = (Message)serializer.Deserialize(ms);

        //            return msg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "< Error >",
        //        MessageBoxButton.OK, MessageBoxImage.Error);
        //        return null;
        //    }
        //    finally { listener.Close(); }
        //}
        //public void SendMSG(Send_ send)
        //{
        //    UdpClient client = new UdpClient();
        //    try
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(Send_));
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            serializer.Serialize(ms, send);

        //            byte[] dataToSend = ms.ToArray();
        //            client.Send(dataToSend, dataToSend.Length, new IPEndPoint(IPAddress.Parse("192.168.1.3" /*server address*/), 3333));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "< Error >",
        //            MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally { client.Close(); }
        //}
        //public bool SendMSG_(Send_ send)
        //{
        //    UdpClient client = new UdpClient();
        //    try
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(Send_));
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            serializer.Serialize(ms, send);

        //            byte[] dataToSend = ms.ToArray();
        //            client.Send(dataToSend, dataToSend.Length, new IPEndPoint(IPAddress.Parse("192.168.1.3" /*server address*/), 3333));
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "< Error >",
        //            MessageBoxButton.OK, MessageBoxImage.Error);
        //        return false;
        //    }
        //    finally { client.Close(); }
        //}

        //public IPAddress GetLocalIPAddress()
        //{
        //    var host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (var ip in host.AddressList)
        //        if (ip.AddressFamily == AddressFamily.InterNetwork)
        //            return ip;
        //    throw new Exception("< No network adapters with an IPv4 address in the system! >");
        //}
        //public int FreeTcpPort()
        //{
        //    TcpListener l = new TcpListener(IPAddress.Loopback, 0);
        //    l.Start();
        //    int port = ((IPEndPoint)l.LocalEndpoint).Port;
        //    l.Stop();
        //    return port;
        //}
    }
}
