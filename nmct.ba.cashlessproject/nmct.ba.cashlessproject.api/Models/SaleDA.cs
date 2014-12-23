using model;
using nmct.ba.cashless.helper;
using nmct.ba.cashlessproject.api.Helper;
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
    public class SaleDA
    {
         private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            string test = Cryptography.Decrypt(dbname);
           
            return Database.CreateConnectionString("System.Data.SqlClient", @"ROBMACBOOTCAMP", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }
        public static List<Sale> GetSales(IEnumerable<Claim> claims)
        {
            List<Sale> list = new List<Sale>();

            string sql = "SELECT ID, Timestamp, CustomerID, RegisterID, ProductID, Amount, TotalPrice FROM Sales";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }

            reader.Close();
            return list;
        }

        public static List<Sale> GetSalesByCriteria(int from, int until, int registerID, int productID,IEnumerable<Claim> claims)
        {
            DbDataReader reader = null;
            List<Sale> list = new List<Sale>();

            if ((from > 0) && (until > 0) && (registerID == 0) && (productID == 0))
            {
                string sql = "SELECT ID, Timestamp, CustomerID, RegisterID, ProductID, Amount, TotalPrice FROM Sales WHERE Timestamp BETWEEN @From AND @Until";
                DbParameter par1 = Database.AddParameter("KassaDB", "@From", from);
                DbParameter par2 = Database.AddParameter("KassaDB", "@Until", until);
                reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);
            }

            if ((from == 0) && (until == 0) && (registerID > 0) && (productID == 0))
            {
                string sql = "SELECT ID, Timestamp, CustomerID, RegisterID, ProductID, Amount, TotalPrice FROM Sales WHERE RegisterID=@RegisterID";
                DbParameter par1 = Database.AddParameter("KassaDB", "@RegisterID", registerID);
                reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);
            }

            if ((from == 0) && (until == 0) && (registerID == 0) && (productID > 0))
            {
                string sql = "SELECT ID, Timestamp, CustomerID, RegisterID, ProductID, Amount, TotalPrice FROM Sales WHERE ProductID=@ProductID";
                DbParameter par1 = Database.AddParameter("KassaDB", "@ProductID", productID);
                reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);
            }

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static int InsertSale(Sale s, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Sales VALUES(@ID, @Timestamp, @CustomerID, @RegisterID, @ProductID, @Amount, @TotalPrice)";
            DbParameter par1 = Database.AddParameter("KassaDB", "@ID", s.Id);
            DbParameter par2 = Database.AddParameter("KassaDB", "@Timestamp", s.TimeStamp);
            DbParameter par3 = Database.AddParameter("KassaDB", "@CustomerID", s.CumstomerID);
            DbParameter par4 = Database.AddParameter("KassaDB", "@RegisterID", s.RegisterID);
            DbParameter par5 = Database.AddParameter("KassaDB", "@ProductID", s.ProductID);
            DbParameter par6 = Database.AddParameter("KassaDB", "@Amount", s.Amount);
            DbParameter par7 = Database.AddParameter("KassaDB", "@TotalPrice", s.TotalPrice);

            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5, par6, par7);
        }

        //create sale
        private static Sale Create(IDataRecord record)
        {
            return new Sale()
            {
                Id = Int32.Parse(record["ID"].ToString()),
                TimeStamp = Int32.Parse(record["Timestamp"].ToString()),
                CumstomerID = Int32.Parse(record["CustomerID"].ToString()),
                RegisterID = Int32.Parse(record["RegisterID"].ToString()),
                ProductID = Int32.Parse(record["ProductID"].ToString()),
                Amount = Int32.Parse(record["Amount"].ToString()),
                TotalPrice = Double.Parse(record["TotalPrice"].ToString()),
            };
        }
    }
}