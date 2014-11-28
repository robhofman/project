using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class RegisterKassaVM: ObservableObject, IPage
    {
   public RegisterVM()
        {
            if (ApplicationVM.token != null)
            {
                GetRegisersKassa();
            }
        }

        private ObservableCollection<RegisterKassa> _registersKassa;

        public ObservableCollection<RegisterKassa> RegistersKassa
        {
            get { return _registersKassa; }
            set { _registersKassa = value; OnPropertyChanged("RegistersKassa"); }
        }

        private async void GetRegisersKassa()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:15237/api/customer");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    RegistersKassa = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(json);
                }
            }
        }
        public string Name
        {
            get { return "Customer page"; }
        }
    }
}
