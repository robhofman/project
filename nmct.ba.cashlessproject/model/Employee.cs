using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class Employee
    {
        private int _id;
        private string _employeename;
        private string _address;
        private string _email;
        private string _phone;

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

        public string Employeename
        {
            get { return _employeename; }
            set { _employeename = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
