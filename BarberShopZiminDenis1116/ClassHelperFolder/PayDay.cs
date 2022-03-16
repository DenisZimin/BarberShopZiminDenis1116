using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopZiminDenis1116.ClassHelperFolder
{
    class PayDay
    {
        public static double PayDayMonth(List<double> ListPayDayMonth)
        {
            return (ListPayDayMonth.Sum() * 0.3) * 0.87;
        }
        
    }
}
