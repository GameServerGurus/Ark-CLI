using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Controllers
{
    /// <summary>
    /// Interaction logic for Connect.xaml
    /// </summary>
    public partial class Connect : UserControl
    {
        
        public string Username {
            get { return username_control.Text; }
            set { username_control.Text = value;  } 
        }

        public string Password
        {
            get { return password_control.Password; }
            set { password_control.Password = value; }
        }

        public object UsernamePlaceholder
        {
            get { return username_control.Placeholder; }
            set { username_control.Placeholder = value; }
        }

        public object PasswordPlaceholder
        {
            get { return password_control.Placeholder; }
            set { password_control.Placeholder = value; }
        }

        public Connect()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            submit.IsEnabled = false;
            // TODO: check if the username and password is accessed correctly
            username_control.Text = "This component isn't finished yet";
            submit.IsEnabled = true;
        }

        private void link_MouseEnter(object sender, MouseEventArgs e)
        {
            Color color = (Color) ColorConverter.ConvertFromString("#00585A");
            link.Foreground = new SolidColorBrush(color);
        }

        private void link_MouseLeave(object sender, MouseEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FF55FBFF");
            link.Foreground = new SolidColorBrush(color);
        }

        private void link_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo {
                UseShellExecute = true,
                FileName = "https://www.wiki.loadingproductions.com"
            };
            System.Diagnostics.Process.Start(info);
        }
    }
}
