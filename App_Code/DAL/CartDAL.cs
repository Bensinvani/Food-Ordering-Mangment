using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class CartDAL
    {
        public static List<Cart> GetAll()
        {
            FoodDb db = new FoodDb();
            string sql = "SELECT * FROM T_Cart";
            DataTable dt = db.Execute(sql);
            List<Cart> carts = new List<Cart>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Cart cart = new Cart()
                {
                    UserId = int.Parse(dt.Rows[i]["UserId"].ToString()),
                    Items = GetCartItemsByUserId(int.Parse(dt.Rows[i]["UserId"].ToString()))
                };
                carts.Add(cart);
            }
            db.Close();
            return carts;
        }

        public static Cart GetById(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_Cart WHERE UserId={id}";
            DataTable dt = db.Execute(sql);
            Cart cart = null;
            if (dt.Rows.Count > 0)
            {
                cart = new Cart()
                {
                    UserId = int.Parse(dt.Rows[0]["UserId"].ToString()),
                    Items = GetCartItemsByUserId(int.Parse(dt.Rows[0]["UserId"].ToString()))
                };
            }
            db.Close();
            return cart;
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

        public static void Save(Cart cart)
        {
            FoodDb db = new FoodDb();

            // Check if the UserId exists in the T_User table
            string checkUserSql = $"SELECT COUNT(*) FROM T_User WHERE UserId={cart.UserId}";
            int userCount = (int)db.ExecuteScalar(checkUserSql);

            if (userCount == 0)
            {
                throw new Exception("User does not exist.");
            }

            // Check if the UserId exists in the T_Cart table
            string checkCartSql = $"SELECT COUNT(*) FROM T_Cart WHERE UserId={cart.UserId}";
            int cartCount = (int)db.ExecuteScalar(checkCartSql);

            if (cartCount == 0)
            {
                // Insert the UserId into the T_Cart table
                string insertCartSql = $"INSERT INTO T_Cart (UserId) VALUES ({cart.UserId})";
                db.ExecuteNonQuery(insertCartSql);
            }

            // Clear existing cart items
            string deleteItemsSql = $"DELETE FROM T_CartItem WHERE UserId={cart.UserId}";
            db.ExecuteNonQuery(deleteItemsSql);

            // Insert new cart items
            foreach (var item in cart.Items)
            {
                string insertItemSql = $"INSERT INTO T_CartItem (UserId, MenuItemId, Quantity, UnitPrice) VALUES ({cart.UserId}, {item.MenuItemId}, {item.Quantity}, {item.UnitPrice})";
                db.ExecuteNonQuery(insertItemSql);
            }
            db.Close();
        }

        public static void Delete(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"DELETE FROM T_Cart WHERE UserId={id}";
            db.ExecuteNonQuery(sql);
            db.Close();
        }
    }
}
