using BLL;
using System;
using System.Web.UI;

namespace Food_Ordering_Mangment
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Login"] != null)
                {
                    User loggedInUser = (User)Session["Login"];
                    txtFirstName.Text = loggedInUser.FirstName;
                    txtLastName.Text = loggedInUser.LastName;
                    txtEmail.Text = loggedInUser.Email;
                    txtPhone.Text = loggedInUser.PhoneNumber;
                    txtUsername.Text = loggedInUser.Username;
                    txtFirstName.Enabled = false;
                    txtLastName.Enabled = false;
                    txtEmail.Enabled = false;
                    txtPhone.Enabled = false;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int? userId = null;
            if (Session["Login"] != null)
            {
                User loggedInUser = (User)Session["Login"];
                userId = loggedInUser.UserId;
            }

            BLL.Contact message = new BLL.Contact
            {
                UserId = userId,
                Name = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}",
                Email = txtEmail.Text.Trim(),
                Message = $"Issue Type: {ddlIssueType.SelectedValue}\n\n{txtMessage.Text.Trim()}",
                Date = DateTime.Now,
                IsReplied = false
            };

            message.Save();
            lblMessage.Text = "Message sent successfully!";
            lblMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "hideMessage", "hideMessage();", true);
            ClearFields();
        }

        private void ClearFields()
        {
            if (Session["Login"] == null)
            {
                txtFirstName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtUsername.Text = string.Empty;
            }
            ddlIssueType.SelectedIndex = 0;
            txtMessage.Text = string.Empty;
        }
    }
}
