using nmct.ba.cashless.helper;
using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class CustomerDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            string test = Cryptography.Decrypt(dbname);
            string t = test;
            string u = Cryptography.Decrypt(dblogin);
            string us = u;
            string p = Cryptography.Decrypt(dbpass);
            string pw = p;            
            return Database.CreateConnectionString("System.Data.SqlClient", @"ROBMACBOOTCAMP", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        
        public static List<Customer> GetCustomers(IEnumerable<Claim> claims)
        {
           
            List<Customer> list = new List<Customer>();
            string sql = "SELECT ID, CustomerName, Address, Picture, Balance FROM Customers";

            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while(reader.Read())
            {
                list.Add(new Customer()
                    {
                        Id = Convert.ToInt64(reader["ID"]),
                        Customername = reader["CustomerName"].ToString(),
                        Address = reader["Address"].ToString(),
                        Image = GetBytes(reader["Picture"].ToString()),
                        Balance = Double.Parse(reader["Balance"].ToString())
                    });
                Console.WriteLine(reader["CustomerName"].ToString());
            }
            return list;
        }

        public static int InsertCustomer(Customer c, IEnumerable<Claim> claims)
        {
            
                string sql = "INSERT INTO Customers VALUES(@ID, @CustomerName, @Address, @Picture, @Balance)";
                DbParameter par1 = Database.AddParameter("ITDB", "@ID", c.Id);
                DbParameter par2 = Database.AddParameter("ITDB", "@CustomerName", c.Customername);
                DbParameter par3 = Database.AddParameter("ITDB", "@Address", c.Address);
                DbParameter par4 = Database.AddParameter("ITDB", "@Picture", c.Image);
                DbParameter par5 = Database.AddParameter("ITDB", "@Balance", c.Balance);

                return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
   
        }

        public static void UpdateCustomer(Customer c, IEnumerable<Claim> claims)
        {
            try
            {
                string sql = "UPDATE Customers SET Balance=@Balance WHERE ID=@ID";
                DbParameter par1 = Database.AddParameter("ITDB", "@ID", c.Id);
                DbParameter par2 = Database.AddParameter("ITDB", "@Balance", c.Balance);

                Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static Customer Create(IDataRecord record)
        {
            return new Customer()
            {
                Id = Int32.Parse(record["ID"].ToString()),
                Customername = record["CustomerName"].ToString(),
                Address = record["Address"].ToString(),
                Image = GetBytes(record["Picture"].ToString()),
                Balance = Double.Parse(record["Balance"].ToString())
            };
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static Customer GetCustomerByID(string rijks, IEnumerable<Claim> claims)
        {
            Customer customer = new Customer();
            Int64 ID = Convert.ToInt64(rijks);
            string sql = "SELECT * FROM Customer WHERE Id=@ID";
            DbParameter par1 = Database.AddParameter("ITDB", "@NationalNumber", ID);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);

            while (reader.Read())
            {
                customer = Create(reader);
            }
            reader.Close();

            return customer;
        }
    }
}