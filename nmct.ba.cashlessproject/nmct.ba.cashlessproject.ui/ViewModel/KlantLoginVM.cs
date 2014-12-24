using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class KlantLoginVM : ObservableObject, IPage
    {
        ICommand GaNaarRegistrerenCommand
        {
            get { return new RelayCommand(naarRegistreer); }
        }

        private void naarRegistreer()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new KlantRegistreerVM());
        }

        public string Name
        {
            get { return "Klantlogin page"; }
        }
    }
}
