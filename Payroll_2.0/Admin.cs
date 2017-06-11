using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_2._0
{
    [Serializable]

    class Admin
    {
        public DateTime PayDay { get; set; }
        public int CurrentEmpNumber { get; set; }

        public Admin(DateTime payDay, int empNumber)
        {
            this.PayDay = payDay;
            this.CurrentEmpNumber = empNumber;
        }


    }
}
