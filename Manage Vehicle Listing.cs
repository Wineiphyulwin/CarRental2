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

    public partial class Manage_Vehicle_Listing : Form
    {
        
        private readonly CarRentalEntities1 _db;
        private string order = String.Empty;
        public Manage_Vehicle_Listing()
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
           
        }


        private void Manage_Vehicle_Listing_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
                // throw;
            }
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            var addEditVehicle = new AddEditVehicle(this);
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();

        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            // get id of selected row
            var id = (int)gvVehicleList.SelectedRows[0].Cells["id"].Value;

            //query database for record 
            var car = _db.TypeofCars.FirstOrDefault(q => q.id == id);

            //launch editvehicle window with data  
            var addEditVehicle = new AddEditVehicle(car,this);
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            var id = (int)gvVehicleList.SelectedRows[0].Cells["id"].Value;
            var car = _db.TypeofCars.FirstOrDefault(q => q.id == id);
           
            DialogResult dr = MessageBox.Show("Are you sure ?" ,"Delete",MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);
            if( dr == DialogResult.Yes)
            {
                _db.TypeofCars.Remove(car);
                _db.SaveChanges();
            }
            PopulateGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();           
        }

        public void PopulateGrid()
        {
          
            var cars = _db.TypeofCars
                .Select(q => new
                {
                    make = q.make,
                    model = q.model,
                    Vin = q.VIN,
                    year = q.Year,
                    LicensePlateNUmber = q.LicensePlateNumber,
                    q.id
                }).ToList();
            gvVehicleList.DataSource = cars;           
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            gvVehicleList.Columns["id"].Visible = false;
            
            gvVehicleList.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(gvVehicleList_ColumnHeaderMouseClick);
            // gvVehicleList.Sort(gvVehicleList.Columns["make"], ListSortDirection.Ascending);
            //gvVehicleList.Columns["make"].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            // gvVehicleList.Columns[5].Visible = flase;
            /* gvVehicleList.Columns[0].HeaderText = "ID";
           gvVehicleList.Columns[1].HeaderText = "NAME";*/


        }

        private void gvVehicleList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (order == "d")
            {
                order = "a";
                gvVehicleList.DataSource = _db.TypeofCars.Select(s => new { id = s.id, make = s.make}).OrderBy(s => s.id).ToList();
            }
            else
            {
                order = "d";
                gvVehicleList.DataSource = _db.TypeofCars.Select(s => new { id = s.id, make = s.make }).OrderByDescending(s => s.id).ToList();
            }
        }
    }
}
