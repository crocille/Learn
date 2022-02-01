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

namespace LearnApp.Pages
{
    /// <summary>
    /// Interaction logic for ServiceListPage.xaml
    /// </summary>
    public partial class ServiceListPage : Page
    {
        bool IsDataUpdate = false;
        public ServiceListPage()
        {
            InitializeComponent();
            CbSort.Items.Add("По возрастанию цены");
            CbSort.Items.Add("По убывнию цены");
            CbSort.SelectedIndex = 0;

            CbFilter.Items.Add("Все");
            CbFilter.Items.Add("от 0 до 5%");
            CbFilter.Items.Add("от 5% до 15%,");
            CbFilter.Items.Add("от 15 до 30%");
            CbFilter.Items.Add("от 30 до 70%");
            CbFilter.Items.Add("от 70 до 100%");
            CbFilter.SelectedIndex = 0;

            IsDataUpdate = true;

            UpdateData();
        }

        private void UpdateData() {
            IEnumerable<Service> services = EfModel.Init().Services
                .Where(s => s.Title.Contains(TbSearch.Text) || s.Description.Contains(TbSearch.Text));
            switch (CbSort.SelectedIndex)
            {
                case 0:
                    services = services.OrderBy(s=>s.DiscountPrice);
                    break;
                case 1:
                    services = services.OrderByDescending(s => s.DiscountPrice);
                    break;
            }


            switch (CbFilter.SelectedIndex)
            {
                case 1:
                    services = services.Where(s => !s.IsDiscount || (s.Discount>=0 && s.Discount<5));
                    break;
                case 2:
                    services = services.Where(s => s.Discount >= 5 && s.Discount < 15);
                    break;
                case 3:
                    services = services.Where(s => s.Discount >= 15 && s.Discount < 30);
                    break;
                case 4:
                    services = services.Where(s => s.Discount >= 30 && s.Discount < 70);
                    break;
                case 5:
                    services = services.Where(s => s.Discount >= 70 && s.Discount < 100);
                    break;
            }

            LvServices.ItemsSource = services.ToList();

            if (LvServices.Items.Count == 0)
            {
                TbNotFound.Visibility = Visibility.Visible;
            }
            else
            {
                TbNotFound.Visibility = Visibility.Hidden;
            }

            TbResultCount.Text = LvServices.Items.Count + " из " + EfModel.Init().Services.Count();

        }

        private void btOrderClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Service service = button.DataContext as Service;
            NavigationService.Navigate(new ServiceAddOrderPage(service));
        }

        private void TbSearchChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void CbSortChange(object sender, SelectionChangedEventArgs e)
        {
            if(IsDataUpdate)
                UpdateData();
        }
    }
}
