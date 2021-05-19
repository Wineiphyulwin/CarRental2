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
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;
        private Manage_Vehicle_Listing _manage_Vehicle_Listing;
        private bool isValid;
        private readonly CarRentalEntities1 _db;
        public AddEditVehicle(Manage_Vehicle_Listing manage_Vehicle_Listing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle ";
            isEditMode = false;
            isValid = true;
            _db = new CarRentalEntities1();
            _manage_Vehicle_Listing = manage_Vehicle_Listing;
                    
        }

        public AddEditVehicle(TypeofCar carToEdit, Manage_Vehicle_Listing manage_Vehicle_Listing =null)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle ";
            _manage_Vehicle_Listing = manage_Vehicle_Listing;
            isEditMode = true;
            isValid = true;
            _db = new CarRentalEntities1();
            PopulateField(carToEdit);
        }
        private void PopulateField(TypeofCar car)
        {
            lblId.Text = car.id.ToString();
            tbMake.Text = car.make;
            tbModel.Text = car.model;
            tbVin.Text = car.VIN;
            tbLicensePlateNo.Text = car.LicensePlateNumber;
            tbYear.Text = car.Year.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            var errormsg = "";
            try
            {
                if (string.IsNullOrWhiteSpace(tbMake.Text) ||
                    string.IsNullOrWhiteSpace(tbModel.Text))
                {
                    isValid = false;
                    MessageBox.Show( "Please enter missing data.\n\r");
                }
                //if(isEditMode==true)
                if (isEditMode)
                {
                        //Edit Code Here
                    var id = int.Parse(lblId.Text);
                    var car = _db.TypeofCars.FirstOrDefault(q => q.id == id);
                    car.model = tbModel.Text;
                    car.make = tbMake.Text;
                    car.VIN = tbVin.Text;
                    car.LicensePlateNumber = tbLicensePlateNo.Text;
                    car.Year = int.Parse(tbYear.Text);

                    _db.SaveChanges();
                    _manage_Vehicle_Listing.PopulateGrid();
                    MessageBox.Show(
                     "EDIT SUCCESSFUL");
                }
                else
                {
                        //Add Code Here
                    var newCar = new TypeofCar
                        {
                            model = tbModel.Text,
                            make = tbMake.Text,
                            VIN = tbVin.Text,
                            LicensePlateNumber = tbLicensePlateNo.Text,
                            Year = int.Parse(tbYear.Text)
                        };
                    _db.TypeofCars.Add(newCar);
                    _db.SaveChanges();
                    _manage_Vehicle_Listing.PopulateGrid();
                    MessageBox.Show(
                     "INSERT SUCCESSFUL");
                    Close();
                }
                             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // throw;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddEditVehicle_Load(object sender, EventArgs e)
        {

        }
    }
}
