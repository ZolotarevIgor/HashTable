using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Diagnostics;
using static HashTable.Hash;

namespace Hash
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Page
    {
        private Add addEl;
        private Remove RemEl;
        private Search Search;
        private Statistics Stat;
        private HashPr1 _HashPr1;
        private HashPr2 _HashPr2;
        private PracticChoice PC;
        private static Random Rand = new Random();

        public List<string>[] HashTable { get; set; }

        private IHash<string> Hash;

        private static string[] WorkData = new string[] {$"{Rand.Next(1,20)}", $"{Rand.Next(1, 20)}", $"{Rand.Next(1, 20)}", $"{Rand.Next(1, 20)}", $"{Rand.Next(1, 20)}" };

        public Test()
        {
            InitializeComponent();

            Hash = CreateHashTable(WorkData);

            _HashPr2 = new HashPr2();
            _HashPr1 = new HashPr1();
            HashPr.Content = _HashPr1;
            _HashPr2.Initialize(Hash.Hash);
            _HashPr1.Initialize(Hash.AllElements, Hash.AllKeys,Hash.Size);

            addEl = new Add(this);
            RemEl = new Remove(this);
            Search = new Search(this);
            PC = new PracticChoice(this);
            Stat = Stat_Comp();

            
        }
        private Statistics Stat_Comp()
        {
            int Count = Hash.Count; //число элементов
            int ListCount = 0; //число ненулевых массивов в массиве Hash.Hash 
            int Size = Hash.Size; //размер таблицы
            Stopwatch Clock = new Stopwatch();
            double timeAll = 0, ti = 0, tmax = 0; //общее время, время i-го элемента, максимальное время поиска
            int MaxCol = 1, iLen = 1; //максимальная длина списка, длина i-го списка

            foreach (string[] i in Hash.Hash)
                foreach (string j in i)
                {
                    if (j != "Пусто") //если j элемент i массива не пустой, то
                    {
                        Clock.Start();
                        Hash.HashSearch(j); //ищем его
                        Clock.Stop();
                        ti = Clock.Elapsed.TotalMilliseconds; //запоминаем время поиска
                        Clock.Reset();
                        timeAll += ti; //и прибавляем время его поиска к общему
                        if (ti > tmax)
                            tmax = ti; //сравниваем с предыдущим максимальным временем поиска
                    }
                    iLen = i.Length; //запоминаем длину списка
                    if (iLen > MaxCol) //если больше максимальной, то переписываем максимальный
                        MaxCol = iLen;
                }
            int[] Grafics = new int[MaxCol + 1]; //массив Maxcol Х 2 - координаты для графика (х - число коллизий 0..maxcol, y - число ячеек с числом коллизий х)
            foreach (string[] i in Hash.Hash)
            {
                if (i[0] == "Пусто")
                    Grafics[0]++;
                else
                {
                    Grafics[i.Length]++;
                    ListCount++;
                }
            }
            double B = ((double)ListCount / Size) * 100; //коэффициент заполненности в %

            return new Statistics(this, Count, Size, B, timeAll / Count, tmax, Grafics);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WorkData = Hash.AllElements;
            NavigationService.Navigate(new Uri("/Menu.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!addEl.IsVisible)
            {
                addEl = new Add(this);
                addEl.Show();
            }
            else
            {
                addEl.Activate();
            }
        }

        public  void WorkButton(string key,Object Butt)
        {
            if (Butt.GetType() == typeof(Add))
                Hash.Add(key);
            if (Butt.GetType() == typeof(Remove))
                Hash.Remove(key);
            if (Butt.GetType() == typeof(Search))
            {
                Stopwatch Clock = new Stopwatch();
                string S = "Элемент ";
                string FindedKey;
                Clock.Start();
                FindedKey = Hash.HashSearch(key);
                Clock.Stop();
                if (FindedKey == key)
                    S += "найден";
                else S += "не найден";
                MessageBox.Show(S + $"\nВремя поиска - {Clock.Elapsed.TotalMilliseconds} мс", "Время поиска", MessageBoxButton.OK, MessageBoxImage.Information);

            }

            if (Butt.GetType() == typeof(PracticChoice))
            {
                Hash = CreateHashTable(WorkData);
            }
            _HashPr2.Initialize(Hash.Hash);
            _HashPr1.Initialize(Hash.AllElements, Hash.AllKeys,Hash.Size);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!RemEl.IsVisible)
            {
                RemEl = new Remove(this);
                RemEl.Show();
            }
            else
            {
                RemEl.Activate();
            }

        }

        public void ResetData()
        {
            WorkData = new string[] { };
            
        }

        private void Search_Button(object sender, RoutedEventArgs e)
        {
            if (!Search.IsVisible)
            {
                Search = new Search(this);
                Search.Show();
            }
            else
            {
                Search.Activate();
            }
        }
        private void Stat_Button(object sender, RoutedEventArgs e)
        {
            if (!Stat.IsVisible)
            {
                Stat = Stat_Comp();
                Stat.Show();
            }
            else
            {
                Stat.Activate();
            }
        }
        
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (HashPr.Content.GetType() == _HashPr2.GetType())
            {
                HashPr.Content = _HashPr1;
                MemBut.Content = "Представление в памяти";
            }
            else
            {
                HashPr.Content = _HashPr2;
                MemBut.Content = "Хеш-функция"; 
            }
            
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            if (!PC.IsVisible)
            {
                PC = new PracticChoice(this);
                WorkData = Hash.AllElements;
                PC.Show();
            }
            else
            {
                PC.Activate();
            }
        }
    }
}
