using System;
using System.Data;
using System.Web.UI.WebControls;
using BLL;
using DAL;

namespace Food_Ordering_Mangment.AdminManager
{
    public partial class AdminManage : System.Web.UI.Page
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
                LoadStatistics();
                LoadUsers();
                LoadOrders();
                LoadContacts();
            }
        }

        private void LoadStatistics()
        {
            lblTotalUsers.Text = UserDAL.GetAll().Count.ToString();
            lblTotalOrders.Text = OrderDAL.GetAll().Count.ToString();
            lblTotalContacts.Text = ContactDAL.GetAll().Count.ToString(); 
        }

        private void LoadUsers()
        {
            var users = UserDAL.GetAll();
            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }

        private void LoadOrders()
        {
            var orders = OrderDAL.GetAll();
            gvOrders.DataSource = orders;
            gvOrders.DataBind();
        }

        private void LoadContacts()
        {
            var contacts = ContactDAL.GetAll(); 
            gvContacts.DataSource = contacts;
            gvContacts.DataBind();
        }
        protected void gvContacts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reply")
            {
                int contactId = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                TextBox txtReply = (TextBox)row.FindControl("txtReply");

                BLL.Contact contact = BLL.Contact.GetById(contactId);
                contact.IsReplied = true; // Update IsReplied status
                contact.Save();

                // Optionally, you can save the reply message to a new table or send an email to the user.

                LoadContacts(); // Refresh the grid to show updated status
            }
        }

    }
}
