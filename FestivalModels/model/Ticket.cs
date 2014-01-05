using Festival.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "De naam van de ticketholder is verplicht")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public String Ticketholder
        {
            get { return _ticketholder; }
            set { _ticketholder = value; }
        }

        private String _ticketholderEmail;

        [Required(ErrorMessage = "Het emailadres is verplicht")]
        [EmailAddress(ErrorMessage = "Een emailadres moet van het formaat 'example@example.org' zijn")]
        public String TicketholderEmail
        {
            get { return _ticketholderEmail; }
            set { _ticketholderEmail = value; }
        }

        private TicketType _ticketType;

        [Required(ErrorMessage = "Het tickettype is verplicht")]
        public TicketType TicketType
        {
            get { return _ticketType; }
            set { _ticketType = value; }
        }

        private int _amount;

        [Required(ErrorMessage = "Het aantal is verplicht")]
        [RegularExpression(@"[0-2]{1,10}", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
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
                    Price = (decimal)reader[7],
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
            reader.Close();
            return ocTicket;
        }

        public static Ticket GetTicketById(int id)
        {
            ObservableCollection<Ticket> ocTicket = GetTickets();
            foreach (Ticket t in ocTicket)
            {
                if (t.ID == id)
                {
                    return t;
                }
            }

            return null;
        }

        public static int GetLastId()
        {
            string sSql = "SELECT TOP 1 [Id]  FROM [Festival].[dbo].[Ticket] order by Id DESC";

            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                return (int)reader[0];
            }

            return 0;
        }

        public static void AddTicket(Ticket T)
        {
            string sSql = "Insert Into Ticket(Ticketholder, TicketholderEmail, TicketType, Amount) VALUES (@name, @email, @typeid, @amount);";

            DbParameter p1 = DbAccess.AddParameter("@name", T.Ticketholder);
            DbParameter p2 = DbAccess.AddParameter("@email", T.TicketholderEmail);
            DbParameter p3 = DbAccess.AddParameter("@amount", T.Amount);
            DbParameter p4 = DbAccess.AddParameter("@typeid", T.TicketType.ID);

            DbAccess.ModifyData(sSql, p1, p2, p3, p4);

            int iChange = T.TicketType.AvailableTickets - T.Amount;
            TicketType.ChangeAvailable(T.TicketType.ID, iChange);
        }

        public string Error
        {
            get { return "Dit object is niet als juist gevalideerd"; }
        }

        public string this[string columName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                    {
                        MemberName = columName
                    });
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                return string.Empty;
            }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null),
            null, true);
        }
    }
}
