using nmct.ba.cashless.helper;
using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class CustomerDA
    {
        public static List<RegisterKassa> GetCustomers()
        {
            //string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            //string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            //string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            List<RegisterKassa> list = new List<RegisterKassa>();
            string sql = "SELECT * FROM Customers";
            DbConnection con = Database.GetConnection("KassaDB");

            DbDataReader reader = Database.GetData(con, sql);
            while(reader.Read())
            {
                list.Add(new RegisterKassa()
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Customername = reader["CustomerName"].ToString()
                        
                    });
                Console.WriteLine(reader["CustomerName"].ToString());
            }
            return list;
        }
    }
}