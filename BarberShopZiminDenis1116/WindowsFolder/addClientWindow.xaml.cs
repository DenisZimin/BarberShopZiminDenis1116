using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using BarberShopZiminDenis1116.ClassHelperFolder;
using BarberShopZiminDenis1116.EFDataBaseFolder;
using Microsoft.Win32;

namespace BarberShopZiminDenis1116.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для addClientWindow.xaml
    /// </summary>
    public partial class addClientWindow : Window
    {
        //Присоединение к базе
        EFDataBaseFolder.Client editClient = new EFDataBaseFolder.Client();
        //Переменная для фото
        private string pathPhoto = null;
        //Переменная для изменения
        bool isEdit = true;

        public addClientWindow()
        {
            InitializeComponent();
            isEdit = false;
        }

        public addClientWindow(EFDataBaseFolder.Client client)
        {
            InitializeComponent();
            tbLastNameClient.Text = client.LastName;
            tbFirstNameClient.Text = client.FirstName;
            tbMiddleNameClient.Text = client.MiddleName;
            tbPhoneClient.Text = client.PhoneNumber;
            tbEmailClient.Text = client.EMail;

            tbADDEDITCLIENT.Text = "Изменение данных";
            btnAddClient.Content = "Изменить";

            //СЧИТЫВАНИЕ ФОТО
            if (client.ClientPhoto != null)
            {
                using (MemoryStream stream = new MemoryStream(client.ClientPhoto))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    ClientPhoto.Source = bitmapImage;
                }

            }

            editClient = client;
            isEdit = true;

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
            var resClick = MessageBox.Show("Данные готовы?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            try
            {
                if (resClick == MessageBoxResult.Yes)
                {
                    if (isEdit)
                    {
                        //РЕДАКТИРОВАНИЕ КЛИЕНТА
                        editClient.LastName = tbLastNameClient.Text;
                        editClient.FirstName = tbFirstNameClient.Text;
                        editClient.MiddleName = tbMiddleNameClient.Text;
                        editClient.PhoneNumber = tbPhoneClient.Text;
                        editClient.EMail = tbEmailClient.Text;
                        //ИЗМЕНЕНИЕ ФОТО
                        if (pathPhoto != null)
                        {
                            editClient.ClientPhoto = File.ReadAllBytes(pathPhoto);
                        }
                        ClassHelperFolder.AppData.context.SaveChanges(); //Сохранение в базу
                        MessageBox.Show("Клиент изменен", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        //ДОБАВЛЕНИЕ КЛИЕНТА
                        EFDataBaseFolder.Client addClient = new EFDataBaseFolder.Client();
                        addClient.LastName = tbLastNameClient.Text;
                        addClient.FirstName = tbFirstNameClient.Text;
                        addClient.MiddleName = tbMiddleNameClient.Text;
                        addClient.PhoneNumber = tbPhoneClient.Text;
                        addClient.EMail = tbEmailClient.Text;
                        addClient.IsDeleted = false;
                        //ДОБАВЛЕНИЕ ФОТО
                        if (pathPhoto != null)
                        {
                            editClient.ClientPhoto = File.ReadAllBytes(pathPhoto);
                        }
                        ClassHelperFolder.AppData.context.Client.Add(addClient); //Добавление
                        ClassHelperFolder.AppData.context.SaveChanges(); //Сохранение в базу
                        MessageBox.Show("Клиент добавлен", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }                  
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

        //Ограничение на ввод символов в поле "телефон"
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

        //ОБРАБОТКА ФОТО
        private void btnChangePhotoCLIENT_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                ClientPhoto.Source = new BitmapImage(new Uri(openFile.FileName));
                pathPhoto = openFile.FileName;
            }
        }
    }
}
