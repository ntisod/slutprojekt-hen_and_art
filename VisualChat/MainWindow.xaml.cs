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
using System.ComponentModel;
using System.Threading;
using System.Configuration;

namespace VisualChat
{
    public partial class MainWindow : Window
    {
        
        public int ListenPort = -1;
        

        
        public MainWindow()
        {
            InitializeComponent();
        }


        public void Connect_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveBox.Text = "connect";
            
            if (port_connect.Text.Length == 0)
            {
                MessageBoxResult result = MessageBox.Show("Du MÅSTE fulla in 'Port' ryta!",
                                        "Fel");
            }
            else
            {
                
                ListenPort = Convert.ToInt32(port_connect.Text);
                if (ListenPort < 0)
                {
                    MessageBoxResult result = MessageBox.Show("Fel port! Port måste vara 0 eller mer!",
                                        "Fel");
                }
                else
                {
                    if (Nickname_box.Text.Length != 0)
                    {
                        if (Ip_connect.Text.Length != 0)
                        {
                            if (port_connect.Text.Length != 0)
                            {
                                SecondWindow popup = new SecondWindow();
                                Close();
                                popup.ShowDialog();
                            }else
                            {
                                MessageBoxResult result = MessageBox.Show("Du MÅSTE fulla in 'Port' ryta!",
                                                 "Fel");
                            }
                        }else
                        {
                            MessageBoxResult result = MessageBox.Show("Du MÅSTE fulla in 'ip address' ryta!");
                        }
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Du måste fulla in 'nickname' ryta!",
                                                  "Fel");
                    }

                }
            }
           
        }

        public void Host_button_Click(object sender, RoutedEventArgs e)
        {
            SaveBox.Text = "host";

            if (port_connect.Text.Length == 0)
            {
                MessageBoxResult result = MessageBox.Show("Du MÅSTE fulla in 'Port' ryta!",
                                        "Fel");
            }
            else
            {

                ListenPort = Convert.ToInt32(port_connect.Text);
                if (ListenPort < 0)
                {
                    MessageBoxResult result = MessageBox.Show("Fel port! Port måste vara 0 eller mer!",
                                        "Fel");
                }
                else{

                    if (Nickname_box.Text.Length != 0)
                    {
                        if (Ip_connect.Text.Length != 0)
                        {
                            if (port_connect.Text.Length != 0)
                            {
                                SecondWindow popup = new SecondWindow();
                                Close();
                                popup.ShowDialog();
                            }
                            else {
                                MessageBoxResult result = MessageBox.Show("Du MÅSTE fulla in 'Port' ryta!",
                                                 "Fel");
                            }
                           
                        }
                        else{
                            MessageBoxResult result = MessageBox.Show("Du MÅSTE fulla in 'ip address' ryta!");
                        }
                        
                    }
                    else{
                        MessageBoxResult result = MessageBox.Show("Du måste fulla in 'nickname' ryta!",
                                                  "Fel");
                    }
                    

                }
            }


        }

        public void Nickname_box_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void Ip_connect_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void Port_connect_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
