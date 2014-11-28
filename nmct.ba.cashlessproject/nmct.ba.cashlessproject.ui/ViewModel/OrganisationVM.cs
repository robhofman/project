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
    class OrganisationVM : ObservableObject, IPage
 {
        public OrganisationVM()
            {
                if (ApplicationVM.token != null)
                {
                    GetOrganisations();
                }
            }

        private ObservableCollection<Errorlog> _organisations;

        public ObservableCollection<Errorlog> Organisations
            {
                get { return _organisations; }
                set { _organisations = value; OnPropertyChanged("Organisations"); }
            }

            private async void GetOrganisations()
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.GetAsync("http://localhost:15237/api/employee");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        Organisations = JsonConvert.DeserializeObject<ObservableCollection<Errorlog>>(json);
                    }
                }
            }
            public string Name
            {
                get { return "Organisation page"; }
            }
        }
    }