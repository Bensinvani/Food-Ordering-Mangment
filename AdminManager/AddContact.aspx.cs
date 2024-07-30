using BLL;
using System;
using System.Web.UI;

namespace Food_Ordering_Mangment.AdminManager
{
    public partial class AddContact : System.Web.UI.Page
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
                string contactId = Request["ContactId"] ?? "-1";
                string action = Request["action"] ?? "";

                HidContactId.Value = contactId;

                if (action == "delete" && contactId != "-1")
                {
                    BLL.Contact.Delete(int.Parse(contactId));
                    HidContactId.Value = "-1";
                    BindContactsGrid();
                    return;
                }

                if (contactId != "-1")
                {
                    BLL.Contact Tmp = BLL.Contact.GetById(int.Parse(contactId));
                    if (Tmp != null)
                    {
                        txtName.Text = Tmp.Name;
                        txtEmail.Text = Tmp.Email;
                        txtMessage.Text = Tmp.Message;
                    }
                    else
                    {
                        HidContactId.Value = "-1";
                    }
                }

                BindContactsGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                lblMessage.Text = "Name and Email are required.";
                return;
            }

            BLL.Contact contact = new BLL.Contact
            {
                ContactId = int.Parse(HidContactId.Value),
                Name = name,
                Email = email,
                Message = message,
                Date = DateTime.Now
            };

            contact.Save();

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Contact saved successfully!";
            ClearFields();
            BindContactsGrid();
        }

        private void ClearFields()
        {
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMessage.Text = string.Empty;
        }

        private void BindContactsGrid()
        {
            gvContacts.DataSource = BLL.Contact.GetAll();
            gvContacts.DataBind();
        }
    }
}
