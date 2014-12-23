using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class RegisterKassa
    {
        private int _id;
        private string _registername;
        private string _device;

        public string Device
        {
            get { return _device; }
            set { _device = value; }
        }

        public string Registername
        {
            get { return _registername; }
            set { _registername = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private List<Employee> _medewerkers;

        public List<Employee> Medewerkers
        {
            get { return _medewerkers; }
            set { _medewerkers = value; }
        }
    }
}
