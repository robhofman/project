using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class RegisterITCompany
    {
        private int _id;
        private string registername;
        private string _device;
        private string _organisationName;
        private string _purchasedate;
        private string _expiredate;

        public string OrganisationName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }

        public string Expiredate
        {
            get { return _expiredate; }
            set { _expiredate = value; }
        }

        public string Purchasedate
        {
            get { return _purchasedate; }
            set { _purchasedate = value; }
        }

        public string Device
        {
            get { return _device; }
            set { _device = value; }
        }

        public string Registername
        {
            get { return registername; }
            set { registername = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
