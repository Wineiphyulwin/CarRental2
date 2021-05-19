using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    class Utils
    {
        public static bool FormIsOpen(string name)

        {
            //check is windows is already open
            var OpenForms = Application.OpenForms.Cast<Form>();
            var isOpen = OpenForms.Any(q => q.Name == name);
            return isOpen;
        }

        public static string HashPassword(string password)
        {
            SHA256 shaa = SHA256.Create();
            //var username = tbUsername.Text.Trim();
           // var password = tbPassword.Text;
           
            //convert string to byte
            byte[] data = shaa.ComputeHash(Encoding.UTF8.GetBytes(password));
            //create stringbuilder and string

            StringBuilder s = new StringBuilder();
            //loop bytes of data and format
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
           return  s.ToString();
        }

        public static string DefaultHashPassword()
        {
            SHA256 shaa = SHA256.Create();
            //var username = tbUsername.Text.Trim();
            // var password = tbPassword.Text;

            //convert string to byte
            byte[] data = shaa.ComputeHash(Encoding.UTF8.GetBytes("Password@123"));
            //create stringbuilder and string
           //String  original buffer is copied to the new buffer, and the new data is then appended to the new buffer.
            StringBuilder s = new StringBuilder();
            //loop bytes of data and format
            //hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
                //Apeend method =adds an item to the end of the list. 
            }
            return s.ToString();
        }
    }
}
