using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class AddressDAL
    {
        public static List<Address> GetAll()
        {
            FoodDb db = new FoodDb();
            string sql = "SELECT * FROM T_Address";
            DataTable dt = db.Execute(sql);
            List<Address> addresses = new List<Address>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Address address = new Address()
                {
                    AddressId = int.Parse(dt.Rows[i]["AddressId"].ToString()),
                    UserId = int.Parse(dt.Rows[i]["UserId"].ToString()),
                    Street = dt.Rows[i]["Street"].ToString(),
                    City = dt.Rows[i]["City"].ToString(),
                    State = dt.Rows[i]["State"].ToString(),
                    ZipCode = dt.Rows[i]["ZipCode"].ToString(),
                    Country = dt.Rows[i]["Country"].ToString()
                };
                addresses.Add(address);
            }
            db.Close();
            return addresses;
        }

        public static Address GetById(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_Address WHERE AddressId={id}";
            DataTable dt = db.Execute(sql);
            Address address = null;
            if (dt.Rows.Count > 0)
            {
                address = new Address()
                {
                    AddressId = int.Parse(dt.Rows[0]["AddressId"].ToString()),
                    UserId = int.Parse(dt.Rows[0]["UserId"].ToString()),
                    Street = dt.Rows[0]["Street"].ToString(),
                    City = dt.Rows[0]["City"].ToString(),
                    State = dt.Rows[0]["State"].ToString(),
                    ZipCode = dt.Rows[0]["ZipCode"].ToString(),
                    Country = dt.Rows[0]["Country"].ToString()
                };
            }
            db.Close();
            return address;
        }
        public static Address GetByUserId(int userId)
        {
            FoodDb db = new FoodDb();
            string sql = $"SELECT * FROM T_Address WHERE UserId={userId}";
            DataTable dt = db.Execute(sql);
            Address address = null;
            if (dt.Rows.Count > 0)
            {
                address = new Address()
                {
                    AddressId = int.Parse(dt.Rows[0]["AddressId"].ToString()),
                    UserId = int.Parse(dt.Rows[0]["UserId"].ToString()),
                    Street = dt.Rows[0]["Street"].ToString(),
                    City = dt.Rows[0]["City"].ToString(),
                    State = dt.Rows[0]["State"].ToString(),
                    ZipCode = dt.Rows[0]["ZipCode"].ToString(),
                    Country = dt.Rows[0]["Country"].ToString()
                };
            }
            db.Close();
            return address;
        }

        public static void Save(Address address)
        {
            FoodDb db = new FoodDb();
            string sql = "";
            if (address.AddressId == 0) 
            {
                sql = $"INSERT INTO T_Address (UserId, Street, City, State, ZipCode, Country) VALUES ({address.UserId}, '{address.Street}', '{address.City}', '{address.State}', '{address.ZipCode}', '{address.Country}')";
            }
            else
            {
                sql = $"UPDATE T_Address SET UserId={address.UserId}, Street='{address.Street}', City='{address.City}', State='{address.State}', ZipCode='{address.ZipCode}', Country='{address.Country}' WHERE AddressId={address.AddressId}";
            }
            db.ExecuteNonQuery(sql);
            db.Close();
        }

        public static void Delete(int id)
        {
            FoodDb db = new FoodDb();
            string sql = $"DELETE FROM T_Address WHERE AddressId={id}";
            db.ExecuteNonQuery(sql);
            db.Close();
        }
    }
}
