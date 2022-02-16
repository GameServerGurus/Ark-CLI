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
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;

namespace ark_admin_manager
{



    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private static string url = "";
        private static string key = "";
        private bool authenticated = false;

        private string post(string url, FormUrlEncodedContent parameters)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            Task<HttpResponseMessage> response = client.PostAsync(url, parameters);
            string result = "";

            try
            {
                HttpResponseMessage message = response.Result;
                if (message.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    result = message.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                }
                else
                {
                    result = message.ReasonPhrase;
                }
            }
            catch (Exception e)
            {
                result = "API Url failed. Check that your wrote it correctly else the server might be down.";
            }

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();

            return result;
        }

        private string get(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string result = "";
            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    result = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                }
                else
                {
                    result = response.ReasonPhrase;
                }
            }
            catch (Exception e)
            {
                result = "API Key failed. Check that your wrote it correctly else the server might be down.";
            }

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
            return result;

        }

        private string command(string command)
        {
            var url = String.Format("http://{0}/command", MainWindow.url);
            var dict_parameters = new Dictionary<string, string> { { "cheat", command } };
            var parameters = new FormUrlEncodedContent(dict_parameters);
            return post(url, parameters);
        }

        private string authenticate(string api_url, string api_key)
        {
            MainWindow.url = api_url;
            MainWindow.key = api_key;
            var url = String.Format("http://{0}/authenticate", api_url);
            var dict_parameters = new Dictionary<string, string> { { "key", api_key } };
            var parameters = new FormUrlEncodedContent(dict_parameters);
            var message = post(url, parameters);
            this.authenticated = message.Equals("succses");
            return message;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_connect_Click(object sender, RoutedEventArgs e)
        {
            if (!input_api.Text.Equals("API Url") && !input_key.Text.Equals("API Key") && !input_api.Text.Equals("") && !input_key.Equals(""))
            {
                var result = authenticate(input_api.Text, input_key.Text);
                messages.Text = result;
                this.authenticated = result.Equals("success");
            }
            else
            {
                messages.Text = "Enter a valid Url and Key";
            }
        }

        private void Button_send_Click(object sender, RoutedEventArgs e)
        {
            if (!this.authenticated || MainWindow.url.Equals("") || MainWindow.url.Equals(""))
            {
                messages.Text = "You must authenticate the API Url and API key first";
            }
            else if (!input_command.Text.Equals("Enter admin command here"))
            {
                messages.Text = command(input_command.Text);
                input_command.Text = "";
            }
            else if (input_command.Equals("") || input_command.Text.Equals("Enter admin command here"))
            {
                messages.Text = "You must enter a command";
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

        private void Input_api_LostFocus(object sender, RoutedEventArgs e)
        {
            if (input_api.Text.Equals(""))
            {
                input_api.Text = "API Url";
            }
        }

        private void Input_key_LostFocus(object sender, RoutedEventArgs e)
        {
            if (input_key.Text.Equals(""))
            {
                input_key.Text = "API Key";
            }
        }

        private void Input_command_LostFocus(object sender, RoutedEventArgs e)
        {
            if (input_command.Text.Equals(""))
            {
                input_command.Text = "Enter admin command here";
            }
        }
    }
}
