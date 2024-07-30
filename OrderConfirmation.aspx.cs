using System;
using System.Web.UI;

namespace Food_Ordering_Mangment
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int orderId = int.Parse(Request.QueryString["orderId"]);
                lblOrderNumber.Text = orderId.ToString();
            }
        }
    }
}
