using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Food_Ordering_Mangment.AdminManager
{
    public partial class AddUser : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is an admin
            if (Session["Login"] == null || !((User)Session["Login"]).IsAdmin)
            {
                Response.Redirect("/Home.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string userId = Request["UserId"] ?? "0";
                string action = Request["action"] ?? "";

                HidUserId.Value = userId;

                if (action == "delete" && userId != "0")
                {
                    UserDAL.Delete(int.Parse(userId));
                    HidUserId.Value = "-1";
                    BindUsersGrid();
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "User deleted successfully!";
                    return;
                }

                if (userId != "0")
                {
                    LoadUserDetails(int.Parse(userId));
                }

                BindUsersGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<User> users = BLL.User.GetAll();
            int userId = int.Parse(HidUserId.Value);
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();

            bool isDuplicate = users.Any(u => (u.Email == email || u.Username == username) && u.UserId != userId);

            if (isDuplicate)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "A user with this email or username already exists!";
                return;
            }

            User user = new User
            {
                UserId = userId,
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Username = username,
                Email = email,
                Password = txtPassword.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                IsAdmin = chkIsAdmin.Checked,
                IsPasswordConfirmed = chkIsPasswordConfirmed.Checked
            };

            user.Save();

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "User saved successfully!";
            ClearFields();
            BindUsersGrid();
        }

        private void LoadUserDetails(int userId)
        {
            User user = UserDAL.GetById(userId);
            if (user != null)
            {
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtUsername.Text = user.Username;
                txtEmail.Text = user.Email;
                txtPassword.Text = user.Password;
                txtPhoneNumber.Text = user.PhoneNumber;
                chkIsAdmin.Checked = user.IsAdmin;
                chkIsPasswordConfirmed.Checked = user.IsPasswordConfirmed;
            }
            else
            {
                HidUserId.Value = "-1";
            }
        }

        private void ClearFields()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            chkIsAdmin.Checked = false;
            chkIsPasswordConfirmed.Checked = false;
            HidUserId.Value = "-1";
        }

        private void BindUsersGrid()
        {
            gvUsers.DataSource = UserDAL.GetAll();
            gvUsers.DataBind();
        }
    }
}
