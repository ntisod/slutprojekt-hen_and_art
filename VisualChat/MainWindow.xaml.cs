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
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace VisualChat
{

    /// Interaction logic for MainWindow.xaml
 
    public partial class MainWindow : Window
    {
        //variabler som vi kommer använda för stavning kontroll
        string Ipconnect = null;
        int Portconnect = -1;
        string nickname = null;

        //andra variabler för server/client
        private TcpClient client;
        public StreamReader STR;
        public StreamWriter STW;
        public string recieve;
        public String TextToSend;

        //bara mainwindow
        public MainWindow()
        {
            InitializeComponent();
        }

        public char Reference(ref Ipconnect, nickname, Portconnect)
        {
            Ipconnect = Ip_connect.Text;
            Portconnect = Convert.ToInt32(port_connect.Text);
            nickname = Nickname_box.Text;
        }

        //knappen som starta "stavnings" kotroll på alla textbox's så dem är inte tomma 
        private void Connect_Button_Click(object sender, RoutedEventArgs e)
        {
            // konverterar textbox's till int
            Ipconnect = Ip_connect.Text;
            Portconnect = Convert.ToInt32(port_connect.Text);
            nickname = Nickname_box.Text;

            //själva scripten
            if (nickname != null) {
                if (Ipconnect != null) {
                    if (Portconnect > -1)
                    {
                        Ipconnect = Ip_connect.Text;
                        Portconnect = Convert.ToInt32(port_connect.Text);
                        SecondWindow popup = new SecondWindow();
                        Close();
                        popup.ShowDialog();
                    }
                    {
                        MessageBoxResult result = MessageBox.Show("Du skrev fel port nummer!",
                                         "Fel");
                    }
                }
                {
                    MessageBoxResult result = MessageBox.Show("Du skrev fel Ip address!" + Ipconnect,
                                         "Fel");
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Du måste fulla in 'nickname' ryta!",
                                          "Fel");
            }

            client = new TcpClient();
            IPEndPoint IpEnd = new IPEndPoint(IPAddress.Parse(Ip_connect.Text), int.Parse(port_connect.Text));

            
            try
            {
                client.Connect(IpEnd);

                if (client.Connected)
                {
                    ChatTextBox.AppendText("Connected to server" + "\n");
                    STW = new StreamWriter(client.GetStream());
                    STR = new StreamReader(client.GetStream());
                    STW.AutoFlush = true;
                    Client_connect.RunWorkerAsync();
                    Client_disconnect.WorkerSupportsCancellation = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Ip_connect_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void Port_connect_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Host_button_Click(object sender, RoutedEventArgs e)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, int.Parse(port_connect.Text));
            listener.Start();
            client = listener.AcceptTcpClient();
            STR = new StreamReader(client.GetStream());
            STW = new StreamWriter(client.GetStream());
            STW.AutoFlush = true;

            Client_connect.RunWorkerAsync();
            Client_disconnect.WorkerSupportsCancellation = true;

        }

        private void Nickname_box_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
