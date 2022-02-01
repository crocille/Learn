using LearnApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for ServiceAddOrderPage.xaml
    /// </summary>
    public partial class ServiceAddOrderPage : Page
    {
        ClientService clientService;
        public ServiceAddOrderPage(Service service)
        {
            clientService = new ClientService { Service = service };
            InitializeComponent();
            DataContext = clientService;
            CbClient.ItemsSource = EfModel.Init().Clients.ToList();
        }

        private void BtAddOrderClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (clientService.ID == 0)
                    EfModel.Init().ClientServices.Add(clientService);
                EfModel.Init().SaveChanges();
                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(string.Join(Environment.NewLine, ex.EntityValidationErrors.Last().ValidationErrors.Select(ve => ve.ErrorMessage)));
            }
        }
    }
}
