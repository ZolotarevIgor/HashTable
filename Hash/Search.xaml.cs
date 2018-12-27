using System.Windows;
using System.Windows.Input;

namespace Hash
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        public Test Work { get; set; }

        public Search(Test th)
        {
            InitializeComponent();

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            Top = (screenHeight - 2 * Height) / 2;
            Left = (screenWidth - 2 * Width) / 2;
            Work = th;
            txtb.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Work.WorkButton(txtb.Text, this);
            this.Close();
        }
        
        private void Txtb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(sender, e);
            if (Key.D0 <= e.Key && Key.D9 >= e.Key)
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
