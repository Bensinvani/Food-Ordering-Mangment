using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BLL;

namespace Food_Ordering_Mangment
{
    public partial class UserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Login"] != null)
                {
                    User loggedInUser = (User)Session["Login"];
                    PopulateUserDetails(loggedInUser);
                    PopulateOrders(loggedInUser.UserId);
                    PopulateContacts(loggedInUser.UserId);
                }
                else
                {
                    Response.Redirect("/Login.aspx");
                }
            }
        }

        private void PopulateUserDetails(User user)
        {
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtEmail.Text = user.Email;
            txtPhoneNumber.Text = user.PhoneNumber;

            Address address = Address.GetByUserId(user.UserId);
            if (address != null)
            {
                txtStreet.Text = address.Street;
                txtCity.Text = address.City;
                txtState.Text = address.State;
                txtZipCode.Text = address.ZipCode;
                txtCountry.Text = address.Country;
            }
        }

        private void PopulateOrders(int userId)
        {
            List<Order> orders = Order.GetByUserId(userId);
            rptOrders.DataSource = orders;
            rptOrders.DataBind();
        }

        private void PopulateContacts(int userId)
        {
            List<BLL.Contact> contacts = BLL.Contact.GetAll().FindAll(c => c.UserId == userId); 
            rptContacts.DataSource = contacts;
            rptContacts.DataBind();
        }

        protected void lnkToggleEdit_Click(object sender, EventArgs e)
        {
            if (lnkToggleEdit.Text.Trim() == "Edit")
            {
                ToggleEditMode(true);
                lnkToggleEdit.Text = "Save";
            }
            else
            {
                SaveUserDetails();
                ToggleEditMode(false);
                lnkToggleEdit.Text = "Edit";
                LtlMsg.Text = "<div class='alert alert-success'>User details updated successfully.</div>";
            }
        }

        private void ToggleEditMode(bool isEditMode)
        {
            txtFirstName.ReadOnly = !isEditMode;
            txtLastName.ReadOnly = !isEditMode;
            txtEmail.ReadOnly = !isEditMode;
            txtPhoneNumber.ReadOnly = !isEditMode;
            txtStreet.ReadOnly = !isEditMode;
            txtCity.ReadOnly = !isEditMode;
            txtState.ReadOnly = !isEditMode;
            txtZipCode.ReadOnly = !isEditMode;
            txtCountry.ReadOnly = !isEditMode;

            var textBoxes = new[] { txtFirstName, txtLastName, txtEmail, txtPhoneNumber, txtStreet, txtCity, txtState, txtZipCode, txtCountry };
            foreach (var textBox in textBoxes)
            {
                if (isEditMode)
                {
                    textBox.CssClass += " editable";
                }
                else
                {
                    textBox.CssClass = textBox.CssClass.Replace(" editable", "");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveUserDetails();
            ToggleEditMode(false);
            lnkToggleEdit.Text = "Edit";
            LtlMsg.Text = "<div class='alert alert-success'>User details updated successfully.</div>";
        }

        private void SaveUserDetails()
        {
            if (Session["Login"] != null)
            {
                User loggedInUser = (User)Session["Login"];
                loggedInUser.FirstName = txtFirstName.Text;
                loggedInUser.LastName = txtLastName.Text;
                loggedInUser.Email = txtEmail.Text;
                loggedInUser.PhoneNumber = txtPhoneNumber.Text;
                loggedInUser.Save();

                Address address = Address.GetByUserId(loggedInUser.UserId) ?? new Address { UserId = loggedInUser.UserId };
                address.Street = txtStreet.Text;
                address.City = txtCity.Text;
                address.State = txtState.Text;
                address.ZipCode = txtZipCode.Text;
                address.Country = txtCountry.Text;
                address.Save();
            }
        }
    }
}
