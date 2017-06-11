using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_2._0
{

    [Serializable]

    class Commission
    {
        public List<Paycheck> payList { get; set; }
        public Paycheck paycheck { get; set; }

        public decimal UnitsSold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Rate { get; set; }
        public decimal Gross { get; set; }

        public decimal calcCommissionGross(decimal unitsSold, decimal unitPrice)
        {
            paycheck = new Paycheck();

            UnitsSold = unitsSold;
            UnitPrice = unitPrice;

            return Gross = (UnitsSold * UnitPrice) * Rate;
        }
    }
}
