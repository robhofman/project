using nmct.ba.cashless.helper;
using nmct.ba.cashless.WebApp.helper;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace nmct.ba.cashless.WebApp.DataAccess
{
    public class VerenigingenDA
    {
        private const string CONNECTIONSTRING = "ITDB";
        public static List<Organisation> GetOrganisations()
        {


            List<Organisation> list = new List<Organisation>();

            string sql = "SELECT * from Organisations";

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                list.Add(CreateOrganisation(reader));
            }
            reader.Close();
            return list;
        }

        private static Organisation CreateOrganisation(IDataRecord reader)
        {
            return new Organisation()
            {
                Id = int.Parse(reader["id"].ToString()),
                OrganisationName = reader["OrganisationName"].ToString(),
                Address = reader["Address"].ToString(),
                Email = reader["Email"].ToString(),
                Phone = reader["Phone"].ToString()

            };

        }

        internal static int InsertOrganisation(Organisation org)
        {
            string sql = "INSERT INTO Organisations VALUES(@Login,@Password,@DbName,@DbLogin,@DbPassword,@OrganisationName,@Address,@Email,@Phone)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", org.Login);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", org.Password);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", org.DbName);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", org.Dblogin);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", org.Dbpassword);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", org.OrganisationName);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Address", org.Address);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", org.Email);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Phone", org.Phone);
            int id = Database.InsertData(Database.GetConnection(CONNECTIONSTRING), sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);

            org.Login = Cryptography.Decrypt(org.Login);
            org.Password = Cryptography.Decrypt(org.Password);
            org.DbName = Cryptography.Decrypt(org.DbName);
            org.Dblogin = Cryptography.Decrypt(org.Dblogin);
            org.Dbpassword = Cryptography.Decrypt(org.Dbpassword);


            CreateDatabase(org);

            return id;
        }

        private static void CreateDatabase(Organisation o)
        {
            // create the actual database
            string create = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/create.txt")); //only for the web
            //string create = File.ReadAllText(@"..\..\Data\create.txt"); //only for desktop
            string sql = create.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.Dblogin).Replace("@@DbPassword", o.Dbpassword);
            foreach (string commandText in RemoveGo(sql))
            {
                Database.ModifyData(Database.GetConnection(CONNECTIONSTRING), commandText);
            }

            // create login, user and tables
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction(CONNECTIONSTRING);

                string fill = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/fill.txt")); // only for the web
                //string fill = File.ReadAllText(@"..\..\Data\fill.txt"); // only for desktop
                string sql2 = fill.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.Dblogin).Replace("@@DbPassword", o.Dbpassword);

                foreach (string commandText in RemoveGo(sql2))
                {
                    Database.ModifyData(trans, commandText);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }
        private static string[] RemoveGo(string input)
        {
            //split the script on "GO" commands
            string[] splitter = new string[] { "\r\nGO\r\n" };
            string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return commandTexts;
        }

        internal static Organisation GetOrganisationById(int orgId)
        {
            string sql = "SELECT * from Organisations WHERE Id=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", orgId);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);
            reader.Read();
            Organisation o = new Organisation();
            o.OrganisationName = reader["OrganisationName"].ToString();
            o.Address = reader["Address"].ToString();
            o.Email = (reader["Email"].ToString());
            o.Phone = (reader["Phone"].ToString());
            o.Dblogin = reader["DbLogin"].ToString();
            o.Dbpassword = reader["DbPassword"].ToString();
            o.DbName = reader["DbName"].ToString();
            o.Login = reader["Login"].ToString();
            o.Password = reader["Password"].ToString();

            reader.Close();
            return o;
        }

        public static void UpdateOrganisation(Organisation o)
        {
            string sql = "UPDATE Organisations SET Login=@Login, Password=@Password, DbName=@DbName, DbLogin=@DbLogin, DbPassword=@DbPassword, OrganisationName=@OrganisationName, Address=@Address, Email=@Email, Phone=@Phone  WHERE id=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", o.Login);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", o.Password);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", o.DbName);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", o.Dblogin);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", o.Dbpassword);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", o.OrganisationName);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Address", o.Address);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", o.Email);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Phone", o.Phone);
            DbParameter par10 = Database.AddParameter(CONNECTIONSTRING, "@ID", o.Id);
            Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9, par10);
        }
    }
}