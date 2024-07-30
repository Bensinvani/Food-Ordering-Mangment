using BLL;
using System;
using System.Web.UI;

namespace Food_Ordering_Mangment.AdminManager
{
    public partial class AddOrder : Page
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
                string orderId = Request["OrderId"] + "";
                string action = Request["action"] + "";

                if (orderId == "")
                {
                    orderId = "-1";
                }

                HidOrderId.Value = orderId;

                if (action == "delete" && orderId != "-1")
                {
                    Order.Delete(int.Parse(orderId));
                    HidOrderId.Value = "-1";
                    BindOrdersGrid();
                    return;
                }

                if (orderId != "-1")
                {
                    Order order = Order.GetById(int.Parse(orderId));
                    if (order != null)
                    {
                        txtUserId.Text = order.UserId.ToString();
                        txtTotalAmount.Text = order.TotalAmount.ToString();
                        txtOrderDate.Text = order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        HidOrderId.Value = "-1";
                    }
                }

                BindOrdersGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userId;
            decimal totalAmount;
            DateTime orderDate;
            if (!int.TryParse(txtUserId.Text.Trim(), out userId) || !decimal.TryParse(txtTotalAmount.Text.Trim(), out totalAmount) ||
                !DateTime.TryParse(txtOrderDate.Text.Trim(), out orderDate))
            {
                lblMessage.Text = "Invalid input values.";
                return;
            }

            Order order = new Order
            {
                OrderId = int.Parse(HidOrderId.Value),
                UserId = userId,
                TotalAmount = totalAmount,
                OrderDate = orderDate
            };

            order.Save();

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Order saved successfully!";
            ClearFields();
            BindOrdersGrid();
        }

        private void ClearFields()
        {
            txtUserId.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            txtOrderDate.Text = string.Empty;
        }

        private void BindOrdersGrid()
        {
            gvOrders.DataSource = Order.GetAll();
            gvOrders.DataBind();
        }
    }
}
