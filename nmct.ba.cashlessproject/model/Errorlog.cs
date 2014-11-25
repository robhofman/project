using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    class Errorlog
    {
        private int _registerid;
        private DateTime _timestamp;
        private string _message;
        private string _stacktrace;

        public string Stacktrace
        {
            get { return _stacktrace; }
            set { _stacktrace = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public int Registerid
        {
            get { return _registerid; }
            set { _registerid = value; }
        }
    }
}
