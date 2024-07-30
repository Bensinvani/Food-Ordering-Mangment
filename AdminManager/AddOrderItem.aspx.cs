using BLL;
using System;
using System.Web.UI;

namespace Food_Ordering_Mangment.AdminManager
{
    public partial class AddOrderItem : Page
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
                string orderItemId = Request["OrderItemId"] + "";
                string action = Request["action"] + "";

                if (orderItemId == "")
                {
                    orderItemId = "-1";
                }

                HidOrderItemId.Value = orderItemId;

                if (action == "delete" && orderItemId != "-1")
                {
                    OrderItem.Delete(int.Parse(orderItemId));
                    HidOrderItemId.Value = "-1";
                    BindOrderItemsGrid();
                    return;
                }

                if (orderItemId != "-1")
                {
                    OrderItem orderItem = OrderItem.GetById(int.Parse(orderItemId));
                    if (orderItem != null)
                    {
                        txtOrderId.Text = orderItem.OrderId.ToString();
                        txtMenuItemId.Text = orderItem.MenuItemId.ToString();
                        txtQuantity.Text = orderItem.Quantity.ToString();
                        txtUnitPrice.Text = orderItem.UnitPrice.ToString();
                    }
                    else
                    {
                        HidOrderItemId.Value = "-1";
                    }
                }

                BindOrderItemsGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int orderId, menuItemId, quantity;
            decimal unitPrice;
            if (!int.TryParse(txtOrderId.Text.Trim(), out orderId) || !int.TryParse(txtMenuItemId.Text.Trim(), out menuItemId) ||
                !int.TryParse(txtQuantity.Text.Trim(), out quantity) || !decimal.TryParse(txtUnitPrice.Text.Trim(), out unitPrice))
            {
                lblMessage.Text = "Invalid input values.";
                return;
            }

            OrderItem orderItem = new OrderItem
            {
                OrderItemId = int.Parse(HidOrderItemId.Value),
                OrderId = orderId,
                MenuItemId = menuItemId,
                Quantity = quantity,
                UnitPrice = unitPrice
            };

            orderItem.Save();

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Order item saved successfully!";
            ClearFields();
            BindOrderItemsGrid();
        }

        private void ClearFields()
        {
            txtOrderId.Text = string.Empty;
            txtMenuItemId.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
        }

        private void BindOrderItemsGrid()
        {
            gvOrderItems.DataSource = OrderItem.GetAll();
            gvOrderItems.DataBind();
        }
    }
}
