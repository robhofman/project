using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Sale
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //unix timestamp
        private int _timeStamp;
        public int TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        private int _customerID;
        public int CumstomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        private int _registerID;
        public int RegisterID
        {
            get { return _registerID; }
            set { _registerID = value; }
        }

        private int _productID;
        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }

        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private double _totalPrice;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        
    }
}
