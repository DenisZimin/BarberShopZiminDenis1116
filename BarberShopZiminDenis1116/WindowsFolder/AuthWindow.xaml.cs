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
using static BarberShopZiminDenis1116.ClassHelperFolder.AppData;

namespace BarberShopZiminDenis1116.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void BtnLOGIN_Click(object sender, RoutedEventArgs e)
        {
            var userAuth = ClassHelperFolder.AppData.context.Personal.ToList().
                Where(i => i.PersonalLogin == TBoxLOGIN.Text && i.PersonalPassword == TBoxPASSWORD.Text ).
                FirstOrDefault();

            if (userAuth != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                MessageBox.Show("Вы вошли в систему БД 'BarberShop'", "Добро пожаловать!");
            }
            else
            {
                MessageBox.Show("Пользователь не найден!", "Ошибка авторизации");
            }
        }



        private void BtnEXIT_Click(object sender, RoutedEventArgs e)
        {
           MessageBox.Show("Вы вышли из программы", "Выход",MessageBoxButton.OK);
            this.Close();
        }
    }
}
