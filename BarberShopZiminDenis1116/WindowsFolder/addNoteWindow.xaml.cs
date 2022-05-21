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

namespace BarberShopZiminDenis1116.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для addNoteWindow.xaml
    /// </summary>
    public partial class addNoteWindow : Window
    {
        public addNoteWindow()
        {
            //Заполнение комбобокса для записей
            InitializeComponent();
            cmbFKService.ItemsSource = ClassHelperFolder.AppData.context.Service.ToList();
            cmbFKService.DisplayMemberPath = "ServiceName";
            cmbFKService.SelectedIndex = 0;

            cmbFKClient.ItemsSource = ClassHelperFolder.AppData.context.Client.ToList();
            cmbFKClient.DisplayMemberPath = "LastName";
            cmbFKClient.SelectedIndex = 0;

            cmbFKPeronal.ItemsSource = ClassHelperFolder.AppData.context.Personal.ToList();
            cmbFKPeronal.DisplayMemberPath = "LastName";
            cmbFKPeronal.SelectedIndex = 0;
        }



        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {

            //Проверка пустых полей------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (string.IsNullOrWhiteSpace(tbDateRegister.Text))
            {
                MessageBox.Show("Поле Дата регистрации записи не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbComment.Text))
            {
                MessageBox.Show("Поле Уточнение не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbStartDate.Text))
            {
                MessageBox.Show("Поле Дата начала оказания услуги не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbFinalDate.Text))
            {
                MessageBox.Show("Поле Дата окончания оказания услуги не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Добавление------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            var resClick = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                EFDataBaseFolder.Notes addNotes = new EFDataBaseFolder.Notes();
                addNotes.idFKService = cmbFKService.SelectedIndex + 1;
                addNotes.idFKClient = cmbFKClient.SelectedIndex + 1;
                addNotes.idFKPersonal = cmbFKPeronal.SelectedIndex + 1;
                addNotes.NateDate = tbDateRegister.Text;
                addNotes.Comment = tbComment.Text;
                addNotes.DateStart = tbStartDate.Text;
                addNotes.DataFinish = tbFinalDate.Text;

                ClassHelperFolder.AppData.context.Notes.Add(addNotes);
                ClassHelperFolder.AppData.context.SaveChanges();
                MessageBox.Show("Запись добавлена", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
        }

        private void btnExitNoteWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
