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
            get => username_control.Text;
            set => username_control.Text = value;
        }

        public string Password
        {
            get => password_control.Password;
            set => password_control.Password = value;
        }

        public object UsernamePlaceholder
        {
            get => username_control.Placeholder;
            set => username_control.Placeholder = value;
        }

        public object PasswordPlaceholder
        {
            get => password_control.Placeholder;
            set => password_control.Placeholder = value;
        }

        public object Message
        {
            get => message.Content;
            set => message.Content = value;
        }

        public Connect()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            submit.IsEnabled = false;
            // Trigger the SubmitClick event.
            // This event is registered in the properties to be used.
            RaiseSubmitClickRoutedEvent();
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
                FileName = "https://wiki.loadingproductions.com/signup"
            };
            System.Diagnostics.Process.Start(info);
        }



        /*********** SubmitClick Event Handler ****************/

        public static readonly RoutedEvent SubmitClickEvent = EventManager.RegisterRoutedEvent(
            name: "SubmitClick",
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedEventHandler),
            ownerType: typeof(Connect)
        );

        public event RoutedEventHandler SubmitClick
        {
            add { AddHandler(SubmitClickEvent, value); }
            remove { RemoveHandler(SubmitClickEvent, value);}
        }

        void RaiseSubmitClickRoutedEvent()
        {
            RoutedEventArgs routedEventArgs = new RoutedEventArgs(routedEvent: SubmitClickEvent);
            RaiseEvent(routedEventArgs);
        }

    }
}
