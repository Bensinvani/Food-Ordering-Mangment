using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Food_Ordering_Mangment
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAddress();
                PopulateOrderSummary();
                ddlPaymentMethod_SelectedIndexChanged(null, null);
            }
        }

        private void PopulateAddress()
        {
            int userId = GetCurrentUserId();
            if (userId != 0)
            {
                Address address = Address.GetByUserId(userId);
                if (address != null)
                {
                    txtStreet.Text = address.Street;
                    txtCity.Text = address.City;
                    txtState.Text = address.State;
                    txtZipCode.Text = address.ZipCode;
                    txtCountry.Text = address.Country;
                }
            }
        }

        private void PopulateOrderSummary()
        {
            int userId = GetCurrentUserId();
            List<OrderItem> items;
            decimal totalAmount = 0;

            if (userId == 0)
            {
                items = GetGuestCartItems();
                totalAmount = items.Sum(i => i.TotalPrice);
            }
            else
            {
                Cart cart = Cart.GetById(userId);
                items = cart.Items;
                totalAmount = cart.TotalAmount;
            }

            rptOrderSummary.DataSource = items;
            rptOrderSummary.DataBind();
            lblTotalAmount.Text = $"{totalAmount:C}";
        }

        private List<OrderItem> GetGuestCartItems()
        {
            if (Session["GuestCart"] != null)
            {
                return (List<OrderItem>)Session["GuestCart"];
            }
            return new List<OrderItem>();
        }

        private int GetCurrentUserId()
        {
            if (Session["Login"] != null)
            {
                User loggedInUser = (User)Session["Login"];
                return loggedInUser.UserId;
            }
            return 0; // User not logged in
        }

        protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCreditCard.Visible = ddlPaymentMethod.SelectedValue == "CreditCard";
            pnlPayPal.Visible = ddlPaymentMethod.SelectedValue == "PayPal";
            pnlCash.Visible = ddlPaymentMethod.SelectedValue == "Cash";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string street = txtStreet.Text;
            string city = txtCity.Text;
            string state = txtState.Text;
            string zipCode = txtZipCode.Text;
            string country = txtCountry.Text;
            string paymentMethod = ddlPaymentMethod.SelectedValue;

            int userId = GetCurrentUserId();
            if (userId != 0)
            {
                Address existingAddress = Address.GetByUserId(userId);
                if (existingAddress == null)
                {
                    Address newAddress = new Address
                    {
                        UserId = userId,
                        Street = street,
                        City = city,
                        State = state,
                        ZipCode = zipCode,
                        Country = country
                    };
                    newAddress.Save();
                }
            }

            Order order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = CalculateTotalAmount(),
                Items = GetCartItems(userId)
            };
            order.Save();

            Payment payment = new Payment
            {
                OrderId = order.OrderId,
                PaymentMethod = paymentMethod,
                PaymentDate = DateTime.Now,
                Amount = order.TotalAmount
            };
            payment.Save();

            ClearCart(userId);

            SendOrderConfirmationEmail(order);
            SendOrderConfirmationSMS(order);

            Response.Redirect("OrderConfirmation.aspx?orderId=" + order.OrderId);
        }

        private void ClearCart(int userId)
        {
            if (userId == 0)
            {
                Session["GuestCart"] = null;
            }
            else
            {
                Cart cart = Cart.GetById(userId);
                if (cart != null)
                {
                    cart.Items.Clear();
                    cart.Save();
                }
            }
        }

        private decimal CalculateTotalAmount()
        {
            int userId = GetCurrentUserId();
            Cart cart = Cart.GetById(userId);
            return cart != null ? cart.TotalAmount : 0m;
        }

        private List<OrderItem> GetCartItems(int userId)
        {
            Cart cart = Cart.GetById(userId);
            return cart != null ? cart.Items : new List<OrderItem>();
        }

        protected void Btn_Decrement_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int menuItemId = int.Parse(btn.CommandArgument);
            UpdateQuantity(menuItemId, -1);
        }

        protected void Btn_Increment_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int menuItemId = int.Parse(btn.CommandArgument);
            UpdateQuantity(menuItemId, 1);
        }

        protected void RemoveItem_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int menuItemId = int.Parse(btn.CommandArgument);
            RemoveItem(menuItemId);
        }

        private void UpdateQuantity(int menuItemId, int change)
        {
            int userId = GetCurrentUserId();
            if (userId == 0)
            {
                UpdateGuestCart(menuItemId, change);
            }
            else
            {
                Cart cart = Cart.GetById(userId);
                var item = cart.Items.FirstOrDefault(i => i.MenuItemId == menuItemId);
                if (item != null)
                {
                    item.Quantity += change;
                    if (item.Quantity <= 0)
                    {
                        cart.Items.Remove(item);
                    }
                    cart.Save();
                }
            }
            PopulateOrderSummary();
        }

        private void UpdateGuestCart(int menuItemId, int change)
        {
            List<OrderItem> guestCart = (List<OrderItem>)Session["GuestCart"];
            var item = guestCart.FirstOrDefault(i => i.MenuItemId == menuItemId);
            if (item != null)
            {
                item.Quantity += change;
                if (item.Quantity <= 0)
                {
                    guestCart.Remove(item);
                }
                Session["GuestCart"] = guestCart;
            }
            PopulateOrderSummary();
        }

        private void RemoveItem(int menuItemId)
        {
            int userId = GetCurrentUserId();
            if (userId == 0)
            {
                List<OrderItem> guestCart = (List<OrderItem>)Session["GuestCart"];
                var item = guestCart.FirstOrDefault(i => i.MenuItemId == menuItemId);
                if (item != null)
                {
                    guestCart.Remove(item);
                    Session["GuestCart"] = guestCart;
                }
            }
            else
            {
                Cart cart = Cart.GetById(userId);
                var item = cart.Items.FirstOrDefault(i => i.MenuItemId == menuItemId);
                if (item != null)
                {
                    cart.Items.Remove(item);
                    cart.Save();
                }
            }
            PopulateOrderSummary();
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Orders.aspx");
        }

        private void SendOrderConfirmationEmail(Order order)
        {
            // Code to send order confirmation email
        }

        private void SendOrderConfirmationSMS(Order order)
        {
            // Code to send order confirmation SMS
        }
    }
}
