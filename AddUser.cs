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
    public partial class AddUser : Form
    {
        private readonly CarRentalEntities1 _db;
        private ManageUsers _manageUsers;
        public AddUser(ManageUsers manageUsers)
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
            _manageUsers = manageUsers;
        }

       private void AddUser_Load(object sender, EventArgs e)
        {
            var roles = _db.Roles.ToList();
            cbRoles.DataSource = roles;
            cbRoles.ValueMember = "id";
            cbRoles.DisplayMember = "name";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var UserName = tbUsername.Text;
                var Password = Utils.DefaultHashPassword();
                var user = new User
                {
                    username = UserName,
                    password = Password,
                    isActive = true
                };
                _db.Users.Add(user);
                _db.SaveChanges();
                var userId = user.id;
                var RoleId = (int)cbRoles.SelectedValue;
                var userRole = new UserRole
                {
                    roleid = RoleId,
                    userid = userId,
                };
                _db.UserRoles.Add(userRole);
                _db.SaveChanges();
                MessageBox.Show("New User Added Successfully");
                _manageUsers.PopulateGrid();
                Close();
               
            }catch(Exception ex)
            {
                MessageBox.Show($"ERROR :{ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
