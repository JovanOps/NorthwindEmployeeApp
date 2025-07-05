using NorthwindEmployeeApp.Models;
using System.Linq;

namespace NorthwindEmployeeApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadEmployees();
        }
        private void LoadEmployees()
        {
            using (var context = new NorthwindContext())
            {
                var employees = context.Employees
                    .Select(e => new
                    {
                        e.EmployeeId,
                        e.FirstName,
                        e.LastName,
                        e.BirthDate
                    })
                    .ToList();

                dgvEmployees.DataSource = employees;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadEmployees();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var form = new NewEmployeeForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow != null)
            {
                int employeeId = (int)dgvEmployees.CurrentRow.Cells["EmployeeId"].Value;

                using (var context = new NorthwindContext())
                {
                    var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

                    if (employee != null)
                    {
                        var form = new NewEmployeeForm(employee);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            LoadEmployees();
                        }
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow != null)
            {
                int employeeId = (int)dgvEmployees.CurrentRow.Cells["EmployeeId"].Value;

                var confirm = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    using (var context = new NorthwindContext())
                    {
                        var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                        if (employee != null)
                        {
                            context.Employees.Remove(employee);
                            context.SaveChanges();
                            LoadEmployees();
                        }
                    }
                }
            }
        }
    }
}
