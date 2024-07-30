using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class UserDAL
    {
        public static List<User> GetAll()
        {
            FoodDb db = new FoodDb();
            string sql = "SELECT * FROM T_User";
            DataTable dt = db.Execute(sql);
            List<User> users = new List<User>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User user = new User()
                {
                    UserId = int.Parse(dt.Rows[i]["UserId"].ToString()),
                    FirstName = dt.Rows[i]["FirstName"].ToString(),
                    LastName = dt.Rows[i]["LastName"].ToString(),
                    Username = dt.Rows[i]["Username"].ToString(),
                    Email = dt.Rows[i]["Email"].ToString(),
                    Password = dt.Rows[i]["Password"].ToString(),
                    PhoneNumber = dt.Rows[i]["PhoneNumber"].ToString(),
                    IsAdmin = bool.Parse(dt.Rows[i]["IsAdmin"].ToString()),
                    IsPasswordConfirmed = bool.Parse(dt.Rows[i]["IsPasswordConfirmed"].ToString())
                };
                users.Add(user);
            }
            db.Close();
            return users;
        }

        public static User GetById(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_User WHERE UserId={id}";
            DataTable dt = db.Execute(sql);
            User user = null;
            if (dt.Rows.Count > 0)
            {
                user = new User()
                {
                    UserId = int.Parse(dt.Rows[0]["UserId"].ToString()),
                    FirstName = dt.Rows[0]["FirstName"].ToString(),
                    LastName = dt.Rows[0]["LastName"].ToString(),
                    Username = dt.Rows[0]["Username"].ToString(),
                    Email = dt.Rows[0]["Email"].ToString(),
                    Password = dt.Rows[0]["Password"].ToString(),
                    PhoneNumber = dt.Rows[0]["PhoneNumber"].ToString(),
                    IsAdmin = bool.Parse(dt.Rows[0]["IsAdmin"].ToString()),
                    IsPasswordConfirmed = bool.Parse(dt.Rows[0]["IsPasswordConfirmed"].ToString())
                };
            }
            db.Close();
            return user;
        }

        public static void Save(User user)
        {
            FoodDb db = new FoodDb();
            string sql = "";
            if (user.UserId == 0)
            {
                sql = $"INSERT INTO T_User (FirstName, LastName, Username, Email, Password, PhoneNumber, IsAdmin, IsPasswordConfirmed) VALUES ('{user.FirstName}', '{user.LastName}', '{user.Username}', '{user.Email}', '{user.Password}', '{user.PhoneNumber}', '{user.IsAdmin}', '{user.IsPasswordConfirmed}')";
            }
            else
            {
                sql = $"UPDATE T_User SET FirstName='{user.FirstName}', LastName='{user.LastName}', Email='{user.Email}', PhoneNumber='{user.PhoneNumber}', IsAdmin='{user.IsAdmin}', IsPasswordConfirmed='{user.IsPasswordConfirmed}' WHERE UserId={user.UserId}";
            }
            db.ExecuteNonQuery(sql);
            db.Close();
        }
        public static void Delete(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"DELETE FROM T_User WHERE UserId={id}";
            db.ExecuteNonQuery(sql);
            db.Close();
        }
    }
}