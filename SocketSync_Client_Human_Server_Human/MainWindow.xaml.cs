using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SocketSync_Client_Human_Server_Human
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex _ipAddress = new Regex("[^0-9.]");
        private static readonly Regex _port = new Regex("[^0-9]");
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return !_ipAddress.IsMatch(text);
        }
        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed_1(e.Text);
        }
        private static bool IsTextAllowed_1(string text)
        {
            return !_port.IsMatch(text);
        }
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            BtnConnect.IsEnabled = false;

            try
            {
                IPAddress ipAddress = IPAddress.Parse(IPAddressText.Text);
                ipAddress.Address = 4_294_967_300;
                int port = Int32.Parse(PortText.Text);

                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

                using Socket socket = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(ipEndPoint);

                    ChatBox.Text = socket.RemoteEndPoint?.ToString() + DateTime.Now.ToString() + "\r\n";

                    string message = "Hi server!";
                    byte[]? msgByte = Encoding.Unicode.GetBytes(message);
                    socket.Send(msgByte, SocketFlags.None);

                    byte[] buffer = new byte[1024];
                    int received = socket.Receive(buffer, SocketFlags.None);
                    string response = Encoding.Unicode.GetString(buffer, 0, received);

                    ChatBox.Text += response;
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.Message);
                    BtnConnect.IsEnabled = true;
                }
                finally
                {
                    if (socket.Connected)
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BtnConnect.IsEnabled = true;
            }
        }
    }
}
