using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class OrderDAL
    {
        public static List<Order> GetByUserId(int userId)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_Order WHERE UserId = {userId}";
            DataTable dt = db.Execute(sql);
            List<Order> orders = new List<Order>();

            foreach (DataRow row in dt.Rows)
            {
                Order order = new Order
                {
                    OrderId = int.Parse(row["OrderId"].ToString()),
                    UserId = int.Parse(row["UserId"].ToString()),
                    OrderDate = DateTime.Parse(row["OrderDate"].ToString()),
                    TotalAmount = decimal.Parse(row["TotalAmount"].ToString()),
                    Items = OrderItemDAL.GetByOrderId(int.Parse(row["OrderId"].ToString()))
                };
                orders.Add(order);
            }
            db.Close();
            return orders;
        }

        public static List<Order> GetAll()
        {
            FoodDb db = new FoodDb();
            string sql = "SELECT * FROM T_Order";
            DataTable dt = db.Execute(sql);
            List<Order> orders = new List<Order>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Order order = new Order()
                {
                    OrderId = int.Parse(dt.Rows[i]["OrderId"].ToString()),
                    UserId = int.Parse(dt.Rows[i]["UserId"].ToString()),
                    OrderDate = DateTime.Parse(dt.Rows[i]["OrderDate"].ToString()),
                    TotalAmount = decimal.Parse(dt.Rows[i]["TotalAmount"].ToString()),
                    Items = OrderItemDAL.GetByOrderId(int.Parse(dt.Rows[i]["OrderId"].ToString()))
                };
                orders.Add(order);
            }
            db.Close();
            return orders;
        }

        public static Order GetById(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_Order WHERE OrderId={id}";
            DataTable dt = db.Execute(sql);
            Order order = null;
            if (dt.Rows.Count > 0)
            {
                order = new Order()
                {
                    OrderId = int.Parse(dt.Rows[0]["OrderId"].ToString()),
                    UserId = int.Parse(dt.Rows[0]["UserId"].ToString()),
                    OrderDate = DateTime.Parse(dt.Rows[0]["OrderDate"].ToString()),
                    TotalAmount = decimal.Parse(dt.Rows[0]["TotalAmount"].ToString()),
                    Items = OrderItemDAL.GetByOrderId(int.Parse(dt.Rows[0]["OrderId"].ToString()))
                };
            }
            db.Close();
            return order;
        }

        public static void Save(Order order)
        {
            FoodDb db = new FoodDb();
            string sql = "";
            // Format the date to a culture-invariant format
            string formattedDate = order.OrderDate.ToString("yyyy-MM-ddTHH:mm:ss");

            if (order.OrderId == 0)
            {
                // Insert the order and get the new OrderId
                sql = $"INSERT INTO T_Order (UserId, OrderDate, TotalAmount) OUTPUT INSERTED.OrderId VALUES ({order.UserId}, '{formattedDate}', {order.TotalAmount})";
                DataTable dt = db.Execute(sql);
                if (dt.Rows.Count > 0)
                {
                    order.OrderId = Convert.ToInt32(dt.Rows[0]["OrderId"]);
                }
            }
            else
            {
                sql = $"UPDATE T_Order SET UserId={order.UserId}, OrderDate='{formattedDate}', TotalAmount={order.TotalAmount} WHERE OrderId={order.OrderId}";
                db.ExecuteNonQuery(sql);
            }

            // Now save the OrderItems with the correct OrderId
            foreach (var item in order.Items)
            {
                item.OrderId = order.OrderId;
                OrderItemDAL.Save(item);
            }

            db.Close();
        }

        public static void Delete(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"DELETE FROM T_Order WHERE OrderId={id}";
            db.ExecuteNonQuery(sql);
            db.Close();
        }
    }
}
