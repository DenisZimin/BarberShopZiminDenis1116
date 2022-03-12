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
    /// Логика взаимодействия для addPersonalWindow.xaml
    /// </summary>
    public partial class addPersonalWindow : Window
    {
        //Присоединение к базе
        EFDataBaseFolder.Personal editPersonal = new EFDataBaseFolder.Personal();
        //Переменная для фото
        private string pathPhoto = null;
        //Переменная для изменения
        bool isEdit = true;

        public addPersonalWindow()
        {
            //Заполнение комбобкса
            InitializeComponent();
            cmbPostPerson.ItemsSource = ClassHelperFolder.AppData.context.Post.ToList();
            cmbPostPerson.DisplayMemberPath = "PostName";
            cmbPostPerson.SelectedIndex = 0;

            cmbWorkTimePerson.ItemsSource = ClassHelperFolder.AppData.context.WorkTime.ToList();
            cmbWorkTimePerson.DisplayMemberPath = "WorkTime1";
            cmbWorkTimePerson.SelectedIndex = 0;

            isEdit = false;
        }

        public addPersonalWindow(EFDataBaseFolder.Personal personal)
        {
            InitializeComponent();
            cmbPostPerson.ItemsSource = ClassHelperFolder.AppData.context.Post.ToList();
            cmbPostPerson.DisplayMemberPath = "PostName";
            cmbPostPerson.SelectedIndex = Convert.ToInt32(personal.idFKPost-1);

            cmbWorkTimePerson.ItemsSource = ClassHelperFolder.AppData.context.WorkTime.ToList();
            cmbWorkTimePerson.DisplayMemberPath = "WorkTime1";
            cmbWorkTimePerson.SelectedIndex = Convert.ToInt32(personal.idWorkTime-1);

            tbLastNamePersonal.Text = personal.LastName;
            tbFirstNamePersonal.Text = personal.LastName;
            tbMiddleNamePersonal.Text = personal.MiddleName;
            tbPhonePersonal.Text = personal.PhoneNumber;
            tbEmailPersonal.Text = personal.EMail;
            tbLoginPersonal.Text = personal.PersonalLogin;
            tbPassPersonal.Text = personal.PersonalPassword;

            tbADDEDITPersonal.Text = "Изменение данных";
            btnAddPersonal.Content = "Изменить";

            //СЧИТЫВАНИЕ ФОТО
            if (personal.PersonalPhoto != null)
            {
                using (MemoryStream stream = new MemoryStream(personal.PersonalPhoto))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    PersonalPhoto.Source = bitmapImage;
                }

            }

            editPersonal = personal;
            isEdit = true;

        }

        private void btnAddPersonal_Click(object sender, RoutedEventArgs e)
        {
            //Наличие данных в базе------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            var userAuthPhone = AppData.context.Personal.ToList().
                Where(i => i.PhoneNumber == tbPhonePersonal.Text).
                FirstOrDefault();
            if (userAuthPhone != null)
            {
                MessageBox.Show("Телефон есть в системе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var userAuthEmail = AppData.context.Personal.ToList().
                Where(i => i.EMail == tbEmailPersonal.Text).
                FirstOrDefault();
            if (userAuthEmail != null)
            {
                MessageBox.Show("Почта есть в системе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var userAuthLogin = AppData.context.Personal.ToList().
                Where(i => i.PersonalLogin == tbLoginPersonal.Text).
                FirstOrDefault();
            if (userAuthLogin != null)
            {
                MessageBox.Show("Логин есть в системе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var userAuthPass = AppData.context.Personal.ToList().
                Where(i => i.PersonalPassword == tbPassPersonal.Text).
                FirstOrDefault();
            if (userAuthPass != null)
            {
                MessageBox.Show("Пароль есть в системе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Проверка пустых полей------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (string.IsNullOrWhiteSpace(tbLastNamePersonal.Text))
            {
                MessageBox.Show("Поле Фамилия не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbFirstNamePersonal.Text))
            {
                MessageBox.Show("Поле Имя не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPhonePersonal.Text))
            {
                MessageBox.Show("Поле Телефон не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbEmailPersonal.Text))
            {
                MessageBox.Show("Поле Почта не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbLoginPersonal.Text))
            {
                MessageBox.Show("Поле Логин не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPassPersonal.Text))
            {
                MessageBox.Show("Поле Пароль не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Совпадение паролей--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (tbPassPersonal.Text != tbPassConfirmPersonal.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

            //Добавление------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            var resClick = MessageBox.Show("Данные готовы?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //try
            //{
                if (resClick == MessageBoxResult.Yes)
                {
                    if (isEdit)
                    {
                    //РЕДАКТИРОВАНИЕ ПЕРСОНАЛА
                    EFDataBaseFolder.Personal addPersonal = new EFDataBaseFolder.Personal();
                        editPersonal.LastName = tbLastNamePersonal.Text;
                        editPersonal.FirstName = tbFirstNamePersonal.Text;
                        editPersonal.MiddleName = tbMiddleNamePersonal.Text;
                        editPersonal.idFKPost = cmbPostPerson.SelectedIndex + 1;
                        editPersonal.idWorkTime = cmbWorkTimePerson.SelectedIndex + 1;
                        editPersonal.PhoneNumber = tbPhonePersonal.Text;
                        editPersonal.EMail = tbEmailPersonal.Text;
                        editPersonal.PersonalLogin = tbLoginPersonal.Text;
                        editPersonal.PersonalPassword = tbPassPersonal.Text;
                    //ИЗМЕНЕНИЕ ФОТО
                    if (pathPhoto != null)
                        {
                            editPersonal.PersonalPhoto = File.ReadAllBytes(pathPhoto);
                        }
                        ClassHelperFolder.AppData.context.SaveChanges();
                        MessageBox.Show("Сотрудник изменен", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        EFDataBaseFolder.Personal addPersonal = new EFDataBaseFolder.Personal();
                        addPersonal.LastName = tbLastNamePersonal.Text;
                        addPersonal.FirstName = tbFirstNamePersonal.Text;
                        addPersonal.MiddleName = tbMiddleNamePersonal.Text;
                        addPersonal.idFKPost = cmbPostPerson.SelectedIndex + 1;
                        addPersonal.idWorkTime = cmbWorkTimePerson.SelectedIndex + 1;
                        addPersonal.PhoneNumber = tbPhonePersonal.Text;
                        addPersonal.EMail = tbEmailPersonal.Text;
                        addPersonal.PersonalLogin = tbLoginPersonal.Text;
                        addPersonal.PersonalPassword = tbPassPersonal.Text;
                        addPersonal.IsDeleted = false;
                    //ДОБАВЛЕНИЕ ФОТО
                    if (pathPhoto != null)
                    {
                        addPersonal.PersonalPhoto = File.ReadAllBytes(pathPhoto);
                    }
                    ClassHelperFolder.AppData.context.Personal.Add(addPersonal);
                        ClassHelperFolder.AppData.context.SaveChanges();
                        MessageBox.Show("Сотрудник добавлен", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void tbPhonePersonal_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
             textBox.Text = new string
             (textBox.Text.Where
             (ch => ch == '+' || ch == '-'
             || (ch >= '0' && ch <= '9')
             || (ch >= 'а' && ch <= 'я')
             || (ch >= 'А' && ch <= 'Я')
             || ch == 'ё' || ch == 'Ё').ToArray());
            }
        }

        private void btnExitAddPerWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //ОБРАБОТКА ФОТО
        private void btnChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                PersonalPhoto.Source = new BitmapImage(new Uri(openFile.FileName));
                pathPhoto = openFile.FileName;
            }
        }
    }
}
