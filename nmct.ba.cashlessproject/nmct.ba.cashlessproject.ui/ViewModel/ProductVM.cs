using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class ProductVM : ObservableObject, IPage
    {
       public ProductVM()
            {
                if (ApplicationVM.token != null)
                {
                    GetProducts();
                }
            }

        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products

            {
                get { return _products; }
                set { _products = value; OnPropertyChanged("Products"); }
            }

            private async void GetProducts()
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.GetAsync("http://localhost:15237/api/Product");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        Products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                    }
                }
            }
            public string Name
            {
                get { return "Product page"; }
            }
            private Product _selectedProduct;
            public Product SelectedProduct
            {
                get { return _selectedProduct; }
                set
                {
                    _selectedProduct = value;
                    OnPropertyChanged("SelectedProduct");
                    if (SelectedProduct != null)
                    {
                        Id = SelectedProduct.Id;
                        ProductName = SelectedProduct.Productname;
                        Price = SelectedProduct.Price.ToString();                      
                    }
                }
            }
            private int _id;
            public int Id
            {
                get { return _id; }
                set { _id = value; }
            }

            private string _productName;
            public string ProductName
            {
                get { return _productName; }
                set
                {
                    _productName = value;
                    OnPropertyChanged("ProductName");
                }
            }

            private string _price;
            public string Price
            {
                get { return _price; }
                set
                {
                    _price = value;
                    OnPropertyChanged("Price");
                }
            }

            public ICommand NewProductCommand
            {
                get { return new RelayCommand(NewProduct); }
            }

            public ICommand SaveProductCommand
            {
                get { return new RelayCommand(SaveProduct); }
            }

            public ICommand DeleteProductCommand
            {
                get { return new RelayCommand(DeleteProduct); }
            }


            private void NewProduct()
            {
                Product c = new Product();

                Products.Add(c);
                SelectedProduct = c;

            }
            private string _error;
            public string Error
            {
                get { return _error; }
                set { _error = value; OnPropertyChanged("Error"); }
            }


            private async void SaveProduct()
            {
                string input = JsonConvert.SerializeObject(SelectedProduct);

                try
                {
                    // check insert (no ID assigned) or update (already an ID assigned)
                    if (SelectedProduct.Id == 0)
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            client.SetBearerToken(ApplicationVM.token.AccessToken);
                            HttpResponseMessage response = await client.PostAsync("http://localhost:15237/api/product", new StringContent(input, Encoding.UTF8, "application/json"));
                            if (response.IsSuccessStatusCode)
                            {
                                string output = await response.Content.ReadAsStringAsync();
                                SelectedProduct.Id = Int32.Parse(output);
                                Error = "";
                                GetProducts();
                            }
                            else
                            {
                                Console.WriteLine("error");
                            }
                        }
                    }
                    else
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            client.SetBearerToken(ApplicationVM.token.AccessToken);
                            HttpResponseMessage response = await client.PutAsync("http://localhost:15237/api/product", new StringContent(input, Encoding.UTF8, "application/json"));
                            Error = "";
                            if (!response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("error");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error = "eerst New selecteren";
                    Console.WriteLine(ex.Message);
                }
            }

            private async void DeleteProduct()
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.SetBearerToken(ApplicationVM.token.AccessToken);
                        HttpResponseMessage response = await client.DeleteAsync("http://localhost:15237/api/product/" + SelectedProduct.Id);
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("error");
                        }
                        else
                        {
                            Products.Remove(SelectedProduct);
                            Error = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error = "Er is geen waarde geselecteerd";
                    Console.WriteLine(ex.Message);
                }
            }
    }
}