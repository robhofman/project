using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class RegisterVM: ObservableObject, IPage
    {
   public RegisterVM()
        {
            GetRegisters();
            
        }


        private ObservableCollection<RegisterKassa> _registers;
        public ObservableCollection<RegisterKassa> Registers
        {
            get { return _registers; }
            set { _registers = value; OnPropertyChanged("Registers"); }
        }

        private List<Employee> _werknemers;
        public List<Employee> Werknemers
        {
            get { return _werknemers; }
            set { _werknemers = value;}
        }


        private async void GetRegisters()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:15237/api/register");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Registers = JsonConvert.DeserializeObject<ObservableCollection<RegisterKassa>>(json);
                   
                }
            }
        }

        public string Name
        {
            get { return "Register page"; }
        }


        private RegisterKassa _selected;
        public RegisterKassa SelectedRegister
        {
            get { return _selected; }
            set { _selected = value; OnPropertyChanged("SelectedRegister");}
        }


        private Employee _selectedEmp;

        public Employee SelectedEmployee
        {
            get { return _selectedEmp; }
            set { _selectedEmp = value; OnPropertyChanged("SelectedEmployee"); }
        }
        

    }
}
