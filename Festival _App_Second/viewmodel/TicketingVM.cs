using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival__App_Second.viewmodel
{
    class TicketingVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Tickets"; }
        }
    }
}
