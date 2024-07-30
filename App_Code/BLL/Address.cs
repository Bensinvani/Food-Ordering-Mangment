using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public static List<Address> GetAll()
        {
            return AddressDAL.GetAll();
        }

        public static Address GetById(int id)
        {
            return AddressDAL.GetById(id);
        }
        public static Address GetByUserId(int userId)
        {
            return AddressDAL.GetByUserId(userId);
        }

        public void Save()
        {
            AddressDAL.Save(this);
        }

        public static void Delete(int id)
        {
            AddressDAL.Delete(id);
        }
    }
}
