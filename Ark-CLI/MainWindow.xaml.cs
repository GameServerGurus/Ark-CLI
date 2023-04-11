using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Security.Policy;
using Library;
using System.Text.Json.Nodes;

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

            Response<Ping> ping = Requests.get<Ping>("https://www.api.loadingproductions.com/ping");
            if (ping.Ok && ping.Data != null && ping.Data.connection == "successful")
            {
                if (username.Equals("loadingproductions") && password.Equals("password"))
                {
                    connect_controller.Message = "Connection successful";
                    CurrentController = command_controller;
                }
                else
                    connect_controller.Message = "Username or Password is not correct.";
            } else
            {
                connect_controller.Message = "The server is currently undergoing maintainence.";
            }
        }

        private void command_controller_SubmitClick(object sender, RoutedEventArgs e)
        {
            Response<Ping> ping = Requests.get<Ping>("https://www.api.loadingproductions.com/ping");
            if (ping.Ok && ping.Data != null && ping.Data.connection == "successful")
            {
                Dictionary<string, string> dict_parameters = new Dictionary<string, string> { { "code", command_controller.Text } };
                JsonObject json = new JsonObject();
                json.Add("code", command_controller.Text);
                Response<Command>? command = Requests.post<Command>("https://api.loadingproductions.com/command", json.ToString());
                if (command.Ok)
                {
                    command_controller.validated(true);
                }
                else
                {
                    command_controller.validated(false);
                }
            } else
            {
                command_controller.validated(false);
            }
        }
    }
}
