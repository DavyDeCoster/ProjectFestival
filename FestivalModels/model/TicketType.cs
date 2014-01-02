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
    public class TicketType
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Decimal _price;

        public Decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private int _availableTickets;

        public int AvailableTickets
        {
            get { return _availableTickets; }
            set { _availableTickets = value; }
        }

        public static ObservableCollection<TicketType> GetTicketType()
        {
            ObservableCollection<TicketType> ocTicketType = new ObservableCollection<TicketType>();
            string sSql = "Select * from TicketType";
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
    }
}
