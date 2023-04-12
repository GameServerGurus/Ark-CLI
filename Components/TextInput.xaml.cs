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
    public partial class TextInput : UserControl
    {
        public string Text
        {
            get => this.textbox_component.Text;
            set {
                if (value.Length == 0 && !this.textbox_component.IsFocused)
                    this.textbox_placeholder.Visibility = Visibility.Visible;
                else
                    this.textbox_placeholder.Visibility = Visibility.Collapsed;
                this.textbox_component.Text = value; 
            }
        }

        public object Placeholder
        {
            get => this.textbox_placeholder.Content;
            set => this.textbox_placeholder.Content = value;
        }

        public TextInput()
        {
            InitializeComponent();
        }

        private void textbox_component_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.Text.Length == 0)
                textbox_placeholder.Visibility = Visibility.Collapsed;
        }

        private void textbox_component_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.Text.Length == 0)
                textbox_placeholder.Visibility = Visibility.Visible;
        }


        /*********** EnterKeyDown Event Handler ****************/
        
        public static readonly RoutedEvent EnterKeyDownEvent = EventManager.RegisterRoutedEvent(
            name: "TextInputComponent_EnterKeyDown", // Name must be a unique identifier
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(KeyEventHandler),
            ownerType: typeof(TextInput)
        );

        private void textbox_component_KeyDown(object sender, KeyEventArgs e)
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
