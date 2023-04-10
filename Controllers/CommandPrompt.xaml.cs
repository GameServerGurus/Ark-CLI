using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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
using Library;


namespace Controllers
{
    /// <summary>
    /// Interaction logic for CommandPrompt.xaml
    /// </summary>
    public partial class CommandPrompt : UserControl
    {
        public string Text { get => command_control.Text; }

        private Timer? timer;

        public CommandPrompt()
        {
            InitializeComponent();
        }

        private void link_MouseEnter(object sender, MouseEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#00585A");
            link.Foreground = new SolidColorBrush(color);
        }

        private void link_MouseLeave(object sender, MouseEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FF55FBFF");
            link.Foreground = new SolidColorBrush(color);
        }

        private void link_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = "https://wiki.loadingproductions.com/documentation/windows_cli"
            };
            System.Diagnostics.Process.Start(info);
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

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            submit.IsEnabled = false;
            if (command_control.Text.Length > 0)
                RaiseSubmitClickRoutedEvent();
            // Trigger the SubmitClick event.
            // This event is registered in the properties to be used.
            
            command_control.Text = "";
            submit.IsEnabled = true;
        }

        /*********** SubmitClick Event Handler ****************/

        public static readonly RoutedEvent SubmitClickEvent = EventManager.RegisterRoutedEvent(
            name: "CommandPrompt_SubmitClick", // Name must be a unique identifier
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedEventHandler),
            ownerType: typeof(Connect)
        );

        public event RoutedEventHandler SubmitClick
        {
            add { AddHandler(SubmitClickEvent, value); }
            remove { RemoveHandler(SubmitClickEvent, value); }
        }

        void RaiseSubmitClickRoutedEvent()
        {
            RoutedEventArgs routedEventArgs = new RoutedEventArgs(routedEvent: SubmitClickEvent);
            RaiseEvent(routedEventArgs);
        }
    }
}
