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
    /// Логика взаимодействия для addClientWindow.xaml
    /// </summary>
    public partial class addClientWindow : Window
    {
        public addClientWindow()
        {
            InitializeComponent();
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            //Наличие данных в базе------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            var userAuthPhone = AppData.context.Client.ToList().
                Where(i => i.PhoneNumber == tbPhoneClient.Text).
                FirstOrDefault();
                if (userAuthPhone != null)
            {
                MessageBox.Show("Телефон есть в системе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var userAuthEmail = AppData.context.Client.ToList().
            Where(i => i.EMail == tbEmailClient.Text).
            FirstOrDefault();
            if (userAuthEmail != null)
            {
                MessageBox.Show("Почта есть в системе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Проверка пустых полей------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (string.IsNullOrWhiteSpace(tbLastNameClient.Text))
            {
                MessageBox.Show("Поле Фамилия не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            if (string.IsNullOrWhiteSpace(tbFirstNameClient.Text))
            {
                MessageBox.Show("Поле Имя не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            if (string.IsNullOrWhiteSpace(tbPhoneClient.Text))
            {
                MessageBox.Show("Поле Телефон не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbEmailClient.Text))
            {
                MessageBox.Show("Поле Почта не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           
            //Добавление------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            var resClick = MessageBox.Show("Вы уверены, что хотите добавить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            try
            {
                if (resClick == MessageBoxResult.Yes)
                {
                    EFDataBaseFolder.Client addClient = new EFDataBaseFolder.Client();
                    addClient.LastName = tbLastNameClient.Text;
                    addClient.FirstName = tbFirstNameClient.Text;
                    addClient.MiddleName = tbMiddleNameClient.Text;
                    addClient.PhoneNumber = tbPhoneClient.Text;
                    addClient.EMail = tbEmailClient.Text;

                    ClassHelperFolder.AppData.context.Client.Add(addClient);
                    ClassHelperFolder.AppData.context.SaveChanges();
                    MessageBox.Show("Пользователь добавлен", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void tbEmailClient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbPhoneClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
             textBox.Text = new string
             (textBox.Text.Where
             (ch =>
             ch == '+' || ch == '-'
             || (ch >= '0' && ch <= '9')
             || (ch >= 'а' && ch <= 'я')
             || (ch >= 'А' && ch <= 'Я')
             || ch == 'ё' || ch == 'Ё').ToArray());
            }
        }

        private void btnExitClientWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
