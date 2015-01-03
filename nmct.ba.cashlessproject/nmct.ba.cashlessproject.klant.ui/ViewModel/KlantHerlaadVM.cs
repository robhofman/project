using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.klant.ui.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.klant.ui.ViewModel
{
    class KlantHerlaadVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "herlaad page"; }
        }

        public ICommand KiesBedragCommand
        {
            get { return new RelayCommand<string>(KiesBedrag); }
        }

        private void KiesBedrag(string cijfer)
        {
            if (Bedrag == null)
            {
                Bedrag = cijfer;
                BedragInt = Convert.ToInt16(cijfer);
            }
            else 
            {
                Bedrag += cijfer;
                BedragInt = Convert.ToInt16(cijfer);
            }
        }


        private string _bedrag;

        public string Bedrag
        {
            get { return _bedrag; }
            set { _bedrag = value; OnPropertyChanged("Bedrag"); }
        }

        private int _bedragInt;

        public int BedragInt
        {
            get { return _bedragInt; }
            set { _bedragInt = value; OnPropertyChanged("BedragInt"); }
        }
    }
}
