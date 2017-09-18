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
                    stacks[i+1] = new StackPanel();
                    
                    stacks[i+1].Orientation = Orientation.Horizontal;
                    



                    txtb = new Label();
                    txtb.Content = values[i];
                    txtb.Width = 300;
                    txtb.FontSize = 20;
                    txtb.HorizontalContentAlignment = HorizontalAlignment.Center;
                    txtb.VerticalContentAlignment = VerticalAlignment.Center;
                    stacks[i+1].Children.Add(txtb);

                    txtb = new Label();
                    txtb.Content = "→";
                    txtb.Width = 80;
                    txtb.FontSize = 40;
                    txtb.VerticalContentAlignment = VerticalAlignment.Center;
                    txtb.HorizontalContentAlignment = HorizontalAlignment.Center;
                    stacks[i+1].Children.Add(txtb);

                    txtb = new Label();
                    txtb.Content = keys[i];
                    txtb.Width = 300;
                    txtb.FontSize = 20;
                    txtb.VerticalContentAlignment = VerticalAlignment.Center;
                    txtb.HorizontalContentAlignment = HorizontalAlignment.Center;
                    stacks[i+1].Children.Add(txtb);

                }

                stacks[0] = new StackPanel();

                stacks[0].Orientation = Orientation.Horizontal;

                txtb = new Label();
                txtb.Content = "Исходное множество";
                txtb.Width = 340;
                txtb.FontSize = 23;
                txtb.HorizontalContentAlignment = HorizontalAlignment.Center;
                txtb.FontWeight = FontWeights.Bold;
                stacks[0].Children.Add(txtb);

                txtb = new Label();
                txtb.Content = "h(k)";
                txtb.Width = 80;
                txtb.FontSize = 18;
                txtb.FontStyle = FontStyles.Italic;
                txtb.HorizontalContentAlignment = HorizontalAlignment.Center;
                txtb.VerticalContentAlignment = VerticalAlignment.Center;
                stacks[0].Children.Add(txtb);

                txtb = new Label();
                txtb.Content = "Множество хеш-функции";
                txtb.Width = 340;
                txtb.FontSize = 23;
                txtb.HorizontalContentAlignment = HorizontalAlignment.Center;
                txtb.FontWeight = FontWeights.Bold;
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
                    tempS += $"h(K) = [M * ((C * K) mod 1)], где \nM=2^{System.Math.Log(Size, 2.0)} - ближайшая степень двойки к размеру исходного множества, \nС = (√5 - 1)/2 ≈ 0,6180339887 - золотое сечение, mod - остаток от деления, \n[] - операция взятия целой части ";
                    break;
                case HashFunction.Center:
                    tempS += $"h(K) = (средние m битов числа K^2), где \n m={System.Math.Log(Size, 2.0)}";
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
            FirstRow.Height = new GridLength(30*((int)tempS.Length/70 + 1));
            return tempS;
        }
    }
}
