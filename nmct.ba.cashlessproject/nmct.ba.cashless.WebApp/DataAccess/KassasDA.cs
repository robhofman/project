using nmct.ba.cashless.WebApp.helper;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashless.WebApp.DataAccess
{
    public class KassasDA
    {
        private const string CONNECTIONSTRING = "ITDB";
        public static List<RegisterITCompany> GetRegisterOrganisations()
        {
            List<RegisterITCompany> regorg = new List<RegisterITCompany>();


            string sql = "SELECT * FROM Organisations inner join Organisation_Register on Organisation_Register.OrganisationID = OrganisationID inner join Registers on RegisterID = Organisation_Register.RegisterID";

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                regorg.Add(Create(reader));
            }

            reader.Close();
            return regorg;
        }


        private static RegisterITCompany Create(IDataRecord record)
        {
            return new RegisterITCompany()
            {
                Id = Int32.Parse(record["RegisterID"].ToString()),
                Registername = record["RegisterName"].ToString(),
                Device = record["Device"].ToString(),
                Purchasedate = (record["PurchaseDate"].ToString()),
                Expiredate = (record["ExpireDate"].ToString()),
                OrganisationName = record["OrganisationName"].ToString()
            };
        }

        internal static List<RegisterITCompany> GetRegistersByOrganisation(int id)
        {
            List<RegisterITCompany> result = new List<RegisterITCompany>();
            string sql = "SELECT * FROM organisations inner join Organisation_Register on Organisation_Register.OrganisationID = Organisations.id inner join Registers on Registers.ID = Organisation_Register.RegisterID where OrganisationID = @ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            while (reader.Read())
            {
                result.Add(Create(reader));
            }

            reader.Close();


            return result;
        }

        internal static List<RegisterITCompany> GetAvailableRegisters()
        {
            List<RegisterITCompany> registers = new List<RegisterITCompany>();
            string sql = "select * from registers where id not in (SELECT RegisterID FROM Organisation_Register )";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                registers.Add(CreateAvailableRegister(reader));
            }
            reader.Close();

            return registers;
        }


        private static RegisterITCompany CreateAvailableRegister(IDataRecord record)
        {
            RegisterITCompany reg = new RegisterITCompany();
            reg.Id = Int32.Parse(record["ID"].ToString());
            reg.Registername = record["RegisterName"].ToString();
            reg.Device = record["Device"].ToString();
            reg.Purchasedate = (record["PurchaseDate"].ToString());
            reg.Expiredate = (record["ExpireDate"].ToString());
            return reg;
        }

        public static RegisterITCompany GetRegisterById(int id)
        {
            string sql = "SELECT * FROM Registers WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            reader.Read();
            RegisterITCompany register = CreateAvailableRegister(reader);

            reader.Close();
            return register;
        }
        public static int GetOrganisationIdByRegisterId(int RegisterId)
        {
            int id = 0;
            string sql = "SELECT OrganisationID FROM Organisation_Register WHERE RegisterID=@RegisterID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", RegisterId);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);
            while (reader.Read())
            {
                Organisation org = new Organisation();
                org.Id = Int32.Parse(reader["OrganisationID"].ToString());
                id = org.Id;
            }
            reader.Close();
            return id;
        }

        public static void KassaToekennen(RegisterITCompany reg)
        {
            int orgId = KassasDA.GetIDFromOrganisationName(reg.OrganisationName);
            int regid = KassasDA.GetIDFromRegistername(reg.Registername);

            string sql = "INSERT INTO Organisation_Register VALUES(@OrganisationID,@RegisterID,@FromDate,@UntilDate)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationID", orgId);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", reg.Id);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@FromDate", "");
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@UntilDate", "");

            Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4);

        }
        public static int GetIDFromOrganisationName(string oName)
        {
            int id = 0;

            string sql = "SELECT ID FROM Organisations WHERE OrganisationName=@OrganisationName";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", oName);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            reader.Read();
            id = Int32.Parse(reader["ID"].ToString());
            reader.Close();


            return id;
        }

        public static int GetIDFromRegistername(string regName)
        {
            int id = 0;

            string sql = "SELECT ID FROM Registers WHERE RegisterName=@RegisterName";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterName", regName);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            reader.Read();
            id = Int32.Parse(reader["ID"].ToString());
            reader.Close();
            return id;
        }

        public static void DeleteFromOrgRegtable(RegisterITCompany reg)
        {
            string sql = "DELETE FROM Organisation_Register WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("LoginDb", "@ID", reg.Id);

            Database.ModifyData(CONNECTIONSTRING, sql, par1);
        }

        public static void UpdateOrgAndReg(RegisterITCompany reg)
        {


            int organisationID = GetIDFromOrganisationName(reg.OrganisationName);

            string sql = "UPDATE Organisation_Register SET OrganisationID = @OrganisationID WHERE RegisterID=@RegisterID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationID", organisationID);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", reg.Id);

            Database.ModifyData(CONNECTIONSTRING, sql, par1, par2);
        }

        public static void UpdateRegister(RegisterITCompany reg)
        {
            string sql = "UPDATE Registers SET RegisterName = @RegisterName, Device = @Device, PurchaseDate = @PurchaseDate, ExpireDate = @ExpireDate WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterName", reg.Registername);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Device", reg.Device);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@PurchaseDate", "");
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@ExpireDate", "");
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", reg.Id);

            Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5);


        }

    }
}