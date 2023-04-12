using Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
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
using static System.Net.WebRequestMethods;

namespace Controllers
{
    /// <summary>
    /// Interaction logic for DevCommandPrompt.xaml
    /// </summary>
    public partial class DevCommandPrompt : UserControl
    {
        public string Text { get => command_control.Text; set => command_control.Text = value; }
        private Timer? timer;
        private string domain = "http://127.0.0.1:5000";

        public DevCommandPrompt()
        {
            InitializeComponent();
        }

        public void validated(bool status)
        {
            if (status)
            {
                validator.Content = "✓";
                Color color = (Color)ColorConverter.ConvertFromString("#008588");
                validator.Foreground = new SolidColorBrush(color);
            }
            else
            {
                validator.Content = "🚫";
                Color color = (Color)ColorConverter.ConvertFromString("#AA0400");
                validator.Foreground = new SolidColorBrush(color);
            }
            if (timer == null)
                timer = new Timer(state => this.Dispatcher.Invoke(() => validator.Content = ""), null, 2000, Timeout.Infinite);
            else
                timer.Change(2000, Timeout.Infinite);
        }

        private void send_command_to_api()
        {
            string pingUrl = String.Format("{0}{1}", domain, "/ping");
            string commandUrl = String.Format("{0}{1}", domain, "/command");

            Response<Ping> ping = Requests.get<Ping>(pingUrl);
            if (ping.Ok && ping.Data != null && ping.Data.connection == "successful")
            {
                JsonObject jsonSent = new JsonObject() { { "code", this.Text } };       //json.Add("code", this.Text);
                Response<Command>? command = Requests.post<Command>(commandUrl, jsonSent);
                this.validated(command.Ok);

                string jsonData = jsonSent.ToString();
                string response = (command.JSON != null) ? command.JSON.ToString() : "";
                log.Text = String.Format("[URL]: {0}\n\n[JSON Data]: {1}\n\n[Response]: {2}", commandUrl, jsonData, response);
            }
            else
            {
                this.validated(false);
                log.Text = String.Format("[URL]: {0}\n\n[Response]: {1}", pingUrl, ping.Text);
            }
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            submit.IsEnabled = false;
            command_control.IsEnabled = false;
            if (command_control.Text.Length > 0)
                this.send_command_to_api();

            command_control.Text = "";
            submit.IsEnabled = true;
            command_control.IsEnabled = true;
        }

        private void localhost_checkbox_Click(object sender, RoutedEventArgs e)
        {
            domain = "http://127.0.0.1:5000";
            official_checkbox.IsChecked = false;
            test_api_checkbox.IsChecked = false;
            official_checkbox.IsEnabled = true;
            test_api_checkbox.IsEnabled = true;
            localhost_checkbox.IsEnabled = false;
            log.Text = String.Format("[Domain Changed]: {0}", domain);
        }

        private void official_checkbox_Click(object sender, RoutedEventArgs e)
        {
            domain = "https://www.api.loadingproductions.com";
            localhost_checkbox.IsChecked = false;
            test_api_checkbox.IsChecked = false;
            localhost_checkbox.IsEnabled = true;
            test_api_checkbox.IsEnabled = true;
            official_checkbox.IsEnabled = false;
            log.Text = String.Format("[Domain Changed]: {0}", domain);
        }

        private void test_api_checkbox_Click(object sender, RoutedEventArgs e)
        {
            domain = "http://3.137.160.113";
            localhost_checkbox.IsChecked = false;
            official_checkbox.IsChecked = false;
            official_checkbox.IsEnabled = true;
            localhost_checkbox.IsEnabled = true;
            test_api_checkbox.IsEnabled = false;
            log.Text = String.Format("[Domain Changed]: {0}", domain);
        }

        private void command_control_EnterKeyDown(object sender, RoutedEventArgs e)
        {
            submit.IsEnabled = false;
            command_control.IsEnabled = false;
            if (command_control.Text.Length > 0)
                this.send_command_to_api();

            command_control.Text = "";
            submit.IsEnabled = true;
            command_control.IsEnabled = true;
            command_control.Focus();
        }
    }
}
