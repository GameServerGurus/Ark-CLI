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

namespace Ark_CLI
{
    public partial class PasswordInput : UserControl
    {
        public string Password {
            // Allows for the Connect.xaml to access this value.
            get => this.password_component.Password; 
            set {
                if (value.Length == 0)
                    this.password_placeholder.Visibility = Visibility.Visible;
                else
                    this.password_placeholder.Visibility = Visibility.Collapsed;
                this.password_component.Password = value; 
            } 
        }

        public object Placeholder
        {
            get => this.password_placeholder.Content;
            set => this.password_placeholder.Content = value;
        }

        public PasswordInput()
        {
            InitializeComponent();
        }

        private void password_component_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.Password.Length == 0)
                password_placeholder.Visibility = Visibility.Collapsed;
        }

        private void password_component_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.Password.Length == 0)
                password_placeholder.Visibility = Visibility.Visible;
        }
    }
}
