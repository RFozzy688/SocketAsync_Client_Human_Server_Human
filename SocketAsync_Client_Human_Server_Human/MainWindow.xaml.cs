using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SocketAsync_Client_Human_Server_Human
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
    }
}
