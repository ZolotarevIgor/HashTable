using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Hash
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>


    public partial class Menu : Page
    {
        private Author author; 
        public Menu()
        {
            InitializeComponent();
            Practic.Focus();

            author = new Author();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/Theory.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Test.xaml", UriKind.Relative));
        }
        

        private void Menu1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click_1(sender, e);
        }

        private void Practic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click_1(sender, e);
        }

        private void Authors_Click(object sender, RoutedEventArgs e)
        {
            if (!author.IsVisible)
            {
                author = new Author();
                
                author.Show();
            }
            else
            {
                author.Activate();
            }
        }

        
    }
}
