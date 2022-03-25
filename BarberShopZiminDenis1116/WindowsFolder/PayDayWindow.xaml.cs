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
    /// Логика взаимодействия для PayDayWindow.xaml
    /// </summary>
    public partial class PayDayWindow : Window
    {
      
        List<EFDataBaseFolder.Personal> listPersons = new List<EFDataBaseFolder.Personal>();

        public PayDayWindow()
        {
            InitializeComponent();
        }

        private void btnExitPayDay_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
