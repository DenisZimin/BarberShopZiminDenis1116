using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BSZiminDenisTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MethodCalc_Test1_Return()
        {
            //входные           
            List<double> ListPayDayMonth = new List<double>() { 50000, 50000, 50000 };
            //ожидаем
            double finalresult = 39150;
            //вызываем метод
            double act = BarberShopZiminDenis1116.ClassHelperFolder.PayDay.PayDayMonth(ListPayDayMonth);
            //сверяем
            Assert.AreEqual(finalresult, act);
        }

        [TestMethod]
        public void MethodCalc_Test2_Return()
        {
            //входные           
            List<double> ListPayDayMonth = new List<double>() { 100, 200, 300 };
            //ожидаем
            double finalresult = 156.6;
            //вызываем метод
            double act = BarberShopZiminDenis1116.ClassHelperFolder.PayDay.PayDayMonth(ListPayDayMonth);
            //сверяем
            Assert.AreEqual(finalresult, act);
        }

        [TestMethod]
        public void MethodCalc_Test3_Return()
        {
            //входные           
            List<double> ListPayDayMonth = new List<double>() { 100, 200, 300 };
            //ожидаем
            double finalresult = 156.6;
            //вызываем метод
            double act = BarberShopZiminDenis1116.ClassHelperFolder.PayDay.PayDayMonth(ListPayDayMonth);
            //сверяем
            Assert.AreEqual(finalresult, act);
        }
    }
}
