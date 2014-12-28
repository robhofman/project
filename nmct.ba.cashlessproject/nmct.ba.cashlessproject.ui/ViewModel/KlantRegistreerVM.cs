using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace nmct.ba.cashlessproject.ui.ViewModel
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

        private void Registreer()
        {
            maakKlant();
        }

        public Customer maakKlant()
        { 
            EIDReader data = new EIDReader();
            Customer c = new Customer();
            c.Customername = data.Name;
            c.Address = data.Street + data.Address;
            c.Balance = 0;
            c.Id = Int64.Parse(data.Rijks);
            c.Image = data.Picture;
            Picture = byteArrayToImage(data.Picture);
            SelectedCustomer = c;
            Customer x = SelectedCustomer;
            
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
