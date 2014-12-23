﻿using nmct.ba.cashless.helper;
using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class OrganisationDA
    {
        public static Organisation CheckCredentials(string username, string password)
        {
            string sql = "SELECT * FROM Organisations WHERE Login=@Login AND Password=@Password";
            DbParameter par1 = Database.AddParameter("ITDB", "@Login", Cryptography.Encrypt(username));
            DbParameter par2 = Database.AddParameter("ITDB", "@Password", Cryptography.Encrypt(password));
            string u = Cryptography.Encrypt(username);
            string us = u;
            string p = Cryptography.Encrypt(password);
            string pw = p;


            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("ITDB"), sql, par1, par2);
                reader.Read();
                return new Organisation()
                {
                    Id = Int32.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Password = reader["Password"].ToString(),
                    DbName = reader["DbName"].ToString(),
                    Dblogin = reader["DbLogin"].ToString(),
                    Dbpassword = reader["DbPassword"].ToString(),
                    OrganisationName = reader["OrganisationName"].ToString(),
                    Address = reader["Address"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    }
}