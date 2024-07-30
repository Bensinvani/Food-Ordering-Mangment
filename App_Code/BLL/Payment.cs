using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public static List<Payment> GetAll()
        {
            return PaymentDAL.GetAll();
        }

        public static Payment GetById(int id)
        {
            return PaymentDAL.GetById(id);
        }

        public void Save()
        {
            PaymentDAL.Save(this);
        }

        public static void Delete(int id)
        {
            PaymentDAL.Delete(id);
        }
    }
}