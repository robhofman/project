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
        private DateTime _purchasedate;
        private DateTime _expiredate;

        public DateTime Expiredate
        {
            get { return _expiredate; }
            set { _expiredate = value; }
        }

        public DateTime Purchasedate
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
