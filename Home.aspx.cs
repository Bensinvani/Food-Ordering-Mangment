using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Food_Ordering_Mangment
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCarousel();
                BindFeaturedItems();
            }
        }

        private void BindCarousel()
        {
            List<BLL.MenuItem> randomProducts = GetRandomProducts(3); // Get 3 random products
            ProductCarouselRepeater.DataSource = randomProducts;
            ProductCarouselRepeater.DataBind();
            CarouselIndicatorsRepeater.DataSource = randomProducts;
            CarouselIndicatorsRepeater.DataBind();
        }

        private void BindFeaturedItems()
        {
            List<BLL.MenuItem> randomFeaturedItems = GetRandomProducts(3); // Get 3 random products for featured items
            FeaturedItemsRepeater.DataSource = randomFeaturedItems;
            FeaturedItemsRepeater.DataBind();
        }

        private List<BLL.MenuItem> GetRandomProducts(int count)
        {
            List<BLL.MenuItem> allProducts = BLL.MenuItem.GetAll();
            Random random = new Random();
            return allProducts.OrderBy(x => random.Next()).Take(count).ToList();
        }
    }
}
