using System.Windows.Controls;
using System.Windows.Input;

namespace Messenger.Pages
{
    public partial class Dialog : Page
    {
        public Dialog()
        {
            InitializeComponent();
        }

        private void SendMessage(object sender, MouseButtonEventArgs e)
        {
            message.Text = "its work";
        }

        private void LinkDocument(object sender, MouseButtonEventArgs e)
        {
            message.Text = "";
        }
    }
}