using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public string Name { get; set; } // הוסף את המאפיין Name
        public string ImageUrl { get; set; } // הוסף את המאפיין ImageUrl
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public static List<OrderItem> GetAll()
        {
            return OrderItemDAL.GetAll();
        }

        public static OrderItem GetById(int id)
        {
            return OrderItemDAL.GetById(id);
        }

        public void Save()
        {
            OrderItemDAL.Save(this);
        }

        public static void Delete(int id)
        {
            OrderItemDAL.Delete(id);
        }
    }
}
