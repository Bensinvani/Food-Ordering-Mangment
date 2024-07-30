using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage {  get; set; }
        public string ImageUrl { get; set; }
        public static List<MenuItem> GetAll()
        {
            return MenuItemDAL.GetAll();
        }

        public static MenuItem GetById(int id)
        {
            return MenuItemDAL.GetById(id);
        }

        public void Save()
        {
            MenuItemDAL.Save(this);
        }

        public static void Delete(int id)
        {
            MenuItemDAL.Delete(id);
        }
    }
}