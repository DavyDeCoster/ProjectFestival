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
            set { _ticketTypes = value;
            OnPropertyChanged("TicketTypes");
            }
        }

        private TicketType _selectedTicketType;

        public TicketType SelectedTicketType
        {
            get { return _selectedTicketType; }
            set { _selectedTicketType = value;
            if (_selectedTicketType != null)
            {
                NewTicket.TicketType = SelectedTicketType;
            }
                OnPropertyChanged("SelectedTicketType"); }
        }

        private Ticket _newTicket;

        public Ticket NewTicket
        {
            get {
                if (_newTicket == null)
                {
                    _newTicket = new Ticket();
                }
                
                return _newTicket; }

            set { _newTicket = value;  OnPropertyChanged("NewTicket");}
        }

        private TicketType _newType;

        public TicketType NewType
        {
            get 
            {
                if (_newType == null)
                {
                    _newType = new TicketType();
                }

                return _newType; 
            }
            set { _newType = value; OnPropertyChanged("NewType");}
        }
        

        public ICommand OrderCommand
        {
            get { return new RelayCommand(OrderTicket, NewTicket.IsValid);}
        }

        public ICommand SaveTypeCommand
        {
            get { return new RelayCommand(SaveType, NewType.IsValid); }
        }

        private void SaveType()
        {
            TicketType.AddTicketType(NewType);
            TicketTypes = TicketType.GetTicketType();
        }

        private void OrderTicket()
        {
            Ticket.AddTicket(NewTicket);
            TicketTypes = TicketType.GetTicketType();
        }
    }
}
