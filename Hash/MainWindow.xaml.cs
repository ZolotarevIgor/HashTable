using System;
using System.Windows;
using System.Windows.Input;

namespace Hash
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    //
    public partial class MainWindow : Window
    {
        public string sstr;
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Menu();
            //Main.Content = new Welcome();
            //NavigationService.Navigate(new Uri("/Welcom.xaml", UriKind.Relative));
        }

        private void MainW_KeyDown(object sender, KeyEventArgs e)
        {
            
            if ((e.Key == Key.Escape) && (Main.Content.GetType() == new Test().GetType()))
                Main.Content = new Menu();
            if ((e.Key == Key.Escape) && (Main.Content.GetType() == new Theory().GetType()))
                if (((Theory)Main.Content).tree.Visibility.Equals(Visibility.Visible))
                    ((Theory)Main.Content).tree.Visibility = Visibility.Hidden;
            else
                    Main.Content = new Menu();
        }

        private void MainW_Closed(object sender, EventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
                w.Close();
        }
    }
}
