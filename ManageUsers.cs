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
    public partial class ManageUsers : Form
    {
        private readonly CarRentalEntities1 _db;
        private ManageUsers manageUsers;
        private User _user;

        public ManageUsers()
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddUser"))
            {
                var addUser = new AddUser(this);
                addUser.MdiParent = this.MdiParent;
                addUser.Show();
            }
            PopulateGrid();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
        try {
               
            var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value;
            //query database for record 
            var user = _db.Users.FirstOrDefault(q => q.id == id);
            var hashPassword = Utils.DefaultHashPassword();
            user.password = hashPassword;         
            _db.SaveChanges();
            MessageBox.Show($"{user.username}'s Password has been Reset");
            PopulateGrid();
            }
            catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
}

        private void btnDeativateUser_Click(object sender, EventArgs e)
        {
            var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value;
            var user = _db.Users.FirstOrDefault(q => q.id == id);
            user.isActive = user.isActive == true ? false : true;                
            _db.SaveChanges();
            MessageBox.Show($"{user.username}'s Status has been changed");
            PopulateGrid();
        }

        private void ManageUsers_Load(object sender, EventArgs e)
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

        public void PopulateGrid()
        {

            var user = _db.Users
                .Select(q => new
                {
                    id = q.id,
                    name = q.username,
                    isActive = q.isActive,
                    rolename = q.UserRoles.FirstOrDefault().Role.name
                }).ToList();
            gvUserList.DataSource = user;
            gvUserList.Columns["rolename"].HeaderText = "Role Name";
            gvUserList.Columns["isActive"].HeaderText = "User Status";
            gvUserList.Columns["id"].Visible = false;


        }


        }
    }
