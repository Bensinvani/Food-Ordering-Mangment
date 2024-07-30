using BLL;
using System;
using System.Web.UI;

namespace Food_Ordering_Mangment.AdminManager
{
    public partial class AddPayment : Page
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
                string paymentId = Request["PaymentId"] + "";
                string action = Request["action"] + "";

                if (paymentId == "")
                {
                    paymentId = "-1";
                }

                HidPaymentId.Value = paymentId;

                if (action == "delete" && paymentId != "-1")
                {
                    Payment.Delete(int.Parse(paymentId));
                    HidPaymentId.Value = "-1";
                    BindPaymentsGrid();
                    return;
                }

                if (paymentId != "-1")
                {
                    Payment payment = Payment.GetById(int.Parse(paymentId));
                    if (payment != null)
                    {
                        txtOrderId.Text = payment.OrderId.ToString();
                        txtPaymentMethod.Text = payment.PaymentMethod;
                        txtPaymentDate.Text = payment.PaymentDate.ToString("yyyy-MM-dd HH:mm:ss");
                        txtAmount.Text = payment.Amount.ToString();
                    }
                    else
                    {
                        HidPaymentId.Value = "-1";
                    }
                }

                BindPaymentsGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int orderId;
            decimal amount;
            DateTime paymentDate;
            if (!int.TryParse(txtOrderId.Text.Trim(), out orderId) || !decimal.TryParse(txtAmount.Text.Trim(), out amount) ||
                !DateTime.TryParse(txtPaymentDate.Text.Trim(), out paymentDate))
            {
                lblMessage.Text = "Invalid input values.";
                return;
            }

            Payment payment = new Payment
            {
                PaymentId = int.Parse(HidPaymentId.Value),
                OrderId = orderId,
                PaymentMethod = txtPaymentMethod.Text.Trim(),
                PaymentDate = paymentDate,
                Amount = amount
            };

            payment.Save();

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Payment saved successfully!";
            ClearFields();
            BindPaymentsGrid();
        }

        private void ClearFields()
        {
            txtOrderId.Text = string.Empty;
            txtPaymentMethod.Text = string.Empty;
            txtPaymentDate.Text = string.Empty;
            txtAmount.Text = string.Empty;
        }

        private void BindPaymentsGrid()
        {
            gvPayments.DataSource = Payment.GetAll();
            gvPayments.DataBind();
        }
    }
}
