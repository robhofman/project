using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using nmct.ba.cashlessproject.model;
using System.Net.Http;
using Newtonsoft.Json;


namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class CustomerVM : ObservableObject, IPage
    {
        public CustomerVM()
        {
            if (ApplicationVM.token != null)
            {
                GetCustomers();
            }
        }

        private ObservableCollection<RegisterKassa> _customers;

        public ObservableCollection<RegisterKassa> Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged("Customers"); }
        }

        private async void GetCustomers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:15237/api/customer");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<ObservableCollection<RegisterKassa>>(json);
                }
            }
        }
        public string Name
        {
            get { return "Customer page"; }
        }
    }
}
