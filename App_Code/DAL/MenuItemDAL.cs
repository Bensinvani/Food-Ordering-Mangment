using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL
{
    public class MenuItemDAL
    {
        public static List<MenuItem> GetAll()
        {
            FoodDb db = new FoodDb();
            string sql = @"SELECT m.*, c.CategoryName, c.CategoryImage 
                           FROM T_MenuItem m 
                           LEFT JOIN T_Category c ON m.CategoryId = c.CategoryId";
            DataTable dt = db.Execute(sql);
            List<MenuItem> menuItems = new List<MenuItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MenuItem menuItem = new MenuItem()
                {
                    MenuItemId = int.Parse(dt.Rows[i]["MenuItemId"].ToString()),
                    Name = dt.Rows[i]["Name"].ToString(),
                    Description = dt.Rows[i]["Description"].ToString(),
                    Price = decimal.Parse(dt.Rows[i]["Price"].ToString()),
                    CategoryId = int.Parse(dt.Rows[i]["CategoryId"].ToString()),
                    ImageUrl = dt.Rows[i]["ImageUrl"].ToString(),
                    CategoryName = dt.Rows[i]["CategoryName"].ToString(),
                    CategoryImage = dt.Rows[i]["CategoryImage"].ToString()
                };
                menuItems.Add(menuItem);
            }
            db.Close();
            return menuItems;
        }

        public static MenuItem GetById(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $@"SELECT m.*, c.CategoryName, c.CategoryImage 
                            FROM T_MenuItem m 
                            LEFT JOIN T_Category c ON m.CategoryId = c.CategoryId 
                            WHERE m.MenuItemId={id}";
            DataTable dt = db.Execute(sql);
            MenuItem menuItem = null;
            if (dt.Rows.Count > 0)
            {
                menuItem = new MenuItem()
                {
                    MenuItemId = int.Parse(dt.Rows[0]["MenuItemId"].ToString()),
                    Name = dt.Rows[0]["Name"].ToString(),
                    Description = dt.Rows[0]["Description"].ToString(),
                    Price = decimal.Parse(dt.Rows[0]["Price"].ToString()),
                    CategoryId = int.Parse(dt.Rows[0]["CategoryId"].ToString()),
                    ImageUrl = dt.Rows[0]["ImageUrl"].ToString(),
                    CategoryName = dt.Rows[0]["CategoryName"].ToString(),
                    CategoryImage = dt.Rows[0]["CategoryImage"].ToString()
                };
            }
            db.Close();
            return menuItem;
        }

        public static void Save(MenuItem menuItem)
        {
            FoodDb db = new FoodDb();
            string sql = "";
            if (menuItem.MenuItemId == 0)
            {
                sql = $"INSERT INTO T_MenuItem (Name, Description, Price, CategoryId, ImageUrl) VALUES ('{menuItem.Name}', '{menuItem.Description}', {menuItem.Price}, {menuItem.CategoryId}, '{menuItem.ImageUrl}')";
            }
            else
            {
                sql = $"UPDATE T_MenuItem SET Name='{menuItem.Name}', Description='{menuItem.Description}', Price={menuItem.Price}, CategoryId={menuItem.CategoryId}, ImageUrl='{menuItem.ImageUrl}' WHERE MenuItemId={menuItem.MenuItemId}";
            }
            db.ExecuteNonQuery(sql);
            db.Close();
        }

        public static void Delete(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"DELETE FROM T_MenuItem WHERE MenuItemId={id}";
            db.ExecuteNonQuery(sql);
            db.Close();
        }
    }
}
