using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class CustomerDA
    {
        public static List<Customer> GetCustomers()
        {
            List<Customer> list = new List<Customer>();
            string sql = "SELECT * FROM Customer";
            DbConnection con = Database.GetConnection("KassaConnection");

            DbDataReader reader = Database.GetData(con, sql);
            while(reader.Read())
            {
                list.Add(new Customer()
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