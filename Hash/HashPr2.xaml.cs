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

                stacks[i] = new StackPanel
                {
                    Margin = new Thickness(10, 0, 0, 0)
                };

                if (inpData[i][0] == "Пусто")    //проверка не пустой ли список
                {
                    bord = new Border
                    {
                        BorderThickness = new Thickness(2),
                        BorderBrush = Brushes.Black,
                        CornerRadius = new CornerRadius(2)
                    };
                    SolidColorBrush color = new SolidColorBrush
                    {
                        Color = Colors.Salmon,
                        Opacity = 20
                    };
                    bord.Background = color;
                    bord.Margin = new Thickness(5);

                    bord.Width = 200;
                    bord.VerticalAlignment = VerticalAlignment.Stretch;
                    bord.HorizontalAlignment = HorizontalAlignment.Left;


                    num = new Label
                    {
                        FontSize = 29,
                        Content = "Пусто",
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };
                    bord.Child = num;
                    stacks[i].Children.Add(bord);

                }
                else
                {
                    wstack = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };
                    for (int j = 0; j < inpData[i].Length; j++)
                    {
                        if (j >= 1)
                        {
                            num = new Label
                            {
                                FontSize = 32,
                                Content = "→",
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalContentAlignment = HorizontalAlignment.Center
                            };
                            wstack.Children.Add(num);
                        }

                        Datastack = new StackPanel();
                        bord = new Border
                        {
                            BorderThickness = new Thickness(2),
                            BorderBrush = Brushes.Black,
                            CornerRadius = new CornerRadius(2),
                            Margin = new Thickness(5),
                            Width = 200,
                            VerticalAlignment = VerticalAlignment.Stretch
                        };

                        num = new Label
                        {
                            FontSize = 29,
                            Content = inpData[i][j],
                            HorizontalContentAlignment = HorizontalAlignment.Center
                        };
                        bord.Child = num;

                        wstack.Children.Add(bord);
                    } //сам список
                    stacks[i].Children.Add(wstack); //добавление к панели
                }

            }



            StackPanel[] outData = new StackPanel[stacks.Length+2];
            outData[0] = new StackPanel();

            num = new Label
            {
                FontSize = 30,
                Width = MaxWeight * 240 + 20 + weight,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Content = "Хеш-таблица"
            };
            outData[0].Children.Add(num);

            outData[1] = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            bord = new Border
            {
                BorderThickness = new Thickness(1, 1, 1, 3),
                BorderBrush = Brushes.Black,
                Width = nTypeHash == TypeHash.Open ? weight : 190
            };

            num = new Label
            {
                FontSize = 26,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Content = nTypeHash == TypeHash.Open ? "h(k)" : "Номер ячейки",
                FontStyle = nTypeHash == TypeHash.Open ? FontStyles.Italic : FontStyles.Normal
            };

            bord.Child = num;
            outData[1].Children.Add(bord);

            num = new Label
            {
                FontSize = 26,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Content = nTypeHash == TypeHash.Open ? "Цепочки ключей" : "Ячейка" // создание заголовка
            };


            bord = new Border
            {
                BorderThickness = new Thickness(1, 1, 1, 3),
                BorderBrush = Brushes.Black,
                Width = MaxWeight * 240 + 20,
                Child = num
            };
            outData[1].Children.Add(bord);
            outData[1].Margin = new Thickness(0, 0, 0, 10);
            

            for (int i = 2; i < outData.Length; i++) // сама таблица, состоит из бардюра с номером строки, и бардюра со списком значений
            {
                outData[i] = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                bord = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    Width = nTypeHash == TypeHash.Open ? weight : 190
                };

                num = new Label
                {
                    FontSize = 26,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Content = (i - 2).ToString()
                };

                bord.Child = num;
                outData[i].Children.Add(bord);

                bord = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    Width = MaxWeight * 240 + 20,
                    Child = stacks[i - 2]
                };
                outData[i].Children.Add(bord);
                

            }
            HashTableList.ItemsSource = outData;
        }
    }
}