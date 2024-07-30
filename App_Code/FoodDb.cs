using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace DATA
{
    public class FoodDb
    {
        public string Connstr { get; set; } // שדה עבור מחרוזת התחברות לבסיס הנתונים

        public SqlConnection Conn { get; set; } // שדה עבור אובייקט הצינור לבסיס הנתונים

        public FoodDb()
        {
            Connstr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString; // שליפת מחרוזת ההתחברות מתוך קובץ הגדרות האפליקציה / שרת web.config

            Conn = new SqlConnection(Connstr);

            Conn.Open(); // פתיחת הצינור
        }
        public void Close()
        {
            Conn.Close(); // סגירת הצינור
        }
        public int ExecuteNonQuery(string Sql) // פונקציה לשאילתות לא מתשואלות
        {
            SqlCommand Cmd = new SqlCommand(Sql, Conn); // יצירת אובייקט תותח פקודות

            return Cmd.ExecuteNonQuery(); // מחזיר טיפוס נתונים int
        }
        public DataTable Execute(string Sql) // הפונקציה משמשת לשליפת נתונים מבסיס הנתונים
        {
            SqlCommand Cmd = new SqlCommand(Sql, Conn); // Sql מקבל משפט Sql

            SqlDataAdapter Da = new SqlDataAdapter(Cmd); // יצירת אובייקט מסוג מתאם נתונים

            DataTable Dt = new DataTable(); // יצירת אובייקט מסוג טבלת נתונים

            Da.Fill(Dt); // מילוי טבלת הנתונים בתוצאות הפעלת השאילתה

            return Dt; // החזרת טבלת הנתונים
        }
        public object ExecuteScalar(string sql) // הוספת פונקציה להחזרת ערך בודד מתוך השאילתה
        {
            SqlCommand cmd = new SqlCommand(sql, Conn);
            return cmd.ExecuteScalar();
        }
    }
}