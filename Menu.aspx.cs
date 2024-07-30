using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Food_Ordering_Mangment
{
    public partial class Menu : System.Web.UI.Page
    {
        protected string CategoryName = "Recommended in the restaurant";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["MenuItemId"]) && string.IsNullOrEmpty(Request.QueryString["CategoryId"]) && string.IsNullOrEmpty(Request.QueryString["Search"]))
                {
                    Response.Redirect("Menu.aspx?CategoryId=1");
                }
                else
                {
                    BindCategories();
                    BindMenuItems();
                }
            }
        }

        private void BindCategories()
        {
            List<Category> categories = Category.GetAll();
            CategoryRepeater.DataSource = categories;
            CategoryRepeater.DataBind();
        }

        private void BindMenuItems()
        {
            List<BLL.MenuItem> menuItems;
            string menuItemId = Request.QueryString["MenuItemId"];
            string categoryId = Request.QueryString["CategoryId"];
            string search = Request.QueryString["Search"];

            if (!string.IsNullOrEmpty(menuItemId))
            {
                int itemId = int.Parse(menuItemId);
                menuItems = new List<BLL.MenuItem> { BLL.MenuItem.GetById(itemId) };
                CategoryName = "Item Details";
            }
            else if (!string.IsNullOrEmpty(search))
            {
                menuItems = BLL.MenuItem.GetAll().Where(m => m.Name.Contains(search) || m.Description.Contains(search)).ToList();
                CategoryName = $"Search Results for '{search}'";
            }
            else if (!string.IsNullOrEmpty(categoryId))
            {
                int catId = int.Parse(categoryId);
                menuItems = BLL.MenuItem.GetAll().Where(m => m.CategoryId == catId).ToList();
                Category category = Category.GetById(catId);
                CategoryName = category != null ? category.CategoryName : "Category";
            }
            else
            {
                menuItems = BLL.MenuItem.GetAll().Where(m => m.CategoryName == "Recommended").ToList();
            }

            MenuItemsRepeater.DataSource = menuItems;
            MenuItemsRepeater.DataBind();
        }

        protected void Btn_Decrement_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int menuItemId = int.Parse(btn.CommandArgument);
            var txtBox = (TextBox)btn.NamingContainer.FindControl("Quantity");
            int quantity = int.Parse(txtBox.Text);
            if (quantity > 1)
            {
                txtBox.Text = (quantity - 1).ToString();
            }
            UpdatePanel1.Update();
        }

        protected void Btn_Increment_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int menuItemId = int.Parse(btn.CommandArgument);
            var txtBox = (TextBox)btn.NamingContainer.FindControl("Quantity");
            int quantity = int.Parse(txtBox.Text);
            txtBox.Text = (quantity + 1).ToString();
            UpdatePanel1.Update();
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int menuItemId = int.Parse(btn.CommandArgument);
            var txtBox = (TextBox)btn.NamingContainer.FindControl("Quantity");
            int quantity = int.Parse(txtBox.Text);
            var lblItemMessage = (Label)btn.NamingContainer.FindControl("ItemMessage");

            int userId = GetCurrentUserId();
            if (userId == -1)
            {
                AddToGuestCart(menuItemId, quantity);
            }
            else
            {
                BLL.MenuItem menuItem = BLL.MenuItem.GetById(menuItemId);
                try
                {
                    Cart cart = Cart.GetById(userId);
                    if (cart == null)
                    {
                        cart = new Cart() { UserId = userId };
                    }
                    cart.AddItem(menuItem, quantity);
                    cart.Save();
                }
                catch (Exception ex)
                {
                    lblItemMessage.Text = $"Error: {ex.Message}";
                    lblItemMessage.ForeColor = System.Drawing.Color.Red;
                    lblItemMessage.Visible = true;
                    UpdatePanel1.Update();
                    return;
                }
            }

            txtBox.Text = "1";
            lblItemMessage.Text = "The item has been added to the cart successfully.";
            lblItemMessage.ForeColor = System.Drawing.Color.Green;
            lblItemMessage.Visible = true;
            lblItemMessage.Attributes.Add("style", "display:block;");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideMessage", $"hideMessage('{lblItemMessage.ClientID}');", true);
            UpdatePanel1.Update();
        }

        private void AddToGuestCart(int menuItemId, int quantity)
        {
            List<OrderItem> guestCart;
            if (Session["GuestCart"] == null)
            {
                guestCart = new List<OrderItem>();
            }
            else
            {
                guestCart = (List<OrderItem>)Session["GuestCart"];
            }

            var item = guestCart.FirstOrDefault(i => i.MenuItemId == menuItemId);
            if (item == null)
            {
                BLL.MenuItem menuItem = BLL.MenuItem.GetById(menuItemId);
                guestCart.Add(new OrderItem
                {
                    MenuItemId = menuItem.MenuItemId,
                    Quantity = quantity,
                    UnitPrice = menuItem.Price
                });
            }
            else
            {
                item.Quantity += quantity;
            }

            Session["GuestCart"] = guestCart;
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

        protected void searchBox_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = searchBox.Text;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                List<BLL.MenuItem> menuItems = BLL.MenuItem.GetAll()
                    .Where(m => m.Name.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();             
            }
        }

        [WebMethod]
        public static List<string> GetMenuItems(string prefixText, int count)
        {
            List<BLL.MenuItem> menuItems = BLL.MenuItem.GetAll()
                .Where(m => m.Name.StartsWith(prefixText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return menuItems.Select(m => m.Name).ToList();
        }
    }
}
