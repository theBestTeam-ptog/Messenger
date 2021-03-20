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

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            //System.Windows.Controls.TextBox.Text = "its work";
        }
        
        private void UIElement_OnMouseUp2(object sender, MouseButtonEventArgs e)
        {
            //System.Windows.Controls.TextBox.Text = "";
        }
    }
}