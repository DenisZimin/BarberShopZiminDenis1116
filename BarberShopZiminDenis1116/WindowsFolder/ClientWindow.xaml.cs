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
using System.Windows.Shapes;
using BarberShopZiminDenis1116.ClassHelperFolder;
//ку

namespace BarberShopZiminDenis1116.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml 
    /// </summary> 
    public partial class ClientWindow : Window
    {
        List<EFDataBaseFolder.Client> listClients = new List<EFDataBaseFolder.Client>();

        List<String> listForSortClient = new List<string>()
        {
            "По умолчанию",
            "По фамилии",
            "По имени",
            "По отчетству",
            "По телефону",
            "По почте"
        };

        public ClientWindow()
        {
            InitializeComponent();
            cmbSortClient.ItemsSource = listForSortClient;
            cmbSortClient.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            listClients = ClassHelperFolder.AppData.context.Client.ToList();
            listClients = listClients.Where(i => i.LastName.Contains(txtSearchClient.Text)
            || i.FirstName.Contains(txtSearchClient.Text)
            || i.MiddleName.Contains(txtSearchClient.Text)
            || i.PhoneNumber.Contains(txtSearchClient.Text)
            || i.EMail.Contains(txtSearchClient.Text)).ToList();

            switch (cmbSortClient.SelectedIndex)
            {
                case 0:
                    listClients = listClients.OrderBy(i => i.idClient).ToList();
                    break;
                case 1:
                    listClients = listClients.OrderBy(i => i.LastName).ToList();
                    break;
                case 2:
                    listClients = listClients.OrderBy(i => i.FirstName).ToList();
                    break;
                case 3:
                    listClients = listClients.OrderBy(i => i.MiddleName).ToList();
                    break;
                case 4:
                    listClients = listClients.OrderBy(i => i.PhoneNumber).ToList();
                    break;
                case 5:
                    listClients = listClients.OrderBy(i => i.EMail).ToList();
                    break;
                default: 
                    listClients = listClients.OrderBy(i => i.idClient).ToList();
                    break;
            }

            if (listClients.Count==0)
            {
                MessageBox.Show("Записей нет!");
            }
            lvClients.ItemsSource = listClients;
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            addClientWindow addClientWindow = new addClientWindow();
            this.Hide();
            addClientWindow.ShowDialog();
            Filter();
        }

        private void txtSearchClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSortClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnExitCLIWIN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
