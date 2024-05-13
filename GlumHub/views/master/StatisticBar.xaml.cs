using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GlumHub
{
    /// <summary>
    /// Логика взаимодействия для StatisticBar.xaml
    /// </summary>
    public partial class StatisticBar : UserControl
    {
        public StatisticBar()
        {
            InitializeComponent();

            ChartValues<double> temp = new ChartValues<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            User Master = Application.Current.Resources["User"] as User;
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                foreach(Booking booking in db.Bookings.Where(b => b.MasterId == Master.Id && b.Date_Time < DateTime.Now && b.Date_Time.Year == DateTime.Now.Year))
                {
                    temp[booking.Date_Time.Month - 1] += 1;
                }
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = temp
                    
                }
            };


            Labels = ["январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"];
            Formatter = value => value.ToString();

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}
