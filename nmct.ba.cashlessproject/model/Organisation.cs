using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nmct.ba.cashlessproject.model
{
    public class Organisation
    {
        private int _id;
        private string _login;
        private string _password;
        private string _dbName;
        private string _organisationName;
        private string _address;
        private string _email;
        private string _phone;
        private string _dbpassword;
        private string _dblogin;

        public string Dblogin
        {
            get { return _dblogin; }
            set { _dblogin = value; }
        }

        public string Dbpassword
        {
            get { return _dbpassword; }
            set { _dbpassword = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string OrganisationName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }

        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
