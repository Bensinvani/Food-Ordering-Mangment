using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Food_Ordering_Mangment
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Login"] != null)
                {
                    User loggedInUser = (User)Session["Login"];
                    LoginContainer.Visible = false;
                    LtlLoggedUser.Text = $"Hello {loggedInUser.FirstName} {loggedInUser.LastName}";
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            List<User> users = BLL.User.GetAll();
            User authenticatedUser = users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (authenticatedUser != null)
            {
                Session["Login"] = authenticatedUser;

                if (authenticatedUser.IsAdmin)
                {
                    Response.Redirect("/AdminManager/AdminManage.aspx");
                }
                else
                {
                        Response.Redirect("Home.aspx");               
                }
            }
            else
            {
                lblErrorMessage.Text = "Invalid email or password.";
                lblErrorMessage.Visible = true;
            }
        }
    }
}
