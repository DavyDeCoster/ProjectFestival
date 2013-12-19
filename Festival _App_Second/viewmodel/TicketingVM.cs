using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Festival__App_Second.viewmodel
{
    class TicketingVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Tickets"; }
        }

        private ObservableCollection<Ticket> _tickets;

        public ObservableCollection<Ticket> Tickets
        {
            get
            {
                if (_tickets == null)
                {
                    _tickets = Ticket.GetTickets();
                }

                return _tickets;

            }
            set { _tickets = value; }
        }

        private ObservableCollection<TicketType> _ticketTypes;

        public ObservableCollection<TicketType> TicketTypes
        {
            get
            {
                if (_ticketTypes == null)
                {
                    _ticketTypes = TicketType.GetTicketType();
                }
                return _ticketTypes;
            }
            set { _ticketTypes = value; }
        }

        private TicketType _selectedTicketType;

        public TicketType SelectedTicketType
        {
            get { return _selectedTicketType; }
            set { _selectedTicketType = value; OnPropertyChanged("SelectedTicketType"); }
        }
    }
}
