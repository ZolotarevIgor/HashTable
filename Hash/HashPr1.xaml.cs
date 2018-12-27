using System.Windows;
using System;
using System.Windows.Controls;
using static HashTable.Hash;

namespace Hash
{
    /// <summary>
    /// Логика взаимодействия для HashPr1.xaml
    /// </summary>

    public partial class HashPr1 : Page
    {
        int Size;
        public HashPr1()
        {
            InitializeComponent();
            TopLabel.Content = TopLabelText();
        }

        public void Initialize(string[] values ,int[] keys, int Size)
        {
            this.Size = Size;
            TopLabel.Content = TopLabelText();

            if (values.Length == keys.Length)
            {
                Label txtb;
                
                StackPanel[] stacks = new StackPanel[values.Length+1];

                
                for (int i = 0; i < values.Length; i++)             
                {
                    stacks[i + 1] = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };

                    txtb = new Label
                    {
                        Content = values[i],
                        Width = 300,
                        FontSize = 20,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center
                    };
                    stacks[i+1].Children.Add(txtb);

                    txtb = new Label
                    {
                        Content = "→",
                        Width = 80,
                        FontSize = 40,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };
                    stacks[i+1].Children.Add(txtb);

                    txtb = new Label
                    {
                        Content = keys[i],
                        Width = 300,
                        FontSize = 20,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };
                    stacks[i+1].Children.Add(txtb);

                }

                stacks[0] = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                txtb = new Label
                {
                    Content = "Исходное множество",
                    Width = 340,
                    FontSize = 23,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    FontWeight = FontWeights.Bold
                };
                stacks[0].Children.Add(txtb);

                txtb = new Label
                {
                    Content = "h(k)",
                    Width = 80,
                    FontSize = 18,
                    FontStyle = FontStyles.Italic,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                stacks[0].Children.Add(txtb);

                txtb = new Label
                {
                    Content = "Множество хеш-функции",
                    Width = 340,
                    FontSize = 23,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    FontWeight = FontWeights.Bold
                };
                stacks[0].Children.Add(txtb);

                list.ItemsSource = stacks;
                
            }
        }

        private string TopLabelText()
        {
            string tempS = "";
            switch (nTypeHash)
            {
                case TypeHash.Closed:
                    tempS += "Закрытое";
                    break;
                case TypeHash.Open:
                    tempS += "Открытое";
                    break;
            }
            tempS += " хеширование, хеш-функция - ";
            switch (nOpenHashType)
            {
                case HashFunction.Module:
                    tempS += $"h(K) = K mod m, где \nm = {Size} - ближайшее простое число к размеру исходного множества, \nmod - остаток от деления";
                    break;
                case HashFunction.Mult:
                    tempS += $"h(K) = [M * ((C * K) mod 1)], где \nM=2^{Math.Log(Size, 2.0)} - ближайшая степень двойки к размеру исходного множества, \nС = (√5 - 1)/2 ≈ 0,6180339887 - золотое сечение, mod - остаток от деления, \n[] - операция взятия целой части ";
                    break;
                case HashFunction.Center:
                    tempS += $"h(K) = (средние m битов числа K^2), где \n m={Math.Log(Size, 2.0)}";
                    break;
            }
            if (nTypeHash == TypeHash.Closed)
            {
                tempS += "\nПоиск свободных мест ";
                switch (nClosedHashType)
                {
                    case ClosedHashType.Double:
                        tempS += "методом двойного хеширования, адрес=h(x)+i*h2(x)";
                        break;
                    case ClosedHashType.Linear:
                        tempS += "линейным опробованием, адрес=h(x)+c*i";
                        break;
                    case ClosedHashType.Square:
                        tempS += "квадратичным опробованием, адрес=h(x)+c*i+d*i^2";
                        break;
                }
            }
            FirstRow.Height = new GridLength(30*(tempS.Length / 70 + 1));
            return tempS;
        }
    }
}
