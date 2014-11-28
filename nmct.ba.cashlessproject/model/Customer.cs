using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace nmct.ba.cashlessproject.model
{
    public class RegisterKassa
    {
        private int _id;
        private string _customername;
        private string _address;
        private Image _image;
        private int _balance;

        public int Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string Customername
        {
            get { return _customername; }
            set { _customername = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
