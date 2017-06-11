using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_2._0
{

    [Serializable]

    class Salary
    {
        public List<Paycheck> payList { get; set; }
        public Paycheck paycheck { get; set; }

        public decimal Gross{ get; set; }
        public decimal NetPay { get; set; }
        public decimal Rate { get; set; }

        public decimal PTO { get; set; }
        public decimal SickTime { get; set; }

        public decimal CalcSalaryGross(decimal week1Hours, decimal week2Hours, decimal rate)
        {
            paycheck = new Paycheck();

            PTO = 1000;

            decimal ptoUsed;

            if (week1Hours < 40)
            {
                ptoUsed = 40 - week1Hours;
                PTO = PTO - ptoUsed;                
            }

            if (week2Hours < 40)
            {
                ptoUsed = 40 - week2Hours;
                PTO = PTO - ptoUsed;
            }

            Gross = Rate * 2;

            return Gross;
        }
    }
}
