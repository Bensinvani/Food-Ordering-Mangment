using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class ContactDAL
    {
        public static List<Contact> GetAll()
        {
            FoodDb db = new FoodDb();
            string sql = "SELECT * FROM T_Contact";
            DataTable dt = db.Execute(sql);
            List<Contact> contacts = new List<Contact>();

            foreach (DataRow row in dt.Rows)
            {
                Contact contact = new Contact
                {
                    ContactId = int.Parse(row["ContactId"].ToString()),
                    UserId = row["UserId"] == DBNull.Value ? (int?)null : int.Parse(row["UserId"].ToString()), // Handle null values
                    Name = row["Name"].ToString(),
                    Email = row["Email"].ToString(),
                    Message = row["Message"].ToString(),
                    Date = DateTime.Parse(row["Date"].ToString()),
                    IsReplied = bool.Parse(row["IsReplied"].ToString())
                };
                contacts.Add(contact);
            }
            db.Close();
            return contacts;
        }

        public static Contact GetById(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_Contact WHERE ContactId={id}";
            DataTable dt = db.Execute(sql);
            Contact contact = null;

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                contact = new Contact
                {
                    ContactId = int.Parse(row["ContactId"].ToString()),
                    UserId = row["UserId"] == DBNull.Value ? (int?)null : int.Parse(row["UserId"].ToString()), 
                    Name = row["Name"].ToString(),
                    Email = row["Email"].ToString(),
                    Message = row["Message"].ToString(),
                    Date = DateTime.Parse(row["Date"].ToString()),
                    IsReplied = bool.Parse(row["IsReplied"].ToString())
                };
            }
            db.Close();
            return contact;
        }

        public static void Save(Contact contact)
        {
            FoodDb db = new FoodDb();
            string sql;
            int isRepliedValue = contact.IsReplied ? 1 : 0;
            string userIdValue = contact.UserId.HasValue ? contact.UserId.Value.ToString() : "NULL";

            string formattedDate = contact.Date.ToString("yyyy-MM-dd HH:mm:ss");

            if (contact.ContactId == 0)
            {
                sql = $"INSERT INTO T_Contact (UserId, Name, Email, Message, Date, IsReplied) VALUES ({userIdValue}, N'{contact.Name}', N'{contact.Email}', N'{contact.Message}', '{formattedDate}', {isRepliedValue})";
            }
            else
            {
                sql = $"UPDATE T_Contact SET UserId={userIdValue}, Name=N'{contact.Name}', Email=N'{contact.Email}', Message=N'{contact.Message}', Date='{formattedDate}', IsReplied={isRepliedValue} WHERE ContactId={contact.ContactId}";
            }
            db.ExecuteNonQuery(sql);
            db.Close();
        }


        public static void Delete(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"DELETE FROM T_Contact WHERE ContactId={id}";
            db.ExecuteNonQuery(sql);
            db.Close();
        }
    }
}
