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
    /// Interaction logic for AdminServiceListPage.xaml
    /// </summary>
    public partial class AdminServiceListPage : Page
    {

        bool IsDataUpdate = false;
        public AdminServiceListPage()
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

        private void UpdateData()
        {
            IEnumerable<Service> services = EfModel.Init().Services
                .Where(s => s.Title.Contains(TbSearch.Text) || s.Description.Contains(TbSearch.Text));
            switch (CbSort.SelectedIndex)
            {
                case 0:
                    services = services.OrderBy(s => s.DiscountPrice);
                    break;
                case 1:
                    services = services.OrderByDescending(s => s.DiscountPrice);
                    break;
            }


            switch (CbFilter.SelectedIndex)
            {
                case 1:
                    services = services.Where(s => !s.IsDiscount || (s.Discount >= 0 && s.Discount < 5));
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

        private void BtRemoveClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Service service = button.DataContext as Service;
            if (service.ClientServices.Count == 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить услугу " + service.Title + "?", "Удаление услуги", MessageBoxButton.YesNo)
                    == MessageBoxResult.Yes)
                {
                    foreach (ServicePhoto photo in service.ServicePhotoes.ToList())
                    {
                        EfModel.Init().ServicePhotoes.Remove(photo);
                    }
                    EfModel.Init().Services.Remove(service);
                    EfModel.Init().SaveChanges();

                    UpdateData();
                }
            }
            else
            {
                MessageBox.Show("Удаление услуги невозможно! На услугу существуют записи!");
            }
        }

        private void BtEditClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Service service = button.DataContext as Service;
            NavigationService.Navigate(new ServiceAddPage(service));
        }

        private void BtAddClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServiceAddPage(new Service()));
        }

        private void BtOrderListClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderListPage());
        }

        private void TbSearchChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void CbSortChange(object sender, SelectionChangedEventArgs e)
        {
            if (IsDataUpdate)
                UpdateData();
        }

        private void PageVisChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateData();
        }
    }
}
