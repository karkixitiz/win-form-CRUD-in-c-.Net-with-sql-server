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
    public partial class SearchFlightForms : Form
    {
        public string connection = "data source=kiran-pc;initial catalog=Customer;user id=sa;password=sa@12;MultipleActiveResultSets=True;App=EntityFramework";
        public SearchFlightForms()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Please Enter Customer ID");
            }
            else
            {
                int customerId = Convert.ToInt32(txtSearch.Text);
                RunStoredProc(customerId);
            }
        }
        public void RunStoredProc(int customerId)
        {
            
            SqlConnection myConnection = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("GetCustomerInfoByID", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            myConnection.Open();
            SqlParameter custId = cmd.Parameters.AddWithValue("@customerId", customerId);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    txtCustomer.Text = dr["name"].ToString();
                    txtOrigin.Text = dr["origin"].ToString();
                    txtDestination.Text = dr["destination"].ToString();
                    txtDate.Text = dr["date"].ToString();
                    txtAirlineCompany.Text = dr["airlineCompany"].ToString();
                }
            }
            
        }

    }
}
