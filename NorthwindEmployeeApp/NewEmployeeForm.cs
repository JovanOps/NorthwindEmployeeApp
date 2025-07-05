using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NorthwindEmployeeApp.Models;

namespace NorthwindEmployeeApp
{
    public partial class NewEmployeeForm : Form
    {
        private Employee employee;
        private bool isEditMode = false;

        public NewEmployeeForm()
        {
            InitializeComponent();
            this.Text = "New Employee";
            employee = new Employee();
        }

        public NewEmployeeForm(Employee existingEmployee)
        {
            InitializeComponent();
            this.Text = "Edit Employee";
            employee = existingEmployee;
            isEditMode = true;

            txtFirstName.Text = employee.FirstName;
            txtLastName.Text = employee.LastName;
            dtpBirthDate.Value = employee.BirthDate ?? DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var context = new NorthwindContext())
            {
                employee.FirstName = txtFirstName.Text.Trim();
                employee.LastName = txtLastName.Text.Trim();
                employee.BirthDate = dtpBirthDate.Value;

                if (isEditMode)
                {
                    context.Employees.Update(employee);
                }
                else
                {
                    context.Employees.Add(employee);
                }

                context.SaveChanges();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
