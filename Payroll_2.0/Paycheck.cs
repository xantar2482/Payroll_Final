using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MuBoxes;


namespace Payroll_2._0
{

    [Serializable]

    class Paycheck
    {
        public decimal[] payArray = new decimal[6];

        public DateTime PayPeriod { get; set; }
        public decimal FedPayed { get; set; }
        public decimal MoStatePayed { get; set; }
        public decimal FICAPayed { get; set; }

        public decimal GrossPay { get; set; }
        public decimal TaxPayed { get; set; }
        public decimal NetPay { get; set; }

        private decimal FederalTax = .07m;
        private decimal MoStateTax = .02m;
        private decimal FICA = .03m;

        public decimal calcHourlyNet(decimal grossPay)
        {
            payArray[0] = FedPayed = (grossPay * FederalTax);
            payArray[1] = MoStatePayed = (grossPay * MoStateTax);
            payArray[2] = FICAPayed = (grossPay * FICA);
            payArray[3] = GrossPay = grossPay;
            payArray[4] = TaxPayed = FedPayed + MoStatePayed + FICAPayed;
            payArray[5] = NetPay = GrossPay - TaxPayed;

            return NetPay;
        }

        public decimal calcSalaryNet(decimal grossPay)
        {
            payArray[0] = FedPayed = (grossPay * FederalTax);
            payArray[1] = MoStatePayed = (grossPay * MoStateTax);
            payArray[2] = FICAPayed = (grossPay * FICA);
            payArray[3] = GrossPay = grossPay;
            payArray[4] = TaxPayed = FedPayed + MoStatePayed + FICAPayed;
            payArray[5] = NetPay = GrossPay - TaxPayed;

            return NetPay;
        }

        public decimal calcCommissionNet(decimal grossPay)
        {
            payArray[0] = FedPayed = (grossPay * FederalTax);
            payArray[1] = MoStatePayed = (grossPay * MoStateTax);
            payArray[2] = FICAPayed = (grossPay * FICA);
            payArray[3] = GrossPay = grossPay;
            payArray[4] = TaxPayed = FedPayed + MoStatePayed + FICAPayed;
            payArray[5] = NetPay = GrossPay - TaxPayed;

            return NetPay;
        }

        public Array getPayCheck()
        {
            return payArray;
        }
    }
}
