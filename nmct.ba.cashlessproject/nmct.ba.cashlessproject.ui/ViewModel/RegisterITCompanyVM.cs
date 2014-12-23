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
    class RegisterITCompanyVM : ObservableObject, IPage
    {
          public RegisterITCompanyVM()
            {
                if (ApplicationVM.token != null)
                {
                    GetRegistersITCompany();
                }
            }

        private ObservableCollection<RegisterITCompany> _registersITCompany;

        public ObservableCollection<RegisterITCompany> RegistersITCompany
            {
                get { return _registersITCompany; }
                set { _registersITCompany = value; OnPropertyChanged("RegisterITCompany"); }
            }

            private async void GetRegistersITCompany()
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.GetAsync("http://localhost:15237/api/employee");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        RegistersITCompany = JsonConvert.DeserializeObject<ObservableCollection<RegisterITCompany>>(json);
                    }
                }
            }
            public string Name
            {
                get { return "RegistersITCompany page"; }
            }
        }
    }