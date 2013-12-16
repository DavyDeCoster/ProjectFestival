using Festival.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.Model
{
    public class Ticket
    {
        private int _id;

        public int ID
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

        public static ObservableCollection<Ticket> GetTickets()
        {
            ObservableCollection<Ticket> ocTicket = new ObservableCollection<Ticket>();
            string sSql = "Select * from Ticket INNER JOIN TicketType on Ticket.TicketType = TicketType.Id";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                TicketType tt = new TicketType()
                {
                    ID = (int)reader[5],
                    Name = (string)reader[6],
                    Price = (double)reader[7],
                    AvailableTickets = (int)reader[8]
                };

                Ticket tick = new Ticket()
                {
                    ID = (int)reader[0],
                    Ticketholder = (string)reader[1],
                    TicketholderEmail = (string)reader[2],
                    TicketType = tt,
                    Amount = (int)reader[4]
                };
                ocTicket.Add(tick);
            }
            return ocTicket;
        }
    }
}
