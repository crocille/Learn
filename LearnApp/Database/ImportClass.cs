using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApp.Database
{
    static class ImportClass
    {
        public static void ImportClient() {
            String[] clientsStr = File.ReadAllLines(@"C:\Users\AlexDev\Desktop\Task_09\Сессия 1\client_s_import.txt");
            foreach(string line in clientsStr.Skip(1))
            {
                string[] items = line.Split(',').Select(i=>i.Trim()).ToArray();
                string fio = items[0] + " " + items[1] + " " + items[2];
                fio  = fio.Replace(",", "");
                string[] fioArr = fio.Split(' ');

                Client client = new Client
                {
                    FirstName = fioArr[0],
                    LastName = fioArr[1],
                    Patronymic = fioArr[2],
                    Phone = items[4],
                    GenderCode = items[3],
                    Birthday = DateTime.Parse(items[5]),
                    Email = items[6],
                    RegistrationDate = DateTime.Parse(items[7])
                };

                EfModel.Init().Clients.Add(client);
            }
            EfModel.Init().SaveChanges();
        }

        public static void ImportServices() {
            string[] lines = File.ReadAllLines(@"C:\Users\AlexDev\Desktop\Task_09\Сессия 1\service_s_import.csv");
            foreach(string line in lines.Skip(1))
            {
                string[] items = line.Split(',').Select(i => i.Trim()).ToArray();
                Service service = new Service
                {
                    Title = items[0],
                    DurationInSeconds = Convert.ToInt32(items[2]),
                    Cost = Convert.ToDecimal(items[3])
                };

                try
                {
                    service.MainImage = File.ReadAllBytes(@"C:\Users\AlexDev\Desktop\Task_09\Сессия 1\" + items[1]);
                }
                catch
                {
                    Console.WriteLine(items[1]);
                }

                try
                {
                    service.Discount = Convert.ToDouble(items[4]);
                }
                catch
                {
                    Console.WriteLine(items[4]);
                }

                EfModel.Init().Services.Add(service);

            }
            EfModel.Init().SaveChanges();
        }

        public static void ImportClientService()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\AlexDev\Desktop\Task_09\Сессия 1\serviceclient_s_import.csv");
            foreach (string line in lines.Skip(1))
            {
                string[] items = line.Split(';').Select(i => i.Trim()).ToArray();

                string ServiceTitle = items[0];
                Service service = EfModel.Init().Services.FirstOrDefault(s => s.Title == ServiceTitle);
                string ClientFirstName = items[2];
                Client client = EfModel.Init().Clients.ToArray().FirstOrDefault(s => s.FirstName == ClientFirstName);

                ClientService clientService = new ClientService
                {
                    Service = service,
                    Client = client,
                    StartTime = DateTime.Parse(items[1])
                };
                EfModel.Init().ClientServices.Add(clientService);
            }
            EfModel.Init().SaveChanges();
        }
    }
}
