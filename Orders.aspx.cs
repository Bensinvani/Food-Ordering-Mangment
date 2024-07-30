using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Food_Ordering_Mangment
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrders();
            }
        }

        private void BindOrders()
        {
            int userId = GetCurrentUserId();
            List<OrderItem> items;
            decimal totalAmount = 0;

            if (userId == -1)
            {
                items = GetGuestCartItems();
                if (items == null || items.Count == 0)
                {
                    ShowEmptyCartMessage();
                    return;
                }
                totalAmount = items.Sum(i => i.TotalPrice);
                var guestCart = new Cart { UserId = userId, Items = items };
                OrdersRepeater.DataSource = new List<Cart> { guestCart };
            }
            else
            {
                Cart cart = Cart.GetById(userId);
                if (cart == null || cart.Items.Count == 0)
                {
                    ShowEmptyCartMessage();
                    return;
                }
                totalAmount = cart.TotalAmount;
                OrdersRepeater.DataSource = new List<Cart> { cart };
            }
            OrdersRepeater.DataBind();
            CheckoutButton.Visible = true;
            EmptyCartPanel.Visible = false;
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
            return -1; 
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
            if (userId == -1)
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
            BindOrders();
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
            BindOrders();
        }

        private void RemoveItem(int menuItemId)
        {
            int userId = GetCurrentUserId();
            if (userId == -1)
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
            BindOrders();
        }

        private void ShowEmptyCartMessage()
        {
            OrdersRepeater.DataSource = null;
            OrdersRepeater.DataBind();
            CheckoutButton.Visible = false;
            EmptyCartPanel.Visible = true;
        }

        protected void CheckoutButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Checkout.aspx");
        }
    }
}
