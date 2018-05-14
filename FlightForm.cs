using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise8
{
    public partial class FlightForm : Form
    {
        CustomerEntities _customerEntities;
        public FlightForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                Flight flight = new Flight();
                flight.airlineCompany = txtAirline.Text;
                flight.origin = txtOrigin.Text;
                flight.destination = txtDestination.Text;
                flight.date = dtDate.Text;
                bool result = SaveFlightDetails(flight); // calling SaveStudentDetails method to save the record in table.Here passing a student details object as parameter  
                ShowStatus(result, "Save");
            }
        }
        public bool SaveFlightDetails(Flight flight) // calling SaveStudentMethod for insert a new record  
        {
            bool result = false;
            using (CustomerEntities _entity = new CustomerEntities())
            {
                _entity.Flight.Add(flight);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }
        public void ClearFields() // Clear the fields after Insert or Update or Delete operation  
        {
            txtAirline.Text = "";
            txtOrigin.Text = "";
            txtDestination.Text = "";
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
            Display();
        }
        private void FlightForm_Load(object sender, EventArgs e)
        {
            _customerEntities = new CustomerEntities();
            Display();   
        }
        public void Display()   // Display Method is a common method to bind the Student details in datagridview after save,update and delete operation perform.
        {
            using (CustomerEntities _entity = new CustomerEntities())
            {
                List<FlightClass> _flightList = new List<FlightClass>();
                _flightList = _entity.Flight.Select(x => new FlightClass
                {
                    flightId = x.flightId,
                    airlineCompany = x.airlineCompany,
                    origin = x.origin,
                    destination = x.destination,
                    date = x.date
                }).ToList();
                FlightDataGridView.DataSource = _flightList;
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                Flight flight = SetValues(Convert.ToInt32(lblFlightId.Text), txtAirline.Text, txtOrigin.Text, txtDestination.Text, dtDate.Text); // Binding values to StudentInformationModel  
                bool result = DeleteFlightDetails(flight); //Calling DeleteStudentDetails Method  d
                ShowStatus(result, "Delete");
            }
        }
        public bool DeleteFlightDetails(Flight flight) // DeleteStudentDetails method to delete record from table  
        {
            bool result = false;
            using (CustomerEntities _entity = new CustomerEntities())
            {
                //Flight book = (Flight)_entity.Flight.Where(b => b.flightId == flight.flightId).First();
                //_entity.DeleteObject(book);
                Flight _flight = _entity.Flight.Where(x => x.flightId == flight.flightId).Select(x => x).FirstOrDefault();
                _entity.Flight.Remove(_flight);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }
        public Flight SetValues(int Id, string airline,string origin, string destination, string date) //Setvalues method for binding field values to StudentInformation Model class  
        {
            Flight flight = new Flight();
            flight.flightId = Id;
            flight.airlineCompany = airline;
            flight.origin = origin;
            flight.destination = destination;
            flight.date = date;
            return flight;
        }

        private void FlightDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FlightDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblFlightId.Text= FlightDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            txtAirline.Text = FlightDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtOrigin.Text = FlightDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtDestination.Text = FlightDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            dtDate.Text = FlightDataGridView.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                Flight flight = SetValues(Convert.ToInt32(lblFlightId.Text), txtAirline.Text, txtOrigin.Text, txtDestination.Text, dtDate.Text);
                bool result = UpdateFlightDetails(flight); // calling UpdateStudentDetails Method  
                ShowStatus(result, "Update");
            }
        }
        public bool UpdateFlightDetails(Flight flight) // UpdateStudentDetails method for update a existing Record  
        {
            bool result = false;
            using (CustomerEntities _entity = new CustomerEntities())
            {
                Flight _flight = _entity.Flight.Where(x => x.flightId == flight.flightId).Select(x => x).FirstOrDefault();
                _flight.airlineCompany = flight.airlineCompany;
                _flight.origin = flight.origin;
                _flight.destination = flight.destination;
                _flight.date = flight.date;
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }
        public bool CheckFields()
        {
            bool flag = true;
            if(txtAirline.Text=="" ||txtDestination.Text==""||txtOrigin.Text=="")
            {
                flag = false;
                MessageBox.Show("Please Provide valid information!!!");
            }
            return flag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CustomerForm cForm = new CustomerForm();
            cForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SearchFlightForms s = new SearchFlightForms();
            s.Show();
        }
    }
}
