using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Food_Ordering_Mangment
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string phoneNumber = txtPhoneNumber.Text;

            List<User> users = BLL.User.GetAll();

            // Check if the user already exists
            bool userExists = users.Any(u => u.Email == email || u.Username == username);

            if (userExists)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "A user with this email or username already exists!";
                return;
            }

            // Create a new user
            User newUser = new User
            {
                UserId = 0, // Ensure UserId is 0 for new users
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
                IsPasswordConfirmed = true,
                IsAdmin = false
            };

            newUser.Save();
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Registration successful!";
            ClearFields();
        }

        private void ClearFields()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
        }
    }
}
