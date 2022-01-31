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
        public AdminServiceListPage()
        {
            InitializeComponent();
        }

        private void BtRemoveClick(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить услугу?", "Удаление услуги", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                MessageBox.Show("Функционал в разработке");
            }
        }

        private void BtEditClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServiceAddPage());
        }

        private void BtAddClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServiceAddPage());
        }

        private void BtOrderListClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderListPage());
        }
    }
}
