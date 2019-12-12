using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Brodder
{
    class Program
    {
        //port som man lyssnar på
        private const int ListenPort = 8080;

        static void Main(string[] args)
        {
            //skapar threads
            var listenThread = new Thread(Listener);
            listenThread.Start();

        }

        static void Listener()
        {
            //skapar ny instans av udpclient och socket
            UdpClient listener = new UdpClient(ListenPort);
            Socket socket = new Socket(AddressFamily.InterNetwork,
               SocketType.Dgram, ProtocolType.Udp);
            socket.EnableBroadcast = true;

            try
            {
                while (true)
                {
                    //den lyssnar på portet och alla ip addressana
                    IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, ListenPort);
                    //få information i bytes
                    byte[] bytes = listener.Receive(ref groupEP);
                    //skriver ut data och omvandlar den till utf8
                    Console.WriteLine("Received broadcast from {0} : {1}\n", groupEP.ToString(),
                        Encoding.UTF8.GetString(bytes, 0, bytes.Length));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }
    }
}
