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

namespace ark_admin_manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_send_Click(object sender, RoutedEventArgs e)
        {
            if (!input_command.Text.Equals("Enter admin command here"))
            {
                input_command.Text = "";
            }
        }

        private void Input_command_GotFocus(object sender, RoutedEventArgs e)
        {
            if (input_command.Text.Equals("Enter admin command here"))
            {
                input_command.Text = "";
            }
        }
        
        private void Input_api_GotFocus(object sender, RoutedEventArgs e)
        {
            if (input_api.Text.Equals("API Url"))
            {
                input_api.Text = "";
            }
        }

        private void Input_key_GotFocus(object sender, RoutedEventArgs e)
        {
            if (input_key.Text.Equals("API Key"))
            {
                input_key.Text = "";
            }
        }

        private void Button_connect_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
