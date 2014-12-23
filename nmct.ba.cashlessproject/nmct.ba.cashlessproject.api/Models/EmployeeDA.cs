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
    public class EmployeeDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            string test = Cryptography.Decrypt(dbname);

            return Database.CreateConnectionString("System.Data.SqlClient", @"ROBMACBOOTCAMP", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Employee> GetEmployees(IEnumerable<Claim> claims)
        {
           
            List<Employee> list = new List<Employee>();
            string sql = "SELECT ID, EmployeeName, Address, Email, Phone FROM Employee";
          

            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while(reader.Read())
            {
                list.Add(Create(reader));
                    
                Console.WriteLine(reader["EmployeeName"].ToString());
            }
            return list;
        }

        public static Employee Create(IDataRecord record)
          {
              return new Employee()
              {
                  Id = Int32.Parse(record["ID"].ToString()),
                  Employeename = record["EmployeeName"].ToString(),
                  Address = record["Address"].ToString(),
                  Email = record["Email"].ToString(),
                  Phone = record["Phone"].ToString()
              };
            
          }

        public static Employee GetEmployee(int id, IEnumerable<Claim> claims)
        {
            string sql = "SELECT ID, EmployeeName, Address, Email, Phone FROM Employee WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ITDB", "@ID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            Employee result = null;
            while (reader.Read())
            {
                result = Create(reader);
            }
            reader.Close();
            return result;
        }

        public static int InsertEmployee(Employee e, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Employee VALUES(@ID, @EmployeeName, @Address, @Email, @Phone)";
            DbParameter par1 = Database.AddParameter("ITDB", "@ID", e.Id);
            DbParameter par2 = Database.AddParameter("ITDB", "@EmployeeName", e.Employeename);
            DbParameter par3 = Database.AddParameter("ITDB", "@Address", e.Address);
            DbParameter par4 = Database.AddParameter("ITDB", "@Email", e.Email);
            DbParameter par5 = Database.AddParameter("ITDB", "@Phone", e.Phone);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
        }

        public static void UpdateEmployee(Employee e, IEnumerable<Claim> claims)
        {
            try
            {
                string sql = "UPDATE Employee SET EmployeeName=@EmployeeName, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";
                DbParameter par1 = Database.AddParameter("ITDB", "@EmployeeName", e.Employeename);
                DbParameter par2 = Database.AddParameter("ITDB", "@Address", e.Address);
                DbParameter par3 = Database.AddParameter("ITDB", "@Email", e.Email);
                DbParameter par4 = Database.AddParameter("ITDB", "@Phone", e.Phone);
                DbParameter par5 = Database.AddParameter("ITDB", "@ID", e.Id);
                Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void DeleteEmployee(int id, IEnumerable<Claim> claims)
        {
            try
            {
                string sql = "DELETE FROM Employee WHERE ID=@ID";
                DbParameter par1 = Database.AddParameter("ITDB", "@ID", id);
                Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
