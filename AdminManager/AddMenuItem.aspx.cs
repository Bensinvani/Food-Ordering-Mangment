using BLL;
using System;
using System.Web.UI;

namespace Food_Ordering_Mangment.AdminManager
{
    public partial class AddMenuItem : Page
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
                BindCategories();
                BindMenuItemsGrid();
                string menuItemId = Request["MenuItemId"];
                string action = Request["action"];

                if (menuItemId == null)
                {
                    menuItemId = "0";
                }

                HidMenuItemId.Value = menuItemId;

                if (action == "delete" && menuItemId != "0")
                {
                    MenuItem.Delete(int.Parse(menuItemId));
                    HidMenuItemId.Value = "0";
                    BindMenuItemsGrid();
                    return;
                }

                if (menuItemId != "0")
                {
                    MenuItem Tmp = MenuItem.GetById(int.Parse(menuItemId));
                    if (Tmp != null)
                    {
                        txtName.Text = Tmp.Name;
                        txtDescription.Text = Tmp.Description;
                        txtPrice.Text = Tmp.Price.ToString();
                        ddlCategory.SelectedValue = Tmp.CategoryId.ToString();
                        txtImageUrl.Text = Tmp.ImageUrl;
                    }
                    else
                    {
                        HidMenuItemId.Value = "0";
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string imageUrl = txtImageUrl.Text.Trim();
            decimal price;

            if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
            {
                lblMessage.Text = "Invalid price.";
                return;
            }

            int categoryId = int.Parse(ddlCategory.SelectedValue);

            if (string.IsNullOrEmpty(name))
            {
                lblMessage.Text = "Name is required.";
                return;
            }

            MenuItem menuItem = new MenuItem
            {
                MenuItemId = int.Parse(HidMenuItemId.Value),
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Price = price,
                CategoryId = categoryId
            };

            menuItem.Save();

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Menu item added successfully!";
            ClearFields();
            BindMenuItemsGrid();
        }

        private void ClearFields()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtImageUrl.Text = string.Empty;
            ddlCategory.SelectedIndex = 0;
        }

        private void BindCategories()
        {
            ddlCategory.DataSource = Category.GetAll();
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
        }

        private void BindMenuItemsGrid()
        {
            gvMenuItems.DataSource = MenuItem.GetAll();
            gvMenuItems.DataBind();
        }
    }
}
