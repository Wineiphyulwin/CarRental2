﻿using System;
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
    public partial class ResetPassword : Form
    {
        private readonly CarRentalEntities1 _db;
        private User _user;
        public ResetPassword(User user)
        {
            InitializeComponent();
            _user = user;
            _db = new CarRentalEntities1();
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var password = tbNewPassword.Text;
            var confirmpassword = tbConfirmPassword.Text;
            var user = _db.Users.FirstOrDefault(q=> q.id == _user.id );
            if (password!=confirmpassword)
            {
                MessageBox.Show("Password doesn't match! Please Try Again");
            }
            user.password = Utils.HashPassword(password);
            _db.SaveChanges();
            MessageBox.Show("Reset Successfully");
            Close();
        }
    }
}
