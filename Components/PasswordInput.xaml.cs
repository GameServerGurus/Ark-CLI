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
                if (value.Length == 0 && !this.password_component.IsFocused)
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

        /*********** EnterKeyDown Event Handler ****************/

        public static readonly RoutedEvent EnterKeyDownEvent = EventManager.RegisterRoutedEvent(
            name: "PasswordInputComponent_EnterKeyDown", // Name must be a unique identifier
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(KeyEventHandler),
            ownerType: typeof(TextInput)
        );

        private void password_component_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                // Trigger the EnterKeyDown event.
                // This event is registered in the properties to be used.
                RoutedEventArgs routedEventArgs = new RoutedEventArgs(routedEvent: EnterKeyDownEvent);
                RaiseEvent(routedEventArgs);
            }
        }

        public event RoutedEventHandler EnterKeyDown
        {
            // Registers the name "EnterKeyDown" into the event properties
            add { AddHandler(EnterKeyDownEvent, value); }
            remove { RemoveHandler(EnterKeyDownEvent, value); }
        }
    }
}
