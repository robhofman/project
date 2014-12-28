using ba.cashlessproject.klant.ui;
using ba.cashlessproject.klant.ui.ViewModel;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace ba.cashlessproject.klant.ui.ViewModel
{
    class KlantLoginVM : ObservableObject, IPage
    {
        public ICommand GaNaarRegistrerenCommand
        {
            get { return new RelayCommand(naarRegistreer); }
        }

        private void naarRegistreer()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new KlantRegistreerVM());
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        public string Name
        {
            get { return "Klantlogin page"; }
            
        }
        public ICommand KlantLoginCommand
        { 
            get{ return new RelayCommand(KlantLogin);}
        }

        private void KlantLogin()
        {
            //ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            //ApplicationVM.token = GetToken();

            //if (!ApplicationVM.token.IsError)
            //{
            //    appvm.ChangePage(new ProductVM());
            //}
            //else
            //{
            //    Error = "id bestaat niet";
            //}
        }

        //private TokenResponse GetToken()
        //{
        //    OAuth2Client client = new OAuth2Client(new Uri("http://localhost:15237/token"));
        //    return client.RequestResourceOwnerPasswordAsync(Username, Password).Result;
        //}    
    }
}
