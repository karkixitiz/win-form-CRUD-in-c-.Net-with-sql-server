using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise8
{
    public partial class CustomerForm : Form
    {
        private Label label1;
        private Button button1;
        private Label label2;
        private TextBox txtName;
        private TextBox txtFlightId;
        public string connetionString="Data Source=kiran-pc;Initial Catalog = Customer; User ID = sa; Password=sa@12";
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtFlightId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Name";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(184, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Flight ID";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(163, 70);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(218, 22);
            this.txtName.TabIndex = 3;
            // 
            // txtFlightId
            // 
            this.txtFlightId.Location = new System.Drawing.Point(163, 119);
            this.txtFlightId.Name = "txtFlightId";
            this.txtFlightId.Size = new System.Drawing.Size(218, 22);
            this.txtFlightId.TabIndex = 4;
            // 
            // CustomerForm
            // 
            this.ClientSize = new System.Drawing.Size(486, 287);
            this.Controls.Add(this.txtFlightId);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "CustomerForm";
            this.Text = "Customer Information";
            this.Load += new System.EventHandler(this.CustomerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
           // bindFlights();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                Customer customer = new Customer();
                customer.name = txtName.Text;
                customer.flightId = Convert.ToInt32(txtFlightId.Text);
                bool result = SaveCustomerDetails(customer); // calling SaveStudentDetails method to save the record in table.Here passing a student details object as parameter  
                ShowStatus(result, "Save");
            }
        }
        public bool SaveCustomerDetails(Customer customer) // calling SaveStudentMethod for insert a new record  
        {
            bool result = false;
            using (CustomerEntities _entity = new CustomerEntities())
            {
                _entity.Customer.Add(customer);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }
        public void ClearFields() // Clear the fields after Insert or Update or Delete operation  
        {
            txtName.Text = "";
            txtFlightId.Text = "";
        }
        public bool CheckFields()
        {
            bool flag = true;
            if (txtName.Text == "" || txtFlightId.Text == "")
            {
                flag = false;
                MessageBox.Show("Please Provide valid information!!!");
            }
            return flag;
        }
        public void ShowStatus(bool result, string Action) // validate the Operation Status and Show the Messages To User  
        {
            if (result)
            {
                if (Action.ToUpper() == "SAVE")
                {
                    MessageBox.Show("Saved Successfully!..", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Action.ToUpper() == "UPDATE")
                {
                    MessageBox.Show("Updated Successfully!..", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Deleted Successfully!..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong!. Please try again!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ClearFields();
        }

    }
}
