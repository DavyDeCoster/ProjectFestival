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
    public class TicketType : IDataErrorInfo
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _name;

        [Required(ErrorMessage = "De naam van de tickettype is verplicht")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Decimal _price;

        [Required(ErrorMessage = "De prijs van het ticket is verplicht")]
        [RegularExpression(@"[0-5]{1,10}", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public Decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private int _availableTickets;

        [Required(ErrorMessage = "Het aantal beschikbare ticketten is verplicht")]
        [RegularExpression(@"[0-6]{1,10}", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public int AvailableTickets
        {
            get { return _availableTickets; }
            set { _availableTickets = value; }
        }

        public static ObservableCollection<TicketType> GetTicketType()
        {
            ObservableCollection<TicketType> ocTicketType = new ObservableCollection<TicketType>();
            string sSql = "Select * from TicketType order by Name ASC";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                TicketType tt = new TicketType()
                {
                    ID = (int)reader[0],
                    Name = (string)reader[1],
                    Price = (decimal)reader[2],
                    AvailableTickets = (int)reader[3]
                };
                ocTicketType.Add(tt);
            }
            reader.Close();
            return ocTicketType;
        }

        public static TicketType GetTicketTypeById(int id)
        {
            ObservableCollection<TicketType> ocTicketType = GetTicketType();
            foreach (TicketType tt in ocTicketType)
            {
                if (tt.ID == id)
                {
                    return tt;
                }
            }

            return null;
        }

        public static void AddTicketType(TicketType tt)
        {
            string sSql = "Insert into TicketType (Name, Price, AvailableTickets) values(@name, @price, @itickets)";

            DbParameter p1 = DbAccess.AddParameter("@name", tt.Name);
            DbParameter p2 = DbAccess.AddParameter("@price", tt.Price);
            DbParameter p3 = DbAccess.AddParameter("@itickets", tt.AvailableTickets);

            DbAccess.ModifyData(sSql, p1, p2, p3);
        }

        public static void ChangeAvailable(int Id, int Amount)
        {
            string sSql = "UPDATE TicketType SET AvailableTickets = @amount WHERE Id = @id";

            DbParameter p1 = DbAccess.AddParameter("@amount", Amount);
            DbParameter p2 = DbAccess.AddParameter("@id", Id);

            DbAccess.ModifyData(sSql, p1, p2);
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
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
