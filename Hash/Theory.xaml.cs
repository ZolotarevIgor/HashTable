using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.IO;
using System.Windows.Xps.Packaging;
using System.Windows.Input;

namespace Hash
{
    /// <summary>
    /// Логика взаимодействия для Theory.xaml
    /// </summary>
    public partial class Theory : Page
    {
        public Theory()
        {
            InitializeComponent();
            XpsDocument doc = new XpsDocument(@"Теория\Хеширование.xps", FileAccess.Read);
            docViewer.Document = doc.GetFixedDocumentSequence();
            doc.Close();
            docViewer.Focus();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (tree.Visibility == Visibility.Visible)
            {
                TransformGroup tg = new TransformGroup();
                RotateTransform rt = new RotateTransform(180);
                tg.Children.Add(rt);
                img.RenderTransform = tg;

                tree.Visibility = Visibility.Hidden;
            }
            else
            {
                TransformGroup tg = new TransformGroup();
                RotateTransform rt = new RotateTransform(0);
                tg.Children.Add(rt);
                img.RenderTransform = tg;
                tree.Visibility = Visibility.Visible;
            }
        }

       

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem mytree = (TreeViewItem)e.Source;
            XpsDocument doc = new XpsDocument(@"Теория\Хеширование.xps", FileAccess.Read);
            switch (mytree.Header)
            {
                case "Хеширование":
                    doc = new XpsDocument(@"Теория\Хеширование.xps", FileAccess.Read);
                    break;
                case "Хеш-таблица":
                    {
                        doc = new XpsDocument(@"Теория\Хеш-таблица.xps", FileAccess.Read);
                        docViewer.Focus();
                    }
                    break;
                case "Свойства хеш-таблиц":
                    doc = new XpsDocument(@"Теория\Свойства хеш.xps", FileAccess.Read);
                    break;
                case "Хеш-функции":
                    doc = new XpsDocument(@"Теория\Хеш.xps", FileAccess.Read);
                    break;
                case "Алгоритмы хеширования":
                case "Метод остатков от деления":
                    doc = new XpsDocument(@"Теория\Метод остатков от деления.xps", FileAccess.Read);
                    break;
                case "Метод середины квадрата":
                    doc = new XpsDocument(@"Теория\Метод функции середины квадрата.xps", FileAccess.Read);
                    break;
                case "Мультипликативный метод":
                    doc = new XpsDocument(@"Теория\Метод умножения.xps", FileAccess.Read);
                    break;
                case "Открытое хеширование":
                    doc = new XpsDocument(@"Теория\Открытое хеширование.xps", FileAccess.Read);
                    break;
                case "Закрытое хеширование":
                    doc = new XpsDocument(@"Теория\Закрытое хеширование.xps", FileAccess.Read);
                    break;
                case "Методы повторного хеширования":
                    doc = new XpsDocument(@"Теория\Методы повторного хеширования.xps", FileAccess.Read);
                    break;
            }
            docViewer.Document = doc.GetFixedDocumentSequence();
            doc.Close();
            docViewer.Focus();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
         
            NavigationService.Navigate(new Uri("/Menu.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            tree.Visibility = Visibility.Hidden;
        }

        private void DocViewer_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Button_Click(sender,new RoutedEventArgs());
        }

        private void DocViewer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (true)
                Button_Click(sender, e);
        }
    }
}
