using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static HashTable.Hash;

namespace Hash
{
    /// <summary> 
    /// Логика взаимодействия для HashPr2.xaml 
    /// </summary> 
    public partial class HashPr2 : Page
    {



        public HashPr2()
        {
            InitializeComponent();
        }
         
        public void Initialize(string[][] inpData)    //инициализация визуальной Хеш-таблицы
        {
            Border bord = new Border();
            Label num = new Label();
            StackPanel wstack = new StackPanel(), Datastack;
            

            double weight = 60; //знаечение ширины ячейки с номером строки (далее сожет меняться)
            if (inpData.Length != 0)
            {
                weight = (Math.Truncate(Math.Log(inpData.Length, 10))) * 5 + 60;

            }
            int MaxWeight = 1;


            StackPanel[] stacks = new StackPanel[inpData.Length]; //массив стеков со спискми

            for (int i = 0; i < inpData.Length; i++)  //проход по самссиву списков
            {
                if (inpData[i].Length > MaxWeight) // находим максимальную длину списка (для создания ровной таблицы)
                    MaxWeight = inpData[i].Length;

                stacks[i] = new StackPanel();
                stacks[i].Margin = new Thickness(10, 0, 0, 0);

                if (inpData[i][0] == "Пусто")    //проверка не пустой ли список
                {
                    bord = new Border();
                    bord.BorderThickness = new Thickness(2);
                    bord.BorderBrush = Brushes.Black;
                    bord.CornerRadius = new CornerRadius(2);
                    SolidColorBrush color = new SolidColorBrush();
                    color.Color = Colors.Salmon;
                    color.Opacity = 20;
                    bord.Background = color;
                    bord.Margin = new Thickness(5);

                    bord.Width = 200;
                    bord.VerticalAlignment = VerticalAlignment.Stretch;
                    bord.HorizontalAlignment = HorizontalAlignment.Left;


                    num = new Label();
                    num.FontSize = 29;
                    num.Content = "Пусто";
                    num.HorizontalContentAlignment = HorizontalAlignment.Center;
                    bord.Child = num;
                    stacks[i].Children.Add(bord);

                }
                else
                {
                    wstack = new StackPanel();
                    wstack.Orientation = Orientation.Horizontal;
                    for (int j = 0; j < inpData[i].Length; j++)
                    {
                        if (j >= 1)
                        {
                            num = new Label();
                            num.FontSize = 32;
                            num.Content = "→";
                            num.VerticalAlignment = VerticalAlignment.Center;
                            num.HorizontalContentAlignment = HorizontalAlignment.Center;
                            wstack.Children.Add(num);
                        }

                        Datastack = new StackPanel();
                        bord = new Border();
                        bord.BorderThickness = new Thickness(2);
                        bord.BorderBrush = Brushes.Black;
                        bord.CornerRadius = new CornerRadius(2);
                        bord.Margin = new Thickness(5);
                        bord.Width = 200;
                        bord.VerticalAlignment = VerticalAlignment.Stretch;

                        num = new Label();
                        num.FontSize = 29;
                        num.Content = inpData[i][j];
                        num.HorizontalContentAlignment = HorizontalAlignment.Center;
                        bord.Child = num;

                        wstack.Children.Add(bord);
                    } //сам список
                    stacks[i].Children.Add(wstack); //добавление к панели
                }

            }



            StackPanel[] outData = new StackPanel[stacks.Length+2];
            outData[0] = new StackPanel();

            num = new Label();
            num.FontSize = 30;
            num.Width = MaxWeight * 240 + 20 + weight;
            num.HorizontalContentAlignment = HorizontalAlignment.Center;
            num.VerticalContentAlignment = VerticalAlignment.Center;
            num.FontWeight = FontWeights.Bold;
            num.Content = "Хеш-таблица";
            outData[0].Children.Add(num);

            outData[1] = new StackPanel();
            outData[1].Orientation = Orientation.Horizontal;

            bord = new Border();
            bord.BorderThickness = new Thickness(1,1,1,3);
            bord.BorderBrush = Brushes.Black;
            bord.Width = (nTypeHash == TypeHash.Open ? weight : 190); //

            num = new Label();
            num.FontSize = 26;
            num.HorizontalAlignment = HorizontalAlignment.Stretch;
            num.VerticalAlignment = VerticalAlignment.Stretch;
            num.HorizontalContentAlignment = HorizontalAlignment.Center;
            num.VerticalContentAlignment = VerticalAlignment.Center;
            num.Content = nTypeHash == TypeHash.Open ?  "h(k)":"Номер ячейки";
            num.FontStyle = nTypeHash == TypeHash.Open ? FontStyles.Italic : FontStyles.Normal;

            bord.Child = num;
            outData[1].Children.Add(bord);

            num = new Label();
            num.FontSize = 26;
            num.HorizontalAlignment = HorizontalAlignment.Stretch;
            num.VerticalAlignment = VerticalAlignment.Stretch;
            num.HorizontalContentAlignment = HorizontalAlignment.Center;
            num.VerticalContentAlignment = VerticalAlignment.Center;
            num.Content = nTypeHash == TypeHash.Open ? "Цепочки ключей" : "Ячейка"; // создание заголовка


            bord = new Border();
            bord.BorderThickness = new Thickness(1, 1, 1, 3);
            bord.BorderBrush = Brushes.Black;
            bord.Width = MaxWeight * 240 + 20;
            bord.Child = num;
            outData[1].Children.Add(bord);
            outData[1].Margin = new Thickness(0, 0, 0, 10);
            

            for (int i = 2; i < outData.Length; i++) // сама таблица, состоит из бардюра с номером строки, и бардюра со списком значений
            {
                outData[i] = new StackPanel();
                outData[i].Orientation = Orientation.Horizontal;

                bord = new Border();
                bord.BorderThickness = new Thickness(1);
                bord.BorderBrush = Brushes.Black;
                bord.Width = (nTypeHash == TypeHash.Open ? weight : 190); ;

                num = new Label();
                num.FontSize = 26;
                num.HorizontalAlignment = HorizontalAlignment.Stretch;
                num.VerticalAlignment = VerticalAlignment.Stretch;
                num.HorizontalContentAlignment = HorizontalAlignment.Center;
                num.VerticalContentAlignment = VerticalAlignment.Center;
                num.Content = (i - 2).ToString();

                bord.Child = num;
                outData[i].Children.Add(bord);

                bord = new Border();
                bord.BorderThickness = new Thickness(1);
                bord.BorderBrush = Brushes.Black;
                bord.Width = MaxWeight * 240 + 20;
                bord.Child = stacks[i-2];
                outData[i].Children.Add(bord);
                

            }
            HashTableList.ItemsSource = outData;
        }
    }
}