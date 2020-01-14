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
using System.ComponentModel;
using System.Configuration;

namespace VisualChat
{

    public partial class SecondWindow : Window
    {
        MainWindow MainWindow = new MainWindow();
        static int localPort = Int32.Parse(((MainWindow)Application.Current.MainWindow).port_connect.Text);
        static string Nickname = ((MainWindow)Application.Current.MainWindow).Nickname_box.Text;
        string TTS;

        public SecondWindow()
        {
            InitializeComponent();

            if (((MainWindow)Application.Current.MainWindow).SaveBox.Text == "host")
            {
                System.Threading.Thread.Sleep(2000);
                Server.Startserver();

            }
            else if (((MainWindow)Application.Current.MainWindow).SaveBox.Text == "connect")
            {
                System.Threading.Thread.Sleep(2000);
                Client.Startclient();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Asså hur kunde starta andra fönstret utan att klicka på knappen...",
                                    "Fel");
                Close();
            }
        }

        

        public void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendText_Box.Text = TTS;
            SendText_Box.Text += ChatText_Box.Text;
            SendText_Box.Clear();
        }

        private void ChatText_Box_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SendText_Box_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        class Server
        {

            static Socket listeningSocket;

            static List<IPEndPoint> clients = new List<IPEndPoint>();

            public static void Startserver()
            {

                try
                {
                    SecondWindow secondWindow = new SecondWindow();
                    secondWindow.ChatText_Box.Text += "Server startas";

                    listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    Task listeningTask = new Task(Listen);
                    listeningTask.Start();
                    listeningTask.Wait();
                }
                catch (Exception)
                {
                    SecondWindow secondWindow = new SecondWindow();
                    secondWindow.ChatText_Box.Text += secondWindow.TTS;
                }
                finally
                {
                    Close();
                }
            }


            public static void Listen()
            {
                try
                {
                    
                    IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), localPort);
                    listeningSocket.Bind(localIP);

                    while (true)
                    {
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;
                        byte[] data = new byte[256];
                        EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);
                        do
                        {
                            bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (listeningSocket.Available > 0);
                        IPEndPoint remoteFullIp = remoteIp as IPEndPoint;
                        SecondWindow secondWindow = new SecondWindow();
                        string UIM = secondWindow.SendText_Box.Text;
                        UIM += builder.ToString() + "\r\n";
                        bool addClient = true;
                        for (int i = 0; i < clients.Count; i++)
                            if (clients[i].Address.ToString() == remoteFullIp.Address.ToString())
                                addClient = false;
                        if (addClient == true)  
                            clients.Add(remoteFullIp);
                        BroadcastMessage(builder.ToString(), remoteFullIp.Address.ToString());
                    }
                }
                catch (Exception)
                {
                    SecondWindow secondWindow = new SecondWindow();
                    secondWindow.ChatText_Box.Text += secondWindow.TTS;
                }
                finally
                {
                    Close();
                }
            }


            private static void BroadcastMessage(string TTS , string ip)
            {
                byte[] data = Encoding.Unicode.GetBytes(TTS);

                for (int i = 0; i < clients.Count; i++)
                    if (clients[i].Address.ToString() != ip)
                        listeningSocket.SendTo(data, clients[i]);
            }


            private static void Close()
            {
                if (listeningSocket != null)
                {
                    listeningSocket.Shutdown(SocketShutdown.Both);
                    listeningSocket.Close();
                    listeningSocket = null;
                }
                SecondWindow secondWindow = new SecondWindow();
                secondWindow.ChatText_Box.Text += "Server stänge sig själv!";
            }
        }

        class Client
        {
            static int remotePort;
            static IPAddress ipAddress;
            static Socket listeningSocket;

            public static void Startclient()
            {
                
                ipAddress = IPAddress.Parse(((MainWindow)Application.Current.MainWindow).Ip_connect.Text);
                remotePort = localPort;
                try
                {
                    listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    Task listeningTask = new Task(Listen);
                    listeningTask.Start();
                    SecondWindow secondWindow = new SecondWindow();

                    while (true)
                    {
                        

                        secondWindow.SendButton.Click += (source, e)=>
                        {
                            secondWindow.SendText_Box.Text = secondWindow.TTS;
                        };


                        byte[] data = Encoding.Unicode.GetBytes(secondWindow.TTS);
                        EndPoint remotePoint = new IPEndPoint(ipAddress, remotePort);
                        listeningSocket.SendTo(data, remotePoint);
                    }
                }
                catch (Exception)
                {
                    SecondWindow secondWindow = new SecondWindow();
                    secondWindow.ChatText_Box.Text += secondWindow.TTS;
                }
                finally
                {
                    Close();
                }
            }


            private static void Listen()
            {
                try
                {
                    IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);
                    listeningSocket.Bind(localIP);

                    while (true)
                    {
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;
                        byte[] data = new byte[256];

                        EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);

                        do
                        {
                            bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (listeningSocket.Available > 0);

                        IPEndPoint remoteFullIp = remoteIp as IPEndPoint;

                        Console.WriteLine("{0}:{1} - {2}", remoteFullIp.Address.ToString(), remoteFullIp.Port, builder.ToString());
                    }
                }
                catch (Exception)
                {
                    SecondWindow secondWindow = new SecondWindow();
                    secondWindow.ChatText_Box.Text += secondWindow.TTS;
                }
                finally
                {
                    Close();
                }
            }

            private static void Close()
            {
                if (listeningSocket != null)
                {
                    listeningSocket.Shutdown(SocketShutdown.Both);
                    listeningSocket.Close();
                    listeningSocket = null;
                }

                SecondWindow secondWindow = new SecondWindow();
                secondWindow.ChatText_Box.Text += "Server stänge sig själv!";
            }
        }


    }
}
