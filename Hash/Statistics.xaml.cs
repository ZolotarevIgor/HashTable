using System.Windows;
using System.Windows.Input;
using System.Windows.Forms.DataVisualization.Charting;

namespace Hash
{
    /// <summary>
    /// Логика взаимодействия для Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public Test Work { get; set; }

        public Statistics(Test th, int Count, int Size, double B, double meanTime, double maxTime, int[] Graph)
        {
            InitializeComponent();
            Work = th;
            Focus();
            L01.Content = Count.ToString();
            L11.Content = Size.ToString();
            L21.Content = B.ToString("##.##") + "%";
            L31.Content = meanTime.ToString("0.####") + " мс";
            L41.Content = maxTime.ToString("0.####") + " мс";
            // Все графики находятся в пределах области построения ChartArea, создадим ее
            chart.ChartAreas.Add(new ChartArea("Default"));
            if (Graph == null || Graph.Length <= 2)
            {
                Height = 300;
                G2.Height = 1;
                return;

            }
            // Добавим линию, и назначим ее в ранее созданную область "Default"
            chart.Series.Add(new Series("Series1"));
            chart.Series["Series1"].ChartArea = "Default";
            chart.Series["Series1"].ChartType = SeriesChartType.Line;
            int Glen = 0, max = 0;
            // добавим данные линии
            for (int i = 0; i < Graph.Length; i++)
                if (Graph[i] != 0)
                    Glen++;
            int[] axisXData = new int[Glen + 1];
            int[] axisYData = new int[Glen + 1];
            int j = 0;
            for (int i = 0; i < Graph.Length; i++)
            {
                if (Graph[i] != 0)
                {
                    axisXData[j] = i;
                    axisYData[j] = Graph[i];
                    j++;
                    if (Graph[i] > max)
                        max = Graph[i];
                }
            }
            axisXData[j] = axisXData[j - 1] + 1;
            axisYData[j] = 0;
            chart.Series["Series1"].Points.DataBindXY(axisXData, axisYData);
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.Maximum = axisXData[axisXData.Length - 1];
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = max;
            chart.Series["Series1"].BorderWidth = 4;
            chart.Series["Series1"].Color = System.Drawing.Color.BlueViolet;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Enter)
                Close();
        }
    }
}
