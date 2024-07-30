using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class CategoryDAL
    {
        public static List<Category> GetAll()
        {
            FoodDb db = new FoodDb();
            string sql = "SELECT * FROM T_Category";
            DataTable dt = db.Execute(sql);
            List<Category> categories = new List<Category>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Category category = new Category()
                {
                    CategoryId = int.Parse(dt.Rows[i]["CategoryId"].ToString()),
                    CategoryName = dt.Rows[i]["CategoryName"].ToString(),
                    Description = dt.Rows[i]["Description"].ToString(),
                    CategoryImage = dt.Rows[i]["CategoryImage"].ToString()
                };
                categories.Add(category);
            }
            db.Close();
            return categories;
        }

        public static Category GetById(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_Category WHERE CategoryId={id}";
            DataTable dt = db.Execute(sql);
            Category category = null;
            if (dt.Rows.Count > 0)
            {
                category = new Category()
                {
                    CategoryId = int.Parse(dt.Rows[0]["CategoryId"].ToString()),
                    CategoryName = dt.Rows[0]["CategoryName"].ToString(),
                    Description = dt.Rows[0]["Description"].ToString(),
                    CategoryImage = dt.Rows[0]["CategoryImage"].ToString()
                };
            }
            db.Close();
            return category;
        }

        public static void Save(Category category)
        {
            FoodDb db = new FoodDb();
            string sql = "";
            if (category.CategoryId == 0)
            {
                sql = $"INSERT INTO T_Category (CategoryName, Description, CategoryImage) VALUES ('{category.CategoryName}', '{category.Description}', '{category.CategoryImage}')";
            }
            else
            {
                sql = $"UPDATE T_Category SET CategoryName='{category.CategoryName}', Description='{category.Description}', CategoryImage='{category.CategoryImage}' WHERE CategoryId={category.CategoryId}";
            }
            db.ExecuteNonQuery(sql);
            db.Close();
        }

        public static void Delete(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"DELETE FROM T_Category WHERE CategoryId={id}";
            db.ExecuteNonQuery(sql);
            db.Close();
        }
    }
}
