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
    class EmployeeVM : ObservableObject, IPage
    {
        public EmployeeVM()
            {
                if (ApplicationVM.token != null)
                {
                    GetEmployees();
                }
            }

        private ObservableCollection<Errorlog> _employees;

        public ObservableCollection<Errorlog> Employees
            {
                get { return _employees; }
                set { _employees = value; OnPropertyChanged("Employees"); }
            }

            private async void GetEmployees()
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.GetAsync("http://localhost:15237/api/employee");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        Employees = JsonConvert.DeserializeObject<ObservableCollection<Errorlog>>(json);
                    }
                }
            }
            public string Name
            {
                get { return "Employee page"; }
            }
        }
    }
