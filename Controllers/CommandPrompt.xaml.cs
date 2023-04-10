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
    /// Interaction logic for CommandPrompt.xaml
    /// </summary>
    public partial class CommandPrompt : UserControl
    {
        public CommandPrompt()
        {
            InitializeComponent();
        }

        private void link_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void link_MouseLeave(object sender, MouseEventArgs e)
        {

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
    }
}
