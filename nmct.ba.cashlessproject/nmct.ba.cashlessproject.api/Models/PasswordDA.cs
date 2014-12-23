using model;
using nmct.ba.cashless.helper;
using nmct.ba.cashlessproject.api.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class PasswordDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            return Database.CreateConnectionString("System.Data.SqlClient", @"ROBMACBOOTCAMP", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }
        public static void ChangePassword(Password np, IEnumerable<Claim> claims)
        {
            try
            {
                string username = Cryptography.Encrypt(np.Username);
                string pass = Cryptography.Encrypt(np.NewPass);

                string sql = "UPDATE Organisations SET Password=@Password WHERE Login=@Login";
                DbParameter par1 = Database.AddParameter("ITDB", "@Password", pass);
                DbParameter par2 = Database.AddParameter("ITDB", "@Login", username);
                Database.ModifyData(Database.GetConnection("ITDB"), sql, par1, par2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}