using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class OrderItemDAL
    {
        public static List<OrderItem> GetByOrderId(int orderId)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_OrderItem WHERE OrderId = {orderId}";
            DataTable dt = db.Execute(sql);
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (DataRow row in dt.Rows)
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderItemId = int.Parse(row["OrderItemId"].ToString()),
                    OrderId = int.Parse(row["OrderId"].ToString()),
                    MenuItemId = int.Parse(row["MenuItemId"].ToString()),
                    Quantity = int.Parse(row["Quantity"].ToString()),
                    UnitPrice = decimal.Parse(row["UnitPrice"].ToString())
                };
                orderItems.Add(orderItem);
            }
            db.Close();
            return orderItems;
        }
        public static List<OrderItem> GetAll()
        {
            FoodDb db = new FoodDb();
            string sql = "SELECT * FROM T_OrderItem";
            DataTable dt = db.Execute(sql);
            List<OrderItem> orderItems = new List<OrderItem>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OrderItem orderItem = new OrderItem()
                {
                    OrderItemId = int.Parse(dt.Rows[i]["OrderItemId"].ToString()),
                    OrderId = int.Parse(dt.Rows[i]["OrderId"].ToString()),
                    MenuItemId = int.Parse(dt.Rows[i]["MenuItemId"].ToString()),
                    Quantity = int.Parse(dt.Rows[i]["Quantity"].ToString()),
                    UnitPrice = decimal.Parse(dt.Rows[i]["UnitPrice"].ToString())
                };
                orderItems.Add(orderItem);
            }
            db.Close();
            return orderItems;
        }

        public static OrderItem GetById(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_OrderItem WHERE OrderItemId={id}";
            DataTable dt = db.Execute(sql);
            OrderItem orderItem = null;
            if (dt.Rows.Count > 0)
            {
                orderItem = new OrderItem()
                {
                    OrderItemId = int.Parse(dt.Rows[0]["OrderItemId"].ToString()),
                    OrderId = int.Parse(dt.Rows[0]["OrderId"].ToString()),
                    MenuItemId = int.Parse(dt.Rows[0]["MenuItemId"].ToString()),
                    Quantity = int.Parse(dt.Rows[0]["Quantity"].ToString()),
                    UnitPrice = decimal.Parse(dt.Rows[0]["UnitPrice"].ToString())
                };
            }
            db.Close();
            return orderItem;
        }
        private static List<OrderItem> GetCartItemsByUserId(int userId)
        {
            FoodDb db = new FoodDb();
            string sql = @"
                SELECT ci.*, mi.Name, mi.ImageUrl
                FROM T_CartItem ci
                JOIN T_MenuItem mi ON ci.MenuItemId = mi.MenuItemId
                WHERE ci.UserId = @UserId";
            SqlCommand cmd = new SqlCommand(sql, db.Conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<OrderItem> items = new List<OrderItem>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OrderItem item = new OrderItem()
                {
                    MenuItemId = int.Parse(dt.Rows[i]["MenuItemId"].ToString()),
                    Name = dt.Rows[i]["Name"].ToString(), 
                    ImageUrl = dt.Rows[i]["ImageUrl"].ToString(), 
                    Quantity = int.Parse(dt.Rows[i]["Quantity"].ToString()),
                    UnitPrice = decimal.Parse(dt.Rows[i]["UnitPrice"].ToString())
                };
                items.Add(item);
            }
            db.Close();
            return items;
        }


        public static void Save(OrderItem orderItem)
        {
            FoodDb db = new FoodDb();
            string sql = "";
            if (orderItem.OrderItemId == 0)
            {
                sql = $"INSERT INTO T_OrderItem (OrderId, MenuItemId, Quantity, UnitPrice) VALUES ({orderItem.OrderId}, {orderItem.MenuItemId}, {orderItem.Quantity}, {orderItem.UnitPrice})";
            }
            else
            {
                sql = $"UPDATE T_OrderItem SET OrderId={orderItem.OrderId}, MenuItemId={orderItem.MenuItemId}, Quantity={orderItem.Quantity}, UnitPrice={orderItem.UnitPrice} WHERE OrderItemId={orderItem.OrderItemId}";
            }
            db.ExecuteNonQuery(sql);
            db.Close();
        }



        public static void Delete(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"DELETE FROM T_OrderItem WHERE OrderItemId={id}";
            db.ExecuteNonQuery(sql);
            db.Close();
        }
    }
}