using nmct.ba.cashless.helper;
using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class ProductDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            string test = Cryptography.Decrypt(dbname);

            return Database.CreateConnectionString("System.Data.SqlClient", @"ROBMACBOOTCAMP", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Product> GetProducts(IEnumerable<Claim> claims)
        {
            List<Product> list = new List<Product>();

            string sql = "SELECT ID, ProductName, Price FROM Products";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }

            reader.Close();
            return list;
        }

        public static Product GetProduct(int id, IEnumerable<Claim> claims)
        {
            string sql = "SELECT ID, ProductName, Price FROM Products WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("KassaDB", "@ID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);
            Product result = null;

            while (reader.Read())
            {
                result = Create(reader);
            }
            reader.Close();
            return result;
        }

        public static int InsertProduct(Product p, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Products VALUES(@ProductName, @Price)";
            DbParameter par1 = Database.AddParameter("KassaDB", "@ProductName", p.Productname);
            DbParameter par2 = Database.AddParameter("KassaDB", "@Price", p.Price);

            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);
        }

        public static void EditProduct(Product p, IEnumerable<Claim> claims)
        {
            try
            {
                string sql = "UPDATE Products SET ProductName=@ProductName, Price=@Price WHERE ID=@ID";
                DbParameter par1 = Database.AddParameter("KassaDB", "@ProductName", p.Productname);
                DbParameter par2 = Database.AddParameter("KassaDB", "@Price", p.Price);
                DbParameter par3 = Database.AddParameter("KassaDB", "@ID", p.Id);
                Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public static void DeleteProduct(int id, IEnumerable<Claim> claims)
        {
            try
            {
                string sql = "DELETE FROM Products WHERE ID=@ID";
                DbParameter par1 = Database.AddParameter("KassaDB", "@ID", id);
                Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static Product Create(IDataRecord record)
        {
            return new Product()
            {
                Id = Int32.Parse(record["ID"].ToString()),
                Productname = record["ProductName"].ToString(),
                Price = Double.Parse(record["Price"].ToString())
            };
        }
    }
}