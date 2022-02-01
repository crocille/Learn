using LearnApp.Database;
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
using System.Windows.Threading;

namespace LearnApp.Pages
{
    /// <summary>
    /// Interaction logic for OrderListPage.xaml
    /// </summary>
    public partial class OrderListPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        public OrderListPage()
        {
            InitializeComponent();
            UpdateData();

            timer.Interval = TimeSpan.FromSeconds(13);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            DateTime LastDate = DateTime.Now.AddDays(2);
            DgvOrders.ItemsSource = EfModel.Init().ClientServices
                .Where(cs => cs.StartTime > DateTime.Now && cs.StartTime < LastDate)
                .OrderBy(cs => cs.StartTime).ToList();
            DgvOrders.Items.Refresh();
        }
    }
}
