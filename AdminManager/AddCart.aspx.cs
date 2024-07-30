using BLL;
using System;
using System.Web.UI;

namespace Food_Ordering_Mangment.AdminManager
{
    public partial class AddCart : Page
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
                string cartId = Request["CartId"] + "";
                string action = Request["action"] + "";

                if (cartId == "")
                {
                    cartId = "-1";
                }

                HidCartId.Value = cartId;

                if (action == "delete" && cartId != "-1")
                {
                    Cart.Delete(int.Parse(cartId));
                    HidCartId.Value = "-1";
                    BindCartsGrid();
                    return;
                }

                if (cartId != "-1")
                {
                    Cart cart = Cart.GetById(int.Parse(cartId));
                    if (cart != null)
                    {
                        txtUserId.Text = cart.UserId.ToString();
                        // Assuming you want to edit one of the items
                        if (cart.Items.Count > 0)
                        {
                            var item = cart.Items[0];
                            txtMenuItemId.Text = item.MenuItemId.ToString();
                            txtQuantity.Text = item.Quantity.ToString();
                            txtUnitPrice.Text = item.UnitPrice.ToString();
                        }
                    }
                    else
                    {
                        HidCartId.Value = "-1";
                    }
                }

                BindCartsGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userId, menuItemId, quantity;
            decimal unitPrice;
            if (!int.TryParse(txtUserId.Text.Trim(), out userId) || !int.TryParse(txtMenuItemId.Text.Trim(), out menuItemId) ||
                !int.TryParse(txtQuantity.Text.Trim(), out quantity) || !decimal.TryParse(txtUnitPrice.Text.Trim(), out unitPrice))
            {
                lblMessage.Text = "Invalid input values.";
                return;
            }

            Cart cart = new Cart
            {
                UserId = userId
            };

            cart.AddItem(new MenuItem { MenuItemId = menuItemId, Price = unitPrice }, quantity);
            cart.Save();

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Cart saved successfully!";
            ClearFields();
            BindCartsGrid();
        }

        private void ClearFields()
        {
            txtUserId.Text = string.Empty;
            txtMenuItemId.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
        }

        private void BindCartsGrid()
        {
            gvCarts.DataSource = Cart.GetAll();
            gvCarts.DataBind();
        }
    }
}
