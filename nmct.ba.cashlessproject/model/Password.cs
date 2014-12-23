using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Password
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _newPass;

        public string NewPass
        {
            get { return _newPass; }
            set { _newPass = value; }
        }

        private string _repeatNewPass;

        public string RepeatNewPass
        {
            get { return _repeatNewPass; }
            set { _repeatNewPass = value; }
        }
    }
}
