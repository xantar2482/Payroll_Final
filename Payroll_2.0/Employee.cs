using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_2._0
{
    [Serializable]

    class Employee
    {

        public Hourly Hourly { get; set; }
        public Salary Salary { get; set; }
        public Commission Commission { get; set; } 

        public string EmpNum { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }

        public List<Array> PayList { get; set; }
        public List<Array> SalaryPayList { get; set; }
        public List<Array> CommissionPayList { get; set; }
        public Paycheck paycheck { get; set; }

        public Employee(Hourly hourly, string empNum, string fName, string lName, string type)
        {
            Hourly = new Hourly();
            PayList = new List<Array>();

            Hourly = hourly;
            EmpNum = empNum;
            FirstName = fName;
            LastName = lName;
            Type = type;
        }

        public Employee(Salary salary, string empNum, string fName, string lName, string type)
        {
            Salary = new Salary();
            SalaryPayList = new List<Array>();

            Salary = salary;
            EmpNum = empNum;
            FirstName = fName;
            LastName = lName;
            Type = type;
        }

        public Employee(Commission commission, string empNum, string fName, string lName, string type)
        {
            Commission = new Commission();
            CommissionPayList = new List<Array>();

            Commission = commission;
            EmpNum = empNum;
            FirstName = fName;
            LastName = lName;
            Type = type;
        }   
    }
}
