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
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace VisualChat
{

    class Saver
    {
       public Connects()
        {
            if true(){

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
        }


    }
}
