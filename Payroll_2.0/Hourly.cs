using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_2._0
{

    [Serializable]

    class Hourly
    {
        public List<Paycheck> payList { get; set; }
        public Paycheck paycheck { get; set; }

        public decimal Week1 { get; set; }
        public decimal Week2 { get; set; }
        public decimal Rate { get; set; }

        public decimal OTHoursW1 { get; set; }
        public decimal OTHoursW2 { get; set; }
        public decimal OTPayW1 { get; set; }
        public decimal OTPayW2 { get; set; }
        public decimal Gross { get; set; }

        public decimal calcHourlyGross(decimal week1, decimal week2, decimal rate)
        {            
            paycheck = new Paycheck();

            Rate = rate;
            decimal temp1 = 0m;
            decimal temp2 = 0m;

            if (week1 > 40)  //  Week1 overtime check
            {
                OTHoursW1 = week1 - 40;
                OTPayW1 = OTHoursW1 + (rate * 1.5m);

                temp1 = (week1 * rate) + OTPayW1; 
            }

            if (week1 <= 40) // Week1 no overtime
            {
                Week1 = week1;
                temp1 = (week1 * rate);
            }
            if (week2 > 40)  // Week2 overtime check
            {
                OTHoursW2 = week2 - 40;
                OTPayW2 = OTHoursW2 + (rate * 1.5m);

                temp2 = (week2 * rate) + OTPayW2;
            }
            if (week2 <= 40)  // Week2 no overtime
            {
                Week1 = week2;
                temp2 = (week2 * rate);
            }

            Gross = temp1 + temp2;  // Gross pay, adding both weeks pay together           

            return Gross;
        }        
    }
}
