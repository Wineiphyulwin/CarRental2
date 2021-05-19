using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
   
    public partial class Login : Form
    {
        private readonly CarRentalEntities1 _db;
        public Login()
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SHA256 shaa = SHA256.Create();
                var username = tbUsername.Text.Trim();
                var password = tbPassword.Text;

                var hashedPassword = Utils.HashPassword(password);

                var user = _db.Users.FirstOrDefault(q => q.username == username && q.password == hashedPassword
                &&q.isActive==true);
                if(user == null)
                {
                    MessageBox.Show("Please Provide valid Data");
                }
                else
                {
                    /*var role = user.UserRoles.FirstOrDefault();
                    var roleShortName = role.Role.shortname;*/
                    var mainwindow = new MainWindow(this,user);
                    mainwindow.Show();
                    Hide();
                }
            }catch(Exception )
            {
                MessageBox.Show("Something was wrong : PLesase Try Again");
            }
        }
    }
}
