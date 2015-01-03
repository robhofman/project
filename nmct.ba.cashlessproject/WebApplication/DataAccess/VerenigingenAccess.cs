using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using WebApplication.helper;

namespace WebApplication.DataAccess
{
    public class VerenigingenAccess
    {
        private const string CONNECTIONSTRING = "ITDB";
        public static List<Organisation> GetOrganisations()
        {


            List<Organisation> list = new List<Organisation>();

            string sql = "SELECT * from Organisation";

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

    }
}