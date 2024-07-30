using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class Contact
    {
        public int ContactId { get; set; }
        public int? UserId { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsReplied { get; set; }

        public static List<Contact> GetAll()
        {
            return ContactDAL.GetAll();
        }

        public static Contact GetById(int id)
        {
            return ContactDAL.GetById(id);
        }

        public void Save()
        {
            ContactDAL.Save(this);
        }

        public static void Delete(int id)
        {
            ContactDAL.Delete(id);
        }
    }
}
