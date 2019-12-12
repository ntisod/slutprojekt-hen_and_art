using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace BroadcastChat
{
    class Program
    {
        //port som client lyssnar på
        private const int ListenPort = 8080;

        static void Main(string[] args)
        {
            //ip addressen som man skickar förfrågor, man kan ändra den så den stämmer med din local nätverk ip
            // man kan kolla detta i terminal men commanden ipconfig
            IPAddress address = IPAddress.Parse("192.168.80.58");
            //skapar socket
            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram, ProtocolType.Udp);
            socket.EnableBroadcast = true;

            //den behövs inte just nu
            UdpClient Sendback = new UdpClient(8081);
            IPEndPoint ep = new IPEndPoint(address, ListenPort);


            while (true)
            {
                //där man kan skriva medelande, omvandla den till bytes och skicka den till server
                Console.Write(">");
                string msg = Console.ReadLine();
                
                byte[] sendbuf = Encoding.UTF8.GetBytes(msg);
                socket.SendTo(sendbuf, ep);

            }
        }

        
    }
}
