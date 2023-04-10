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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Control> controls = new List<Control>();
        private string? currentControlUID = null;

        public Control? CurrentController { 
            get => controls.Find(controller => controller.Uid == currentControlUID);
            set
            {
                controls.ForEach(controller => controller.Visibility = Visibility.Collapsed);
                currentControlUID = null;
                if (value != null)
                {
                    currentControlUID = value.Uid;
                    value.Visibility = Visibility.Visible;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            controls.Add(connect_controller);
            controls.Add(command_controller);
            CurrentController = connect_controller;
        }

        private void connect_controller_SubmitClick(object sender, RoutedEventArgs e)
        {
            string username = connect_controller.Username;
            string password = connect_controller.Password;
            if (username.Equals("loadingproductions") && password.Equals("password"))
            {
                connect_controller.Message = "Connection successful";
                CurrentController = command_controller;
            }
            else
                connect_controller.Message = "Username or Password is not correct.";
        }
    }
}
