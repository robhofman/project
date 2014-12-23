using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class KiesAppVM: ObservableObject, IPage
    {
        public ICommand GaNaarKlantCommand
        {
               get { return new RelayCommand(naarKlant); }
        }

        private void naarKlant()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new KlantLoginVM());
        }

        public ICommand GaNaarManagementCommand
        {
            get { return new RelayCommand(naarManagement); }
        }

        private void naarManagement()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new ManagementLoginVM());
        }

        public ICommand GaNaarMedewerkerCommand
        {
            get { return new RelayCommand(naarMedewerker); }
        }

        private void naarMedewerker()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new KassaLoginVM());
        }

        public string Name
        {
            get { return "kiesapp page"; }
        }
    }
}
