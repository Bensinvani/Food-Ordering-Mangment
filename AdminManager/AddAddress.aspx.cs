using BLL;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Food_Ordering_Mangment.AdminManager
{
    public partial class AddAddress : Page
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
                PopulateUsersDropDown();
                string addressId = Request["AddressId"] + "";
                string action = Request["action"] + "";

                if (addressId == "")
                {
                    addressId = "-1";
                }

                HidAddressId.Value = addressId;

                if (action == "delete" && addressId != "-1")
                {
                    Address.Delete(int.Parse(addressId));
                    HidAddressId.Value = "-1";
                    BindAddressesGrid();
                    return;
                }

                if (addressId != "-1")
                {
                    Address address = Address.GetById(int.Parse(addressId));
                    if (address != null)
                    {
                        ddlUsers.SelectedValue = address.UserId.ToString();
                        txtStreet.Text = address.Street;
                        txtCity.Text = address.City;
                        txtState.Text = address.State;
                        txtZipCode.Text = address.ZipCode;
                        txtCountry.Text = address.Country;
                    }
                    else
                    {
                        HidAddressId.Value = "-1";
                    }
                }

                BindAddressesGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userId;
            if (!int.TryParse(ddlUsers.SelectedValue, out userId))
            {
                lblMessage.Text = "Please select a valid User.";
                return;
            }

            Address address = new Address
            {
                AddressId = int.Parse(HidAddressId.Value),
                UserId = userId,
                Street = txtStreet.Text.Trim(),
                City = txtCity.Text.Trim(),
                State = txtState.Text.Trim(),
                ZipCode = txtZipCode.Text.Trim(),
                Country = txtCountry.Text.Trim()
            };

            address.Save();

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Address saved successfully!";
            ClearFields();
            BindAddressesGrid();
        }

        private void ClearFields()
        {
            ddlUsers.SelectedIndex = 0;
            txtStreet.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            txtCountry.Text = string.Empty;
        }

        private void BindAddressesGrid()
        {
            gvAddresses.DataSource = Address.GetAll();
            gvAddresses.DataBind();
        }

        private void PopulateUsersDropDown()
        {
            ddlUsers.DataSource = BLL.User.GetAll();
            ddlUsers.DataTextField = "Username"; // Assuming the User class has a property 'Username'
            ddlUsers.DataValueField = "UserId";
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, new ListItem("Select User", "0"));
        }
    }
}
