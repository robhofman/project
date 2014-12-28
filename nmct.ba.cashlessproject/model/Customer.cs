using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace nmct.ba.cashlessproject.model
{
    public class Customer
    {
        private long _id;
        private string _customername;
        private string _address;
        private byte[] _image;
        private double _balance;

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Customername
        {
            get { return _customername; }
            set { _customername = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public byte[] Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }





    }
}
