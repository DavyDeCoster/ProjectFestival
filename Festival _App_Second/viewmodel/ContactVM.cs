using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival__App_Second.viewmodel
{
    class ContactVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Contacten"; }
        }
    }
}
