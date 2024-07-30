using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPasswordConfirmed { get; set; }
        public static List<User> GetAll()
        {
            return UserDAL.GetAll();
        }

        public static User GetById(int id)
        {
            return UserDAL.GetById(id);
        }

        public void Save()
        {
            UserDAL.Save(this);
        }

        public static void Delete(int id)
        {
            UserDAL.Delete(id);
        }
    }
}