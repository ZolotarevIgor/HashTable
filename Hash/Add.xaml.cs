using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;

namespace Hash
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>

    public partial class Add : Window
    {
        public Test Work { get; set; }
        public int Diam { get; set; }
        private List<string> AddValue = new List<string>();
        private bool[] indexUse;

        public Add(Test th)
        {
            InitializeComponent();
            Diam = 10;
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;


            Top = (screenHeight - 2 * Height) / 2;
            Left = (screenWidth - 2 * Width) / 2;
            
            Work = th;
            ManyAdd(1);

            SuperSlider3000.Value = 10000;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in StackPan.Children)
            {
                if (((TextBox)item).Text != "")
                {
                    Work.WorkButton(((TextBox)item).Text, this);
                }
            }

            Close();
        }

        private void Add_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private void Txtb_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.D0 <= e.Key && Key.D9 >= e.Key)
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;
        }
        private void Txtb_KeyUp(object sender, KeyEventArgs e)
        {
            int a;
            
            if (e.Key == Key.Enter)
            {
                a = StackPan.Children.IndexOf(sender as TextBox);
                if ((a + 1) < StackPan.Children.Count)
                    ((TextBox)StackPan.Children[a + 1]).Focus();
                else
                    Button_Click(sender, e);
                e.Handled = true;
                return;
            }

            if (!((e.Key == Key.Back || e.Key == Key.Delete) && ((TextBox)sender).Text.Length <= 1))
            {
                indexUse[(int)((TextBox)sender).Tag] = true;
            }
            e.Handled = true;
        }

        private void AddMany_Click(object sender, RoutedEventArgs e)
        {
            if (AddMany.IsChecked == false)
                ManyAdd(1);
            else
                try
                {
                    ManyAdd(int.Parse(AddCount.Text));
                }
                catch
                {
                    ManyAdd(1);
                }
            AddCount.IsEnabled = !AddCount.IsEnabled;
        }

        private void AddCount_KeyDown(object sender, KeyEventArgs e)
        {
            int Count;

            AddCount.SelectionStart = AddCount.Text.Length;
            if (Key.D0 <= e.Key && Key.D9 >= e.Key)
            {
                e.Handled = false;
                if (AddCount.Text != "")
                {
                    Count = int.Parse(AddCount.Text + new KeyConverter().ConvertToString(e.Key));
                    if (Count > 100)
                    {
                        AddCount.Text = "100";
                        AddCount.SelectionStart = AddCount.Text.Length;
                        ManyAdd(100);
                        e.Handled = true;
                    }
                    else
                    ManyAdd(Count,false);
                    return;
                }
                ManyAdd(int.Parse(new KeyConverter().ConvertToString(e.Key)),false);
                return;
            }

            e.Handled = true;

        }

        private void ManyAdd(int Count,bool KeyToFoc = true)
        {
            foreach (TextBox item in StackPan.Children)
                if (item.Text != "")
                    AddValue.Add(item.Text);
            switch (Count)
            {
                case 1:
                    Height = 300;
                    BorderAdd.BorderThickness = new Thickness(0);
                    Scrolltxb.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    break;

                case 2:
                    Height = 350;
                    BorderAdd.BorderThickness = new Thickness(0);
                    Scrolltxb.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    break;
                default:
                    Height = 390;
                    BorderAdd.BorderThickness = new Thickness(1);
                    Scrolltxb.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    break;
            }
            for (int i = 0; i < StackPan.Children.Count;)
                StackPan.Children.RemoveAt(0);

            TextBox TB;
            Thickness margin = new Thickness();
            margin.Top = margin.Bottom = 5;
            margin.Left = margin.Right = 10;

            for (int i = 0; i < Count; i++)
            {
                TB = new TextBox
                {
                    Text = "",
                    Height = 35,
                    FontSize = 18,
                    Margin = margin
                };
                TB.KeyUp += Txtb_KeyUp;
                TB.KeyDown += Txtb_KeyDown;
                TB.VerticalContentAlignment = VerticalAlignment.Center;
                TB.Tag = i;

                StackPan.Children.Add(TB);
            }

            for (int i = 0; i < StackPan.Children.Count && i < AddValue.Count; i++)
                ((TextBox)StackPan.Children[i]).Text = AddValue[i];
            AddValue.RemoveAll((x) => true);

            indexUse = new bool[StackPan.Children.Count];

            for (int i = 0; i < indexUse.Length; i++)
                indexUse[i] = false;

            for (int i = 0; i < indexUse.Length; i++)
                if (((TextBox)StackPan.Children[i]).Text != "")
                    indexUse[i] = true;

            if (FromBase.IsChecked == true)
                AddFromBase();

            if (KeyToFoc)
                StackPan.Children[0].Focus();
        }

        private void AddFromBase()
        {
            Random rand = new Random();
            if (FromBase.IsChecked == true)
                for (int i = 0; i < indexUse.Length; i++)
                    if (!indexUse[i])
                        ((TextBox)StackPan.Children[i]).Text = rand.Next(0, Diam).ToString();
         
        }


        private void FromBase_Click(object sender, RoutedEventArgs e)
        {
            if (FromBase.IsChecked == true)
            {
                SuperSlider3000.IsEnabled = true;
                DiamL.IsEnabled = true;

                indexUse = new bool[StackPan.Children.Count];

                for (int i = 0; i < indexUse.Length; i++)
                    indexUse[i] = false;

                for (int i = 0; i < indexUse.Length; i++)
                    if (((TextBox)StackPan.Children[i]).Text != "")
                        indexUse[i] = true;

                AddFromBase();
            }
            else
            {
                SuperSlider3000.IsEnabled = false;
                DiamL.IsEnabled = false;

                for (int i = 0; i < StackPan.Children.Count; i++)
                    if (!indexUse[i])
                        ((TextBox)StackPan.Children[i]).Text = "";
            }
        }

        private void SuperSlider3000_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Diam = (int)SuperSlider3000.Value;
            DiamL.Content = $"Выберите диапазон чисел: {Diam}";
        }

        private void SuperSlider3000_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AddFromBase();
        }
    }
}
