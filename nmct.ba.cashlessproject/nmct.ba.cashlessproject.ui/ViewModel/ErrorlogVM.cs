using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class ErrorlogVM
    {
      public ErrorlogVM()
            {
                if (ApplicationVM.token != null)
                {
                    GetErrorlogs();
                }
            }

        private ObservableCollection<Errorlog> _errorlogs;

        public ObservableCollection<Errorlog> Errorlogs
            {
                get { return _errorlogs; }
                set { _errorlogs = value; OnPropertyChanged("Organisations"); }
            }

            private async void GetErrorlogs()
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.GetAsync("http://localhost:15237/api/employee");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        Errorlogs = JsonConvert.DeserializeObject<ObservableCollection<Organisation>>(json);
                    }
                }
            }
            public string Name
            {
                get { return "Organisation page"; }
            }
        }
    }