using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static HashTable.Hash;

namespace Hash
{
    /// <summary>
    /// Логика взаимодействия для PracticChoice.xaml
    /// </summary>
    public partial class PracticChoice : Window
    {
        Test Parant;

        public PracticChoice(Test obj)
        {
            InitializeComponent();

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;


            Top = (screenHeight - 2 * Height) / 2;
            Left = (screenWidth - 2 * Width) / 2;


            Parant = obj;
            

            HashfCB.ItemsSource = new string[] { "Основанная на делении", "Мультипликативная",  "Выделения середины квадрата" };

            typeCB.SelectedIndex = (int)nTypeHash;
            HashfCB.SelectedIndex = (int)nOpenHashType;
            
            if (nTypeHash == TypeHash.Open)
            {
                CB3.SelectedIndex = -1;
                CB3.IsEnabled = L3.IsEnabled = false;
            }
            else
            {
                CB3.SelectedIndex = (int)nClosedHashType;
                CB3.IsEnabled = L3.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            nTypeHash = typeCB.Text == "Открытое" ? TypeHash.Open : TypeHash.Closed;
            TypeHash qqq = nTypeHash;

            switch (HashfCB.Text)
            {
                case "Мультипликативная":
                    nOpenHashType = HashFunction.Mult;
                    break;
                case "Основанная на делении":
                    nOpenHashType = HashFunction.Module;
                    break;
                case "Выделения середины квадрата":
                    nOpenHashType = HashFunction.Center;
                    break;
            }

            switch (CB3.Text)
            {
                case "Линейного опробования":
                    {
                        nClosedHashType = ClosedHashType.Linear;

                    }
                    break;
                case "Двойного хеширования":
                    {
                        nClosedHashType = ClosedHashType.Double;
                    }

                    break;
                case "Квадратичного опробования":
                    {
                        nClosedHashType = ClosedHashType.Square;
                    }
                    break;
            }

            if (NewTable.IsChecked == true)
            {
                (new Test()).ResetData();
            }

            Parant.WorkButton("",this);
            Close();
            
        }

       

        private void TypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeCB.Text != "Открытое")
            {
                CB3.SelectedIndex = -1;
                CB3.IsEnabled = L3.IsEnabled = false;
            }
            else
            {
                CB3.SelectedIndex = (int)nClosedHashType;
                CB3.IsEnabled = L3.IsEnabled = true;
            }

        }

        private void Practic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Practic.Close();
            if (e.Key == Key.Enter)
                Button_Click(sender, e);
        }

        private void NewTable_Click(object sender, RoutedEventArgs e)
        {
            CreateButton.Content = ((bool)NewTable.IsChecked) ?  "Создать" : "Применить настройки";
        }
    }
}
