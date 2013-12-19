using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.Model;
using System.Collections.ObjectModel;
using System.Data.Common;
using Festival.Model.DAL;

namespace Festival.Model
{
    public class Contactpersoon
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

        private String _company;

        public String Company
        {
            get { return _company; }
            set { _company = value; }
        }

        private ContactpersoonType _jobRole;

        public ContactpersoonType JobRole
        {
            get { return _jobRole; }
            set { _jobRole = value; }
        }
        private Access _accesszone;

        public Access Accesszone
        {
            get { return _accesszone; }
            set { _accesszone = value; }
        }

        private String _city;

        public String City
        {
            get { return _city; }
            set { _city = value; }
        }

        private String _email;

        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private String _phone;

        public String Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private String _cellphone;

        public String Cellphone
        {
            get { return _cellphone; }
            set { _cellphone = value; }
        }

        private Boolean _print;

        public Boolean Print
        {
            get { return _print; }
            set { _print = value; }
        }

        public static ObservableCollection<Contactpersoon> GetContacts()
        {
            ObservableCollection<Contactpersoon> ocContact = new ObservableCollection<Contactpersoon>();
            string sSql = "select * from Contactpersoon INNER JOIN ContactpersoonType on Contactpersoon.Jobrole = ContactpersoonType.Id INNER JOIN Access on Contactpersoon.Accesszone = Access.Id";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                ContactpersoonType conType = MakeContactpersoonType(reader);
                Access acc = MakeAccess(reader);

                Contactpersoon person = MakeContactpersoon(reader, conType, acc);

                ocContact.Add(person);
            }
            return ocContact;
        }

        private static Contactpersoon MakeContactpersoon(DbDataReader reader, ContactpersoonType conType, Access acc)
        {
            Contactpersoon person = new Contactpersoon()
                 {
                     ID = (int)reader[0],
                     Name = (string)reader[1],
                     Company = (string)reader[2],
                     JobRole = conType,
                     Accesszone = acc,
                     City = (string)reader[5],
                     Email = (string)reader[6],
                     Phone = (string)reader[7],
                     Cellphone = (string)reader[8],
                     Print = (bool)reader[9]
                 };
            return person;
        }

        private static Access MakeAccess(DbDataReader reader)
        {
            Access acc = new Access()
            {
                Id = (int)reader[12],
                Name = (string)reader[13]
            };
            return acc;
        }

        private static ContactpersoonType MakeContactpersoonType(DbDataReader reader)
        {
            ContactpersoonType conType = new ContactpersoonType()
            {
                ID = (int)reader[10],
                Name = (string)reader[11]
            };
            return conType;
        }

        public static Contactpersoon GetContactById(int id)
        {
            ObservableCollection<Contactpersoon> ocContact = GetContacts();
            foreach (Contactpersoon c in ocContact)
            {
                if (c.ID == id)
                {
                    return c;
                }
            }

            return null;
        }

        //public static Contactpersoon UpdateContact(
    }
}
