using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class Product
    {
        private int _id;
        private string _productname;
        private int _price;

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Productname
        {
            get { return _productname; }
            set { _productname = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
