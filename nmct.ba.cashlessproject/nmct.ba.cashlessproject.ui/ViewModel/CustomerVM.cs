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
            GetCustomers();
        }

        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged("Customers"); }
        }

        private async void GetCustomers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:31929/api/customer");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(json);
                }
            }
        }
        public string Name
        {
            get { return "Customer page"; }
        }
    }
}
