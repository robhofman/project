using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using nmct.ba.cashlessproject.model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;


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
            set { _customers = value; 
                OnPropertyChanged("Customers"); }
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
                    string q = json;
                    Customers = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(json);

                }
            }
        }
        public string Name
        {
            get { return "Customer page"; }
        }

        private long _id;
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                OnPropertyChanged("CustomerName");
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        private byte[] _picture;
        public byte[] Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                OnPropertyChanged("Picture");
            }
        }

        private string _balance;
        public string Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
                if (SelectedCustomer != null)
                {
                    Id = SelectedCustomer.Id;
                    CustomerName = SelectedCustomer.Customername;
                    Address = SelectedCustomer.Address;
                    Picture = SelectedCustomer.Image;
                    Balance = SelectedCustomer.Balance.ToString();
                }
            }
        }

        public ICommand UpdateCustomerCommand
        {
            get { return new RelayCommand(UpdateCustomer); }
        }

        public async void UpdateCustomer()
        {
            Customer c = new Customer();
            c.Id = Id;
            c.Customername = CustomerName;
            c.Address = Address;
            c.Balance = Convert.ToDouble(Balance);

            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string customer = JsonConvert.SerializeObject(c);
                HttpResponseMessage response = await
                client.PutAsync("http://localhost:15237/api/Customer", new StringContent(customer,
                Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    GetCustomers();
                }
            }
        }




    }
}
