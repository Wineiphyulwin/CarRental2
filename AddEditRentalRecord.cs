using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditRentalRecord : Form
    {
        private bool isEditMode;
        private readonly CarRentalEntities1 _db;
        private string _carType;

        public AddEditRentalRecord()
        {
            InitializeComponent();           
            lblTitle.Text = "Add New Record ";
            this.Text = "Add New Record ";
            isEditMode = false;          
            _db = new CarRentalEntities1();
        }
        public AddEditRentalRecord(CarRentalRecord  recordToEdit, string cartype)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Record ";
            this.Text = "Edit Record ";
            
           /* //isValid = true;
            _db = new CarRentalEntities1();
            PopulateField(recordToEdit);*/
            if(recordToEdit == null)
            {
                MessageBox.Show("Please Seleceted DATA");
                Close();
            }
            else
            {
                _carType = cartype;              
                isEditMode = true;
                _db = new CarRentalEntities1();
                PopulateField(recordToEdit);
            }

        }

        private void PopulateField(CarRentalRecord recordToEdit)
        {         
                tbCusName.Text = recordToEdit.customer_name;
                tbCost.Text= recordToEdit.car_rental_cost.ToString();
                dtRental.Value= (DateTime)recordToEdit.date_rental;
                dtReturn.Value = (DateTime)recordToEdit.date_return;            
                lblRecordId.Text = recordToEdit.id.ToString();          
        }

        //23
        private void Form1_Load(object sender, EventArgs e)
        {
          
            var cars = _db.TypeofCars
                .Select(q => new
                {
                    id = q.id,
                    Name = q.make
                })
                .ToList();
            cbCarType.DisplayMember = "Name"; //Combo show
            cbCarType.ValueMember = "id";
           //store database
            cbCarType.DataSource = cars;
            if (isEditMode)
            {
                cbCarType.SelectedIndex = cbCarType.FindString(_carType);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = tbCusName.Text;
                double cost = Convert.ToDouble(tbCost.Text);
                var dateRental = dtRental.Value;
                var dateReturn = dtReturn.Value;
                var carType = cbCarType.SelectedValue;
                var isValid = true;
                var errormsg = "";
                if (string.IsNullOrWhiteSpace(customerName))
                    
                {
                    isValid = false;
                    errormsg+="Please enter missing data.\n\r";
                }

                if (dateRental > dateReturn)
                {
                    isValid = false;
                    errormsg += "Illegal Date Selection.\n\r";
                }
                if (isValid)
                {
                    var rentalRecord = new CarRentalRecord();
                   if (isEditMode)
                    {                       
                        var id = int.Parse(lblRecordId.Text);
                        rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                      
                    }
                    //populate record obj with values from the form
                    rentalRecord.customer_name= customerName;
                    rentalRecord.date_rental= dateRental;
                    rentalRecord.date_return = dateReturn;
                    rentalRecord.car_rental_cost = (decimal)cost;
                    rentalRecord.car_type_id = (int)cbCarType.SelectedValue;
                    _db.SaveChanges();
                    if (!isEditMode)
                    {
                        _db.CarRentalRecords.Add(rentalRecord);
                       
                    }
                    _db.SaveChanges();

                    MessageBox.Show($"Customer Name: {customerName}\n\r" +
                     $"Date Rental: {dateRental}\n\r" +
                     $"Date Return: { dateReturn}\n\r" +
                      $"Cost: { cost}\n\r" +
                     $"Car Type : {carType}\n\r" +
                     "ADD RECORD SUCCESSFUL");
                    Close();
                }
                else
                {
                    MessageBox.Show(errormsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {           
            Close();           
        }


        /* private void button2_Click(object sender, EventArgs e)
         {
            // MainWindow mainWindow = new MainWindow();
             var mainWindow = new MainWindow();
             mainWindow.Show();

         }*/
    }
}
