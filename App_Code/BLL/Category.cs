using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string CategoryImage { get; set; }

        public static List<Category> GetAll()
        {
            return CategoryDAL.GetAll();
        }

        public static Category GetById(int id)
        {
            return CategoryDAL.GetById(id);
        }

        public void Save()
        {
            CategoryDAL.Save(this);
        }

        public static void Delete(int id)
        {
            CategoryDAL.Delete(id);
        }
    }
}
