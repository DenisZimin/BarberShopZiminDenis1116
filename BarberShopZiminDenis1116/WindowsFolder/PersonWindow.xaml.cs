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
    /// Логика взаимодействия для PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {

        List<EFDataBaseFolder.Personal> listPersons = new List<EFDataBaseFolder.Personal>();

        List<String> listForSortPerson = new List<string>()
        {
            "По умолчанию",
            "По фамилии",
            "По имени",
            "По отчетству",
            "По телефону",
            "По почте",
            "По должности",
            "По времен работы",
            "По логину",
            "По паролю"
        };
        public PersonWindow()
        {
            InitializeComponent();
            cmbSortPerson.ItemsSource = listForSortPerson;
            cmbSortPerson.SelectedIndex = 0;
            Filter();
        }
        private void Filter()
        {
            listPersons = ClassHelperFolder.AppData.context.Personal.ToList();
            listPersons = listPersons.Where(i => i.LastName.Contains(txtSearchPerson.Text)
            || i.FirstName.Contains(txtSearchPerson.Text)
            || i.MiddleName.Contains(txtSearchPerson.Text)
            || i.PhoneNumber.Contains(txtSearchPerson.Text)
            || i.EMail.Contains(txtSearchPerson.Text)
            || i.PersonalLogin.Contains(txtSearchPerson.Text)
            || i.PersonalPassword.Contains(txtSearchPerson.Text)).ToList();

            switch (cmbSortPerson.SelectedIndex)
            {
                case 0:
                    listPersons = listPersons.OrderBy(i => i.idPersonal).ToList();
                    break;
                case 1:
                    listPersons = listPersons.OrderBy(i => i.LastName).ToList();
                    break;
                case 2:
                    listPersons = listPersons.OrderBy(i => i.FirstName).ToList();
                    break;
                case 3:
                    listPersons = listPersons.OrderBy(i => i.MiddleName).ToList();
                    break;
                case 4:
                    listPersons = listPersons.OrderBy(i => i.PhoneNumber).ToList();
                    break;
                case 5:
                    listPersons = listPersons.OrderBy(i => i.EMail).ToList();
                    break;
                case 6:
                    listPersons = listPersons.OrderBy(i => i.idFKPost).ToList();
                    break;
                case 7:
                    listPersons = listPersons.OrderBy(i => i.idWorkTime).ToList();
                    break;
                case 8:
                    listPersons = listPersons.OrderBy(i => i.PersonalLogin).ToList();
                    break;
                case 9:
                    listPersons = listPersons.OrderBy(i => i.PersonalPassword).ToList();
                    break;
                default:
                    listPersons = listPersons.OrderBy(i => i.idPersonal).ToList();
                    break;
            }

            if (listPersons.Count == 0)
            {
                MessageBox.Show("Записей нет");
            }
            lvPerson.ItemsSource = listPersons;
        }


        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            addPersonalWindow addPersonalWindow = new addPersonalWindow();
            this.Hide();
            addPersonalWindow.ShowDialog();
            Filter();
        }

        private void txtSearchPerson_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSortPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnExitPERWIN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lvPerson_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var delClick = MessageBox.Show($"Хотите удалить сотрудника {(lvPerson.SelectedItem as EFDataBaseFolder.Personal).LastName}", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                try
                {
                    if (delClick == MessageBoxResult.Yes)
                    {
                        EFDataBaseFolder.Personal userDel = new EFDataBaseFolder.Personal();
                        userDel = (lvPerson.SelectedItem as EFDataBaseFolder.Personal);
                        ClassHelperFolder.AppData.context.Personal.Remove(userDel);
                        ClassHelperFolder.AppData.context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show($"Сотрудник удалён", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Filter();
        }
    }
}
