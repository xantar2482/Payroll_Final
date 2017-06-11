using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Payroll_2._0
{

    [Serializable]

    public partial class StartForm : Form
    {
        Employee employee;
        Hourly hourly;
        Salary salary;
        Commission commission;
        Admin admin;                             // Admin profile settings... AKA save for dates and current Employee number
        private string adminFile = "admin.txt";

        Dictionary<string, Employee> empDict = new Dictionary<string, Employee>();
        ListViewItem LVitems;
        ListViewItem LVItems2;
        DateTime payDay;

        private int EmpNumber = 1000;
        private string path = "";
        private bool loaded;

        Color btnColor;


        /// <summary>
        ///   Chris Niehaus and Mohammed Langi
        ///   This project is being developed for a "Customer" as well 
        /// </summary>


        public StartForm()
        {
            InitializeComponent();

            // Setting Components to an initial state
            cb_Type.SelectedIndex = 0;
            resetHourlyFields();
            resetSalaryFields();
            disableAllBoxes();

            btnColor = btn_FinishPeriod.BackColor;

            MuBoxes.MuBoxDefault.showMuBoxDefault("Welcome", "Welocome to Payroll 2.0" + Environment.NewLine + "You may load an existing payroll or create a new one under File on the menu bar");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pnl_Welcome.Visible = true;
            pnl_Main.Visible = false;

            listView_Emp.View = View.Details;
            listView_Emp.FullRowSelect = true;
            listView_Emp.MultiSelect = false;
            listView_Emp.Columns.Add("Emp #", 50);
            listView_Emp.Columns.Add("Name", 100);
            listView_Emp.Columns.Add("Type", 90);

            listView_PayHistroy.View = View.Details;
            listView_PayHistroy.FullRowSelect = true;
            listView_PayHistroy.MultiSelect = false;

            listView_PayHistroy.Columns.Add("Pay Periord", 75);
            listView_PayHistroy.Columns.Add("Net Pay", 75);
            listView_PayHistroy.Columns.Add("Gross Pay", 75);
            listView_PayHistroy.Columns.Add("FedPayed", 75);
            listView_PayHistroy.Columns.Add("MoStatePayed", 75);
            listView_PayHistroy.Columns.Add("FICA", 75);
            listView_PayHistroy.Columns.Add("TotalTaxesPayed", 75);
        }

        // Add Row || Create ListView Items || Create Employee Objects

        private void addRowEmpList(string empNum, string fName, string lName, string type)
        {
            if (cb_Type.SelectedItem.ToString() == "Hourly")
            {
                hourly = new Hourly();
                employee = new Employee(hourly, empNum, fName, lName, type);
                employee.Hourly.Rate = Convert.ToDecimal(tBox_Rate.Text);
            }
            if (cb_Type.SelectedItem.ToString() == "Salary")
            {
                salary = new Salary();
                employee = new Employee(salary, empNum, fName, lName, type);
                employee.Salary.Rate = Convert.ToDecimal(tBox_Rate.Text);
            }
            if (cb_Type.SelectedItem.ToString() == "Commission")
            {
                commission = new Commission();
                employee = new Employee(commission, empNum, fName, lName, type);
                employee.Commission.Rate = Convert.ToDecimal(tBox_Rate.Text);
            }

            empDict.Add(empNum, employee);

            String[] row = { empNum, fName + " " + lName, type };
            LVitems = new ListViewItem(row);
            LVitems.Tag = employee;
            listView_Emp.Items.Add(LVitems);

            save(empDict, admin);

            EmpNumber++;
        }

        private void addRowPaycheckList(Employee emp)  
        {
            List<Array> temp = emp.PayList;

            foreach (Array arr in temp)
            {
                String[] row = {
                    emp.Hourly.paycheck.PayPeriod.ToString(),
                    emp.Hourly.paycheck.NetPay.ToString(),
                    emp.Hourly.paycheck.GrossPay.ToString(),
                    emp.Hourly.paycheck.FedPayed.ToString(),
                    emp.Hourly.paycheck.MoStatePayed.ToString(),
                    emp.Hourly.paycheck.FICAPayed.ToString(),                    
                    emp.Hourly.paycheck.TaxPayed.ToString(),
                    };

                LVItems2 = new ListViewItem(row);
                LVItems2.Tag = employee;
                listView_PayHistroy.Items.Add(LVItems2);
            }
        }

        private  void rePopulateEmpList()
        {
            foreach (KeyValuePair<string, Employee> kvp in empDict)
            {
                string empNum = kvp.Key;
                string fName = kvp.Value.FirstName;
                string lName = kvp.Value.LastName;
                string type = kvp.Value.Type;

                String[] row = { empNum, fName + " " + lName, type };
                LVitems = new ListViewItem(row);
                LVitems.Tag = employee;
                listView_Emp.Items.Add(LVitems);
            }
        }

        // Populate methods
        private void populateEmpList(Dictionary<string, Employee> dict)
        {
            foreach (KeyValuePair<string, Employee> kvp in dict)
            {
                employee = kvp.Value;

                String[] row = { employee.EmpNum.ToString(), employee.FirstName + " " + employee.LastName, employee.Type };
                LVitems = new ListViewItem(row);                          // if null check
                LVitems.Tag = employee;
                listView_Emp.Items.Add(LVitems);
            }
        }

        private void paycheckPreview(Employee emp)
        {
            lb_PaycheckPreview.Items.Add("dates");
            lb_PaycheckPreview.Items.Add(employee.FirstName);
            lb_PaycheckPreview.Items.Add(employee.LastName);
            lb_PaycheckPreview.Items.Add(employee.EmpNum.ToString());
            lb_PaycheckPreview.Items.Add("---------------------------");

            if (emp.Type == "Hourly")
            {
                lb_PaycheckPreview.Items.Add(employee.Hourly.paycheck.FedPayed);
                lb_PaycheckPreview.Items.Add(employee.Hourly.paycheck.MoStatePayed);
                lb_PaycheckPreview.Items.Add(employee.Hourly.paycheck.FICAPayed);
                lb_PaycheckPreview.Items.Add("---------------------------");
                lb_PaycheckPreview.Items.Add(employee.Hourly.Gross);
                lb_PaycheckPreview.Items.Add(employee.Hourly.paycheck.TaxPayed);
                lb_PaycheckPreview.Items.Add(employee.Hourly.paycheck.NetPay);
            }
        
            if (emp.Type == "Salary")
            {
                lb_PaycheckPreview.Items.Add(employee.Salary.paycheck.FedPayed);
                lb_PaycheckPreview.Items.Add(employee.Salary.paycheck.MoStatePayed);
                lb_PaycheckPreview.Items.Add(employee.Salary.paycheck.FICAPayed);
                lb_PaycheckPreview.Items.Add("---------------------------");
                lb_PaycheckPreview.Items.Add(employee.Salary.Gross);
                lb_PaycheckPreview.Items.Add(employee.Salary.paycheck.TaxPayed);
                lb_PaycheckPreview.Items.Add(employee.Salary.paycheck.NetPay);
            }
            if (emp.Type == "Commission")
            {
                lb_PaycheckPreview.Items.Add(employee.Commission.paycheck.FedPayed);
                lb_PaycheckPreview.Items.Add(employee.Commission.paycheck.MoStatePayed);
                lb_PaycheckPreview.Items.Add(employee.Commission.paycheck.FICAPayed);
                lb_PaycheckPreview.Items.Add("---------------------------");
                lb_PaycheckPreview.Items.Add(employee.Commission.Gross);
                lb_PaycheckPreview.Items.Add(employee.Commission.paycheck.TaxPayed);
                lb_PaycheckPreview.Items.Add(employee.Commission.paycheck.NetPay);
            }                    
        }

        private void StartPayPeriod()
        {
            admin.PayDay = dtp_PayPeriodStart.Value;

            hideAddEmp();
            showEmpDisplay();
            gBox_List.Enabled = true;
        }

        ///// Group Box Add Employee 
        private void btn_Finished_Click(object sender, EventArgs e)
        {
            btn_GetEmp.Enabled = true;
            gBox_AddEmp.Visible = false;
            gBox_DisplayEmp.Visible = true;

            MuBoxes.MuBoxDefault.showMuBoxDefault("Finished Adding Employees","You may now work on Payroll by starting a New Pay Period" + Environment.NewLine + "You may add new employees at any time by selecting Add Employee from the menu bar ");
        }

        private void btn_AddNew_Click(object sender, EventArgs e)
        {

            if (tBox_EmpFirstName.Text == "First Name"
                || tBox_EmpLastName.Text == "Last Name"
                || tBox_Rate.Text == "Dollars per Hour"
                || tBox_Rate.Text == "Salary per Week"
                || tBox_Rate.Text == "Percent of Sales"
                || tBox_Rate.Text == "Rate"
                || cb_Type.SelectedIndex == 0
                || tBox_Rate.Text == ""
                || tBox_EmpFirstName.Text == ""
                || tBox_EmpLastName.Text == ""
                 )
            // if (tb.Text == "First Name"
            //|| tb.Text == "Last Name"
            //|| tb.Text == "Dollars per Hour"
            //|| tb.Text == "Salary per Week"
            //|| tb.Text == "Percent of Sales"
            //|| tb.Text == "Rate"
            //|| cb_Type.SelectedIndex == 0
            //|| tb.Text == ""
            //)
            {
                MuBoxes.MuBoxDefault.showMuBoxDefault("***** Error *****","Please enter proper information");
                return;
            }

            string tempType = cb_Type.SelectedItem.ToString();
            string tempEmpNumber = EmpNumber.ToString();
            string tempFName = tBox_EmpFirstName.Text;
            string tempLName = tBox_EmpLastName.Text;
            string tempRate = tBox_Rate.Text;

            admin.CurrentEmpNumber = EmpNumber;

            addRowEmpList(tempEmpNumber, tempFName, tempLName, tempType);   // Populate Row in Emp List
          
            resetInfoBox();

            lbl_EmpNumber.Text = EmpNumber.ToString();                      // Change the label for EmpNumber post
            tBox_EmpFirstName.Focus();                                      // Reset Focus to the first name box
        }

        private void btn_ResetEmpInfo_Click(object sender, EventArgs e)
        {
            btn_AddNew.Enabled = true;
            resetInfoBox();
        }

        ///// Group Box Employee Info
        private void btn_GetEmp_Click(object sender, EventArgs e)                   // BookMark
        {
            Menu_Edit.Enabled = false;
            Menu_Payroll.Enabled = false;

            try
            {
                employee = (Employee)listView_Emp.SelectedItems[0].Tag;
            }
            catch (Exception)
            {
                MuBoxes.MuBoxDefault.showMuBoxDefault("***** Alert *****", "Please select an employee from the list");
                return;
            }

            populateEmpDisplay();                                    // Populate the Employee Display Box 

            if (employee.Type == "Hourly")
            {
                gBox_Hourly.Enabled = true;
                gBox_Commission.Enabled = false;
                gBox_Salary.Enabled = false;

                tb_HourlyRate.Enabled = false;
                tb_ttl1.Enabled = false;
                tb_ttl2.Enabled = false;
                tb_TotalHours.Enabled = false;
                tb_HourlyGross.Enabled = false;
                tb_HourlyRate.Text = employee.Hourly.Rate.ToString();        /////////////////////////////////////////////////
            }
            if (employee.Type == "Salary")
            {
                gBox_Hourly.Enabled = false;
                gBox_Commission.Enabled = false;
                gBox_Salary.Enabled = true;

                tb_SalRate.Enabled = false;
                tb_SalTotalW1.Enabled = false;
                tb_SalTotalW2.Enabled = false;
                tb_SalTotalHours.Enabled = false;
                tb_SalGross.Enabled = false;
                tb_SalRate.Text = employee.Salary.Rate.ToString();
            }
            if (employee.Type == "Commission")
            {
                gBox_Hourly.Enabled = false;
                gBox_Commission.Enabled = true;
                gBox_Salary.Enabled = false;
                tb_CommRate.Text = employee.Commission.Rate.ToString();
            }
        }

        private void populateEmpDisplay()
        {

            lb_ShowEmpWorking.Items.Clear();           
            lb_ShowEmpWorking.Items.Add(employee.EmpNum.ToString());
            lb_ShowEmpWorking.Items.Add(employee.FirstName);
            lb_ShowEmpWorking.Items.Add(employee.LastName);
            lb_ShowEmpWorking.Items.Add(employee.Type);
        }

        ///// Group Box Hourly
        private void btn_HourlySet_Click(object sender, EventArgs e)
        {
            btn_GetEmp.Enabled = false;

            decimal[] week1 = new decimal[7];
            decimal[] week2 = new decimal[7];
            decimal week1Sum;
            decimal week2Sum;
            decimal tempGross;
            decimal totalHours;
            decimal OT1;
            decimal OT2;
            decimal TotalOT;

            week1[0] = Convert.ToDecimal(tb_11.Text);
            week1[1] = Convert.ToDecimal(tb_12.Text);
            week1[2] = Convert.ToDecimal(tb_13.Text);
            week1[3] = Convert.ToDecimal(tb_14.Text);
            week1[4] = Convert.ToDecimal(tb_15.Text);
            week1[5] = Convert.ToDecimal(tb_16.Text);
            week1[6] = Convert.ToDecimal(tb_17.Text);

            week2[0] = Convert.ToDecimal(tb_21.Text);
            week2[1] = Convert.ToDecimal(tb_22.Text);
            week2[2] = Convert.ToDecimal(tb_23.Text);
            week2[3] = Convert.ToDecimal(tb_24.Text);
            week2[4] = Convert.ToDecimal(tb_25.Text);
            week2[5] = Convert.ToDecimal(tb_26.Text);
            week2[6] = Convert.ToDecimal(tb_27.Text);

            week1Sum = week1.Sum();
            week2Sum = week2.Sum();
            totalHours = week1Sum + week2Sum;

            // Calculating Gross Pay
            tempGross = employee.Hourly.calcHourlyGross(week1Sum, week2Sum, employee.Hourly.Rate); 

            // Processing the gross pay for hourly
            employee.Hourly.paycheck.calcHourlyNet(tempGross);                  //  Sending Gross to paycheck.calc for netpay
            employee.PayList.Add(employee.Hourly.paycheck.payArray);            //  Adding paycheck array to PayList in Employee
            employee.Hourly.paycheck.PayPeriod = dtp_PayPeriodStart.Value;      //  Setting the Period date
            OT1 = employee.Hourly.OTHoursW1;
            OT2 = employee.Hourly.OTHoursW2;
            TotalOT = OT1 + OT2;

            // filling in the Hourly Totals tBoxes
            // tb_HourlyGross.Text = tempGross.ToString();

            tb_ttl1.Text = week1Sum.ToString();
            tb_ttl2.Text = week2Sum.ToString();
            tb_TotalHours.Text = totalHours.ToString();
            tb_HourlyGross.Text = employee.Hourly.Gross.ToString();
            tb_Week1OT.Text = OT1.ToString();
            tb_week2OT.Text = OT2.ToString();
            tb_TotalOT.Text = TotalOT.ToString();


            gBox_Hourly.Enabled = false;
            gBox_PayPreview.Enabled = true;

            // Populate the Paycheck ListBox with PayCheck info
            paycheckPreview(employee);
            
            // build paycheck
        }

        private void btn_HourlyReset_Click(object sender, EventArgs e)
        {
            resetHourlyFields();
        }

        ///// Group Box Salary
        private void btn_SetSalary_Click(object sender, EventArgs e)
        {
            btn_GetEmp.Enabled = false;

            decimal[] week1 = new decimal[7];
            decimal[] week2 = new decimal[7];

            decimal week1Sum;
            decimal week2Sum;
            decimal tempGross;
            decimal totalHours;

            week1[0] = Convert.ToDecimal(tb_S11.Text);
            week1[1] = Convert.ToDecimal(tb_S12.Text);
            week1[2] = Convert.ToDecimal(tb_S13.Text);
            week1[3] = Convert.ToDecimal(tb_S14.Text);
            week1[4] = Convert.ToDecimal(tb_S15.Text);
            week1[5] = Convert.ToDecimal(tb_S16.Text);
            week1[6] = Convert.ToDecimal(tb_S17.Text);

            week2[0] = Convert.ToDecimal(tb_S21.Text);
            week2[1] = Convert.ToDecimal(tb_S22.Text);
            week2[2] = Convert.ToDecimal(tb_S23.Text);
            week2[3] = Convert.ToDecimal(tb_S24.Text);
            week2[4] = Convert.ToDecimal(tb_S25.Text);
            week2[5] = Convert.ToDecimal(tb_S26.Text);
            week2[6] = Convert.ToDecimal(tb_S27.Text);

            week1Sum = week1.Sum();
            week2Sum = week2.Sum();
            totalHours = week1Sum + week2Sum;

            /// Feature PTO

            if (week1Sum < 40)
            {
                decimal temp = 40 - week1Sum;
                MuBoxes.MuBoxDefault.showMuBoxDefault("***** Alert *****", temp.ToString() + " Hours of PTO have been used in first week" + Environment.NewLine + "$$ You must pay for this feature to be added $$");
            }
            if (week2Sum < 40)
            {
                decimal temp = 40 - week2Sum;
                MuBoxes.MuBoxDefault.showMuBoxDefault("***** Alert *****", temp.ToString() + " Hours of PTO have been used in second week" + Environment.NewLine + "$$ You must pay for this feature to be added $$");
            }

            //// Calculating Gross Pay
            tempGross = employee.Salary.CalcSalaryGross(week1Sum, week2Sum, employee.Salary.Rate);

            //// Processing the gross pay for Salary
            employee.Salary.paycheck.calcSalaryNet(tempGross);  //  Sending Gross to paycheck.calc for netpay
            employee.SalaryPayList.Add(employee.Salary.paycheck.payArray);            //  Adding paycheck array to PayList in Employee
            employee.Salary.paycheck.PayPeriod = dtp_PayPeriodStart.Value;      //  Setting the Period date

            //// filling in the Hourly Totals tBoxes
            tb_SalGross.Text = employee.Salary.Gross.ToString();
            tb_SalTotalW1.Text = week1Sum.ToString();
            tb_SalTotalW2.Text = week2Sum.ToString();
            tb_SalTotalHours.Text = totalHours.ToString();

            gBox_Salary.Enabled = false;
            gBox_PayPreview.Enabled = true;

            paycheckPreview(employee);
        }

        //*****  Paycheck GroupBox
        private void btn_confirm_Click(object sender, EventArgs e)
        {
            btn_GetEmp.Enabled = true;

            listView_Emp.Items.Remove(listView_Emp.SelectedItems[0]);
            lb_ShowEmpWorking.Items.Clear();
            clearPayCheckPreview();
            gBox_PayPreview.Enabled = false;

            if (listView_Emp.Items.Count == 0)
            {
                btn_GetEmp.Enabled = false;
                btn_FinishPeriod.Enabled = true;
                btn_FinishPeriod.BackColor = Color.Red;

            }

            resetHourlyFields();
            resetCommissionFields();
            resetSalaryFields();

        }

        private void btn_History_Click(object sender, EventArgs e)
        {
            addRowPaycheckList(employee);
        }

        ///// Group Box Commission
        private void btn_CommissionSet_Click(object sender, EventArgs e)
        {
            btn_GetEmp.Enabled = false;

            tb_CommRate.Text = employee.Commission.Rate.ToString();           

            employee.Commission.UnitPrice = Convert.ToDecimal(tb_UnitPrice.Text);
            employee.Commission.UnitsSold = Convert.ToDecimal(tb_UnitsSold.Text);

            decimal tempGross = employee.Commission.calcCommissionGross(employee.Commission.UnitsSold, employee.Commission.UnitPrice);
            employee.Commission.paycheck.calcCommissionNet(tempGross);
            employee.CommissionPayList.Add(employee.Commission.paycheck.payArray);            //  Adding paycheck array to PayList in Employee
            employee.Commission.paycheck.PayPeriod = dtp_PayPeriodStart.Value;

            paycheckPreview(employee);

            gBox_Commission.Enabled = false;
            gBox_PayPreview.Enabled = true;

        }

        // // Group Box List
        private void btn_FinishPeriod_Click(object sender, EventArgs e)
        {
            DateTime tempDate = payDay.AddDays(14);
            payDay = tempDate;
            admin.PayDay = payDay;

            lbl_PeriodDate.Text = payDay.ToShortDateString();
            btn_FinishPeriod.Enabled = false;
            save(empDict, admin);

            Menu_Edit.Enabled = true;
            Menu_Payroll.Enabled = true;

            MuBoxes.MuBoxDefault.showMuBoxDefault("Pay Period Completed","Pay Period has been completed." + Environment.NewLine + "You may start a new Pay Period from the menu bar");
        }

        // // Group Box Paycheck Preview
        private void btn_Save_Click(object sender, EventArgs e)
        {
            save(empDict, admin);
        }

        // Clear || Reset Inputs || Disable
        private void enableWelcome()
        {
            pnl_Welcome.Visible = true;
            pnl_Main.Visible = false;
        }

        private void disableWelcome()
        {
            pnl_Welcome.Visible = false;
            pnl_Main.Visible = true;
        }

        private void disableAllTypeGroupBoxes()
        { 
            gBox_Hourly.Enabled = false;
            gBox_Commission.Enabled = false;
            gBox_Salary.Enabled = false;
            lb_PaycheckPreview.Enabled = false;
            btn_GetEmp.Enabled = false;
        }

        private void disableAllBoxes()
        {
            gBox_DisplayEmp.Visible = false;
            gBox_AddEmp.Visible = false;

            gBox_AddEmp.Enabled = false;
            gBox_Commission.Enabled = false;
            gBox_DisplayEmp.Enabled = false;
            gBox_Hourly.Enabled = false;
            gBox_Salary.Enabled = false;        
            gBox_List.Enabled = false;
            gBox_PayPreview.Enabled = false;
            gBox_PayStubHistory.Enabled = false;
            
         }

        private void resetHourlyFields()                    //// Reset Hourly textBoxes to default 
        {
            //week 1
            tb_11.Text = "0.00";
            tb_12.Text = "0.00";
            tb_13.Text = "0.00";
            tb_14.Text = "0.00";
            tb_15.Text = "0.00";
            tb_16.Text = "0.00";
            tb_17.Text = "0.00";
            //week 2
            tb_21.Text = "0.00";
            tb_22.Text = "0.00";
            tb_23.Text = "0.00";
            tb_24.Text = "0.00";
            tb_25.Text = "0.00";
            tb_26.Text = "0.00";
            tb_27.Text = "0.00";
            // totals
            tb_ttl1.Text = "0.00";
            tb_ttl2.Text = "0.00";
            tb_TotalHours.Text = "0.00";
            tb_HourlyRate.Text = "0.00";
            tb_HourlyGross.Text = "0.00";

        }    
        
        private void resetInfoBox()
        {
            cb_Type.SelectedIndex = 0;
            lbl_EmpNumber.Text = "Employee Number";
            tBox_EmpFirstName.Text = "First Name";
            tBox_EmpLastName.Text = "Last Name";
            //tBox_EmpNumber.Text = "Emp Number";
            //tBox_Type.Text = "Type";
            tBox_Rate.Text = "Rate";
        }                     //// Reset Employee Display Box //

        private void resetSalaryFields()
        {
            //week 1
            tb_S11.Text = "0.00";
            tb_S12.Text = "0.00";
            tb_S13.Text = "0.00";
            tb_S14.Text = "0.00";
            tb_S15.Text = "0.00";
            tb_S16.Text = "0.00";
            tb_S17.Text = "0.00";
            //week 2
            tb_S21.Text = "0.00";
            tb_S22.Text = "0.00";
            tb_S23.Text = "0.00";
            tb_S24.Text = "0.00";
            tb_S25.Text = "0.00";
            tb_S26.Text = "0.00";
            tb_S27.Text = "0.00";
            // totals
            tb_SalTotalW1.Text = "0.00";
            tb_SalTotalW2.Text = "0.00";
            tb_SalTotalHours.Text = "0.00";
            tb_SalRate.Text = "0.00";
            tb_SalGross.Text = "0.00";
        }

        private void resetCommissionFields()
        {
            tb_UnitPrice.Text = "0.00";
            tb_UnitsSold.Text = "0.00";
            tb_CommGross.Text = "0.00";
            tb_CommRate.Text = "0.00";
        }

        private void clearPayCheckPreview()
        {
            lb_PaycheckPreview.Items.Clear();
        }

        private void hideEmpDisplay()
        {
            gBox_DisplayEmp.Visible = false;
            gBox_DisplayEmp.Enabled = false;
        }

        private void showEmpDisplay()
        {
            gBox_DisplayEmp.Visible = true;
            gBox_DisplayEmp.Enabled = true;
        }

        private void hideAddEmp()
        {
            gBox_AddEmp.Visible = false;
            gBox_AddEmp.Enabled = false;
        }

        private void showAddEmp()
        {
            gBox_AddEmp.Visible = true;
            gBox_AddEmp.Enabled = true;
        }

        private void cb_Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cb_Type.SelectedItem.ToString() == "Hourly")
            {
                tBox_Rate.Text = "Dollars per Hour";
            }
            if (cb_Type.SelectedItem.ToString() == "Salary")
            {
                tBox_Rate.Text = "Salary per Week";
            }
            if (cb_Type.SelectedItem.ToString() == "Commission")
            {
                tBox_Rate.Text = "Percent of Sales";
            }
        }

        private void noLetters(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl (e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void noDigits(object sender, KeyPressEventArgs e)
        { 
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public void turnOffButtona(object sender, EventArgs e)
        {
            btn_GetEmp.Enabled = false;
        }

        public void turnOnButtons()
        {
            btn_GetEmp.Enabled = true;
        }
        // Text Box Events 
        private void ClickClear(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Clear();
        }

        private void tBox_EmpFirstName_Click(object sender, EventArgs e)
        {
            tBox_EmpFirstName.Clear();
        }

        private void tBox_EmpLastName_Click(object sender, EventArgs e)
        {
            tBox_EmpLastName.Clear();
        }

        private void tBox_Rate_Click(object sender, EventArgs e)
        {
            tBox_Rate.Clear();
        }

        // Menu Bar Events
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addNewEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            disableWelcome();

            hideEmpDisplay();
            showAddEmp();
           
            cb_Type.SelectedIndex = 0;
            lbl_EmpNumber.Text = EmpNumber.ToString();
            btn_GetEmp.Enabled = false;
           
            disableAllTypeGroupBoxes();
        }

        private void startPayrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //disableWelcome();

            if (loaded == false)
            {
                createNewSave();
                loaded = true;
            }
            //gBox_DisplayEmp.Visible = true;
            //gBox_AddEmp.Visible = false;
            //listView_Emp.Enabled = true;
            //btn_GetEmp.Enabled = true;
        }

        // Save || Load
        private void load()
        {
            BinaryFormatter bf = new BinaryFormatter();
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;

                using (Stream stream = File.OpenRead(path))
                {
                    Dictionary<string, Employee> empDict = (Dictionary<string, Employee>)bf.Deserialize(stream);
                    this.empDict = empDict;
                    stream.Close();
                    populateEmpList(empDict);

                    //EmpNumber = empDict.Count() + 1000;
                    loaded = true;
                    Menu_File_Load.Enabled = false;
                    Menu_File_CreateNew.Enabled = false;
                    disableWelcome();
                }

                using (Stream stream = File.OpenRead(adminFile))
                {
                    admin = (Admin)bf.Deserialize(stream);
                    stream.Close();

                    EmpNumber = admin.CurrentEmpNumber + 1;
                    payDay = admin.PayDay;

                    loaded = true;
                    Menu_File_Load.Enabled = false;
                    Menu_File_CreateNew.Enabled = false;
                    disableWelcome();                   
                }

                Menu_File_Save.Enabled = true;
                Menu_Edit.Enabled = true;
                Menu_Payroll.Enabled = true;

                lbl_PeriodDate.Text = admin.PayDay.ToShortDateString();
            }
            else
            {
                return;
            }
        }

        private void createNewSave()
        {
            if (loaded == true)
            {
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Create a new Save file";
            sfd.Filter = "Text File | *.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                empDict = new Dictionary<string, Employee>();
                admin = new Admin(payDay, EmpNumber);
                string path = sfd.FileName;
                
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    this.path = path;
                    bf.Serialize(stream, empDict);
                    stream.Close();
                    Menu_File_Load.Enabled = false;
                    Menu_File_CreateNew.Enabled = false;
                }
                using (Stream stream = new FileStream(adminFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    bf.Serialize(stream, admin);
                    stream.Close();
                    Menu_File_Load.Enabled = false;
                    Menu_File_CreateNew.Enabled = false;
                }

                loaded = true;
                disableWelcome();
                                
                Menu_File_Save.Enabled = true;
                Menu_Edit.Enabled = true;
                Menu_Payroll.Enabled = true;

                lbl_PeriodDate.Text = admin.PayDay.ToShortDateString();
                MuBoxes.MuBoxDefault.showMuBoxDefault("Create New Employee List","Add Employees to Payroll by filling in the employee information." + Environment.NewLine + "Once you are finished adding employees, you may start by selecting New Payroll under Payroll on the menu bar");
            }
            else
            {
                return;
            }
        }

        private void save(Dictionary<string, Employee> dict, Admin admin)
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(stream, dict);
                stream.Close();
            }
            using (Stream stream = new FileStream(adminFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(stream, admin);
                stream.Close();
            }
        }

        // Menu Strip 
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (empDict != null)
            {
                save(empDict, admin);
            }
            if (empDict == null)
            {
                createNewSave();
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == true)
            {
                // add warning
                createNewSave();
                loaded = true;
                return;
            }
            if (loaded == false)
            {
                load();
                loaded = true;                               
            }              
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            hideEmpDisplay();
            showAddEmp();

            lbl_PeriodDate.Text = dtp_PayPeriodStart.Value.ToShortDateString();
            payDay = dtp_PayPeriodStart.Value;
            createNewSave();
        }

        private void removeEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            disableWelcome();
        }

        private void newPayPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_GetEmp.Enabled = true;
            btn_FinishPeriod.BackColor = btnColor;
            MuBoxes.MuBoxDefault.showMuBoxDefault("New Pay Period Started","Select an employee from the list below" + Environment.NewLine + "Then press the Get button to start working with that employee" + Environment.NewLine + "Then hit Commit when hours added and Confirm the paycheck below");

            if (listView_Emp.Items.Count == 0)
            {
                rePopulateEmpList();
            }            
            StartPayPeriod();
        }
    }
}
