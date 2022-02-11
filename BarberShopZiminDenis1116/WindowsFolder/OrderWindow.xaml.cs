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

namespace BarberShopZiminDenis1116.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {

        List<EFDataBaseFolder.Notes> listOrders = new List<EFDataBaseFolder.Notes>();

        List<String> listForSortOrders = new List<string>()
        {
            "По умолчанию",
            "По услуге",
            "По клиенту",
            "По сотруднику",
            "По дате регистрации",
            "По комментарию",
            "По дате начала",
            "По дате окончания услуги"
        };

        public OrderWindow()
        {
            InitializeComponent();
            cmbSortOrders.ItemsSource = listForSortOrders;
            cmbSortOrders.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            listOrders = ClassHelperFolder.AppData.context.Notes.ToList();
            listOrders = listOrders.Where(i => i.NateDate.Contains(txtSearchOrders.Text)
            || i.Comment.Contains(txtSearchOrders.Text)
            || i.DateStart.Contains(txtSearchOrders.Text)
            || i.DateStart.Contains(txtSearchOrders.Text)).ToList();

            switch (cmbSortOrders.SelectedIndex)
            {
                case 0:
                    listOrders = listOrders.OrderBy(i => i.idNote).ToList();
                    break;
                case 1:
                    listOrders = listOrders.OrderBy(i => i.idFKService).ToList();
                    break;
                case 2:
                    listOrders = listOrders.OrderBy(i => i.idFKClient).ToList();
                    break;
                case 3:
                    listOrders = listOrders.OrderBy(i => i.idFKPersonal).ToList();
                    break;
                case 4:
                    listOrders = listOrders.OrderBy(i => i.NateDate).ToList();
                    break;
                case 5:
                    listOrders = listOrders.OrderBy(i => i.Comment).ToList();
                    break;
                case 6:
                    listOrders = listOrders.OrderBy(i => i.DateStart).ToList();
                    break;
                case 7:
                    listOrders = listOrders.OrderBy(i => i.DataFinish).ToList();
                    break;
                default:
                    listOrders = listOrders.OrderBy(i => i.idNote).ToList();
                    break;
            }

            if (listOrders.Count == 0)
            {
                MessageBox.Show("Записей нет!");
            }
            lvOrders.ItemsSource = listOrders;
        }

        private void btnAddNotes_Click(object sender, RoutedEventArgs e)
        {
            addNoteWindow addNoteWindow = new addNoteWindow();
            this.Hide();
            addNoteWindow.ShowDialog();
            Filter();
        }

        private void cmbSortOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void txtSearchOrders_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void btnExitORDWIN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lvOrders_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var delClick = MessageBox.Show($"Хотите удалить запись на {(lvOrders.SelectedItem as EFDataBaseFolder.Notes).DateStart}", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                try
                {
                    if (delClick == MessageBoxResult.Yes)
                    {
                        EFDataBaseFolder.Notes orderDel = new EFDataBaseFolder.Notes();
                        orderDel = (lvOrders.SelectedItem as EFDataBaseFolder.Notes);
                        ClassHelperFolder.AppData.context.Notes.Remove(orderDel);
                        ClassHelperFolder.AppData.context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show($"Запись удалена!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Filter();

        }
    }
}
