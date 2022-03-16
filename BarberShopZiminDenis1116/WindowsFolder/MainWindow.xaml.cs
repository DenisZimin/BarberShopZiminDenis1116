using BarberShopZiminDenis1116.WindowsFolder;
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

namespace BarberShopZiminDenis1116
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BTNExitMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы вышли из программы", "Выход", MessageBoxButton.OK);
            this.Close();
        }

        private void BTNInfoMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Зимин Д.В. и Логуа А.Д. - 2ИСП11-16. Помощь: Адышкин Сергей Сергеевич", "Разработчики", MessageBoxButton.OK);
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            this.Hide();
            clientWindow.ShowDialog();
            this.Show();
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            this.Hide();
            orderWindow.ShowDialog();
            this.Show();
        }

        private void btnPersonal_Click(object sender, RoutedEventArgs e)
        {
            PersonWindow personWindow = new PersonWindow();
            this.Hide();
            personWindow.ShowDialog();
            this.Show();
        }

        private void btnPayDay_Click(object sender, RoutedEventArgs e)
        {
            PayDayWindow payDayWindow = new PayDayWindow();
            this.Hide();
            payDayWindow.ShowDialog();
            this.Show();
        }
    }
}
