using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace VisualChat
{

    public partial class SecondWindow : Window
    {
        private TcpClient client;
        public StreamReader STR;
        public StreamWriter STW;
        public string recieve;
        public String TextToSend;

        public SecondWindow()
        {
            InitializeComponent();

            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress address in localIP)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    Ip_connect.Text = address.ToString();
                }
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (SendTextBox.Text != "")
            {
                TextToSend = SendTextBox.Text;
                Client_disconnect.RunWorkerAsync();
            }
            SendTextBox.Text = "";
        }

        private void ChatTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SendTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Client_connect()

        {
            while (client.Connected)
            {
                try
                {
                    recieve = STR.ReadLine();
                    this.ChatTextBox.Invoke(new MethodInvoker(delegate ()
                    {
                        ChatScreentextBox.AppendText("You:" + recieve + "\n");
                    }));
                    recieve = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }


        }

        private void Client_disconnect()
        {
            if (client.Connected)
            {
                STW.WriteLine(TextToSend);
                this.ChatTextBox.Invoke(new MethodInvoker(delegate () { ChatScreentextBox.AppendText("Me:" + TextToSend + "\n"); }));
            }
            else
            {
                MessageBox.Show("Fel, kan inte skicka text!");
            }
            Worker.Client_disconnect.CancelAsync();
        }
    }
}
