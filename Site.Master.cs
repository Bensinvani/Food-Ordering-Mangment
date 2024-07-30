using System;
using System.Web.UI.WebControls;
using BLL;

namespace Food_Ordering_Mangment
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Login"] != null)
                {
                    User loggedInUser = (User)Session["Login"];
                    loginBtn.Visible = false;
                    logoutBtn.Visible = true;
                    userLink.NavigateUrl = "/UserDetails.aspx";
                    userLink.Text = $"Hello, {loggedInUser.FirstName}";

                    // Check if the user is an admin
                    if (loggedInUser.IsAdmin)
                    {
                        adminLink.Visible = true;
                    }
                }
                else
                {
                    loginBtn.Visible = true;
                    logoutBtn.Visible = false;
                    userLink.NavigateUrl = "/Register.aspx";
                    userLink.Text = "<i class='fa-regular fa-user'></i>";
                    adminLink.Visible = false;
                }
            }
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Session["Login"] = null;
            Response.Redirect("/Home.aspx");
        }
    }
}
