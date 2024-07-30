using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class PaymentDAL
    {
        public static List<Payment> GetAll()
        {
            FoodDb db = new FoodDb();
            string sql = "SELECT * FROM T_Payment";
            DataTable dt = db.Execute(sql);
            List<Payment> payments = new List<Payment>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Payment payment = new Payment()
                {
                    PaymentId = int.Parse(dt.Rows[i]["PaymentId"].ToString()),
                    OrderId = int.Parse(dt.Rows[i]["OrderId"].ToString()),
                    PaymentMethod = dt.Rows[i]["PaymentMethod"].ToString(),
                    PaymentDate = DateTime.Parse(dt.Rows[i]["PaymentDate"].ToString()),
                    Amount = decimal.Parse(dt.Rows[i]["Amount"].ToString())
                };
                payments.Add(payment);
            }
            db.Close();
            return payments;
        }

        public static Payment GetById(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_Payment WHERE PaymentId={id}";
            DataTable dt = db.Execute(sql);
            Payment payment = null;
            if (dt.Rows.Count > 0)
            {
                payment = new Payment()
                {
                    PaymentId = int.Parse(dt.Rows[0]["PaymentId"].ToString()),
                    OrderId = int.Parse(dt.Rows[0]["OrderId"].ToString()),
                    PaymentMethod = dt.Rows[0]["PaymentMethod"].ToString(),
                    PaymentDate = DateTime.Parse(dt.Rows[0]["PaymentDate"].ToString()),
                    Amount = decimal.Parse(dt.Rows[0]["Amount"].ToString())
                };
            }
            db.Close();
            return payment;
        }

        public static void Save(Payment payment)
        {
            FoodDb db = new FoodDb();
            string sql = "";
            string formattedDate = payment.PaymentDate.ToString("yyyy-MM-ddTHH:mm:ss"); // Use a culture-invariant format

            if (payment.PaymentId == 0)
            {
                sql = $"INSERT INTO T_Payment (OrderId, PaymentMethod, PaymentDate, Amount) VALUES ({payment.OrderId}, '{payment.PaymentMethod}', '{formattedDate}', {payment.Amount})";
            }
            else
            {
                sql = $"UPDATE T_Payment SET OrderId={payment.OrderId}, PaymentMethod='{payment.PaymentMethod}', PaymentDate='{formattedDate}', Amount={payment.Amount} WHERE PaymentId={payment.PaymentId}";
            }
            db.ExecuteNonQuery(sql);
            db.Close();
        }


        public static void Delete(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"DELETE FROM T_Payment WHERE PaymentId={id}";
            db.ExecuteNonQuery(sql);
            db.Close();
        }
    }
}