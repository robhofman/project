﻿using ba.cashlessproject.klant.ui.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.klant.ui.ViewModel
{
    class KlantHerlaadVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "herlaad page"; }
        }
    }
}
