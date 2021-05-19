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
    
    public partial class MainWindow : Form
    {
        private Login _login;
        private string _roleName;
        private User _user;
        public MainWindow()
        {
            InitializeComponent();
            //_login = new Login();
        }
        public MainWindow(Login login,  User user)
        {
            InitializeComponent();
            _login = login;
            _user = user;
            _roleName = user.UserRoles.FirstOrDefault().Role.shortname;
        }

       /* private void addRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord();
            addRentalRecord.ShowDialog();
            addRentalRecord.MdiParent = this;//represent mainwindow
          //  addRentalRecord.ShowDialog();
        }*/

      

        private void manageVehicleListingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var OpenForm = Application.OpenForms.Cast<Form>();
            var isOpen = OpenForm.Any(q => q.Name == "Manage_Vehicle_Listing");

            if (!isOpen)
            {
                var vehicleListing = new Manage_Vehicle_Listing(); ///class
                vehicleListing.MdiParent = this;
                vehicleListing.Show();
            }
        }

        private void viewArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("ManageRentalRecord"))
            {
                var manageRentalRecords = new ManageRentalRecord();
                manageRentalRecords.MdiParent = this;
                manageRentalRecords.Show();
            }
        }

        private void Maindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _login.Close();
        }
        private void manageUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("ManageUsers"))
            {
                var manageUsers = new ManageUsers();
                manageUsers.MdiParent = this;
                manageUsers.Show();
            }
           
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
           if (_user.password ==Utils.DefaultHashPassword())
           {
                var resetpassword = new ResetPassword(_user);
                resetpassword.ShowDialog();
            }
           var username = _user.username;
            tsiLoginText.Text = $"Logged in As:{username}";
            if(_roleName != "admin")
            {
                manageUserToolStripMenuItem.Visible = false;
            }       
        }       
    }
}
