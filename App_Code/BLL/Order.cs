using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public static List<Order> GetByUserId(int userId)
        {
            return OrderDAL.GetByUserId(userId);
        }

        public static List<Order> GetAll()
        {
            return OrderDAL.GetAll();
        }

        public static Order GetById(int id)
        {
            return OrderDAL.GetById(id);
        }

        public void Save()
        {
            OrderDAL.Save(this);
        }

        public static void Delete(int id)
        {
            OrderDAL.Delete(id);
        }
    }
}
