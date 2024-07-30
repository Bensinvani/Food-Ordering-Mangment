using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class Cart
    {
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; }

        public Cart()
        {
            Items = new List<OrderItem>();
        }

        public void AddItem(MenuItem menuItem, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.MenuItemId == menuItem.MenuItemId);
            if (item == null)
            {
                Items.Add(new OrderItem
                {
                    MenuItemId = menuItem.MenuItemId,
                    Quantity = quantity,
                    UnitPrice = menuItem.Price
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(int menuItemId)
        {
            var item = Items.FirstOrDefault(i => i.MenuItemId == menuItemId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public decimal TotalAmount
        {
            get
            {
                return Items.Sum(i => i.TotalPrice);
            }
        }
        public static List<Cart> GetAll()
        {
            return CartDAL.GetAll();
        }

        public static Cart GetById(int id)
        {
            return CartDAL.GetById(id);
        }

        public void Save()
        {
            CartDAL.Save(this);
        }

        public static void Delete(int id)
        {
            CartDAL.Delete(id);
        }
    }
}
