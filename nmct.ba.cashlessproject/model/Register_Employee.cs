using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Register_Employee
    {
 
        private int _registerID;
        public int RegisterID
        {
            get { return _registerID; }
            set { _registerID = value; }
        }

        private string _registerName;
        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }


        private int _employeeID;
        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }

        private string _employeeName;
        public string EmployeeName
        {
            get { return _employeeName; }
            set { _employeeName = value; }
        }

        private DateTime _van;
        public DateTime Van
        {
            get { return _van; }
            set { _van = value; }
        }

        private DateTime _tot;
        public DateTime Tot
        {
            get { return _tot; }
            set { _tot = value; }
        }
    }
}
