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
    public class RegisterDA
    {

        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            string test = Cryptography.Decrypt(dbname);

            return Database.CreateConnectionString("System.Data.SqlClient", @"ROBMACBOOTCAMP", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<RegisterKassa> GetRegisters(IEnumerable<Claim> claims)
        {


            List<RegisterKassa> list = new List<RegisterKassa>();
            string sql = "SELECT * FROM Registers";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                RegisterKassa c = new RegisterKassa();
                c.Id = Convert.ToInt32(reader["ID"]);
                c.Registername = reader["RegisterName"].ToString();
                c.Device = (reader["Device"].ToString());

                string sql2 = "SELECT * from Employee join Register_Employee on Employee.ID = Register_Employee.EmployeeID where Register_Employee.RegisterID = " + c.Id;
                DbDataReader reader2 = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql2);
                List<Employee> emplist = new List<Employee>();
                while (reader2.Read())
                {
                    Employee e = new Employee();
                    e.Employeename = reader2["EmployeeName"].ToString();

                    emplist.Add(e);

                }
                c.Medewerkers = emplist;
                list.Add(c);
            }

            return list;
        }
    }
}