using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.klant.ui.ViewModel;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace nmct.ba.cashlessproject.klant.ui.ViewModel
{
    class KlantRegistreerVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Customer register page"; }
        }

        public ICommand RegistreerCommand
        {
            get { return new RelayCommand(Registreer); }
        }

        public ICommand KlantOpslaanCommand
        {
            get { return new RelayCommand(Opslaan); }

        }

        private async void Opslaan()
        {
            string input = JsonConvert.SerializeObject(SelectedCustomer);

            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.PostAsync("http://localhost:15237/api/Customer", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string output = await response.Content.ReadAsStringAsync();

                    ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                    appvm.ChangePage(new KlantHerlaadVM());
                }
                else
                {
                    Console.WriteLine("error");
                }
            }
        }

        private void Registreer()
        {
            maakKlant();
        }

        public Customer maakKlant()
        {

            EIDReader data = new EIDReader();
            Customer c = new Customer();
            try
            {
                c.Customername = data.Name;
                c.Address = data.Street + data.Address;
                c.Balance = 0;
                c.Id = Int64.Parse(data.Rijks);
                Int64 k = c.Id;
                c.Image = data.Picture;
                Picture = byteArrayToImage(data.Picture);
                SelectedCustomer = c;
                Customer x = SelectedCustomer;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return c;

        }

        private BitmapImage _picture;
        public BitmapImage Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                OnPropertyChanged("Picture");
            }
        }
        private Customer _selected;
        public Customer SelectedCustomer
        {
            get { return _selected; }
            set { _selected = value; OnPropertyChanged("SelectedCustomer"); }
        }

        public static BitmapImage byteArrayToImage(byte[] byteArrayIn)
        {
            //maakt image van byte array
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);

            //maakt bitmap van image
            MemoryStream ms2 = new MemoryStream();
            returnImage.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
            ms2.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms2;
            bi.EndInit();
            return bi;
        }
    }
}
