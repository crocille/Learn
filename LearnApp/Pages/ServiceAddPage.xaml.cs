using LearnApp.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
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
    /// Interaction logic for ServiceAddPage.xaml
    /// </summary>
    public partial class ServiceAddPage : Page
    {
        Service service;
        public ServiceAddPage(Service service)
        {
            this.service = service;
            InitializeComponent();
            DataContext = service;
        }

        private void BtSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (service.DurationInSeconds < 0 && service.DurationInSeconds > 60 * 60 * 4)
                {
                    MessageBox.Show("Время не может быть отрицательным, или привышать 4 часа.");
                    return;
                }
                if (service.ID == 0)
                    EfModel.Init().Services.Add(service);
                EfModel.Init().SaveChanges();
                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(string.Join(Environment.NewLine, ex.EntityValidationErrors.Last().ValidationErrors.Select(ve => ve.ErrorMessage)));
            }
        }

        private void BtImageClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JPG files|*.jpg";
            if(openFile.ShowDialog() == true)
            {
                service.MainImage = File.ReadAllBytes(openFile.FileName);
            }
        }
    }
}
