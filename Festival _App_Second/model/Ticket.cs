using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.Model
{
    public class Ticket
    {
        private String _id;

        public String ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _ticketholder;

        public String Ticketholder
        {
            get { return _ticketholder; }
            set { _ticketholder = value; }
        }

        private String _ticketholderEmail;

        public String TicketholderEmail
        {
            get { return _ticketholderEmail; }
            set { _ticketholderEmail = value; }
        }

        private TicketType _ticketType;

        public TicketType TicketType
        {
            get { return _ticketType; }
            set { _ticketType = value; }
        }

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        
    }
}
