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
    public partial class ManageRentalRecord : Form
    {
        private readonly CarRentalEntities1 _db;
        private string order = String.Empty;
        public ManageRentalRecord()
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
        }
        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord();
            addRentalRecord.MdiParent = this.MdiParent;
            addRentalRecord.Show();
            
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // get id of selected row
                var id = (int)gvRecordList.SelectedRows[0].Cells["id"].Value;
                var cartype = (string)gvRecordList.SelectedRows[0].Cells["carType"].Value;

                //query database for record 
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);

                //launch editvehicle window with data  
                var addEditRentalRecord = new AddEditRentalRecord(record, cartype)
                {
                   MdiParent = this.MdiParent
                };
                addEditRentalRecord.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
                // throw;
            }
           
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                var id = (int)gvRecordList.SelectedRows[0].Cells["id"].Value;
                var records = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                DialogResult dr = MessageBox.Show("Are you sure ?", "Delete", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    _db.CarRentalRecords.Remove(records);
                    _db.SaveChanges();
                }
                PopulateGrid();               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
                // throw;
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void ManageRentalRecord_Load(object sender, EventArgs e)
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

        private void PopulateGrid()
        {
            var records = _db.CarRentalRecords
               .Select(q => new
               {
                   customerName = q.customer_name,
                   dateRental = q.date_rental,
                   dateReturn = q.date_return,
                   q.car_rental_cost,                
                   carType = q.TypeofCar.make,
                   id =q.id,                 
               }).ToList();
            gvRecordList.DataSource = records;
            gvRecordList.Columns["dateRental"].HeaderText = "Date Rental";
            gvRecordList.Columns["dateReturn"].HeaderText = "Date Return";
            //Hide ID
            gvRecordList.Columns["id"].Visible = false;
        }

        private void gvRecordList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (order == "d")
            {
                order = "a";
                gvRecordList.DataSource = _db.CarRentalRecords.Select(s => new { id = s.id, CustomerName = s.customer_name }).OrderBy(s => s.id).ToList();
            }
            else
            {
                order = "d";
                gvRecordList.DataSource = _db.CarRentalRecords.Select(s => new { id = s.id, CustomerName = s.customer_name }).OrderByDescending(s => s.id).ToList();
            }
        }
    }
}
