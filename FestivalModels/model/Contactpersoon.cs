using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.Model;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Festival.Model.DAL;
using System.ComponentModel;

namespace Festival.Model
{
    public class Contactpersoon:IDataErrorInfo
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _name;

        [Required(ErrorMessage = "De naam van een contactpersoon is verplicht")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private String _company;

        [Required(ErrorMessage = "De naam van een bedrijf is verplicht")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public String Company
        {
            get { return _company; }
            set { _company = value; }
        }

        private ContactpersoonType _jobRole;

        [Required(ErrorMessage = "De jobkeuze is verplicht")]
        public ContactpersoonType JobRole
        {
            get { return _jobRole; }
            set { _jobRole = value; }
        }
        private Access _accesszone;

        [Required(ErrorMessage = "De accesszone is verplicht")]
        public Access Accesszone
        {
            get { return _accesszone; }
            set { _accesszone = value; }
        }

        private String _city;

        [Required(ErrorMessage = "De stad is verplicht")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public String City
        {
            get { return _city; }
            set { _city = value; }
        }

        private String _email;

        [Required(ErrorMessage = "De email is verplicht")]
        [EmailAddress(ErrorMessage = "Een emailadres moet van het formaat 'example@example.org' zijn")]
        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private String _phone;

        [Required(ErrorMessage = "Het telefoonnummer is verplicht")]
        [RegularExpression(@"^\+[0-9]{0,3}([0-9]{3}|[0-9]{2}/)([0-9]{2,3}|[0-9]{2}).[0-9]{2}.[0-9]{2}$", ErrorMessage = "Dit telefoonnummer is niet van het juist formaat")]
        public String Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private String _cellphone;

        [Required(ErrorMessage = "Het gsmnummer is verplicht")]
        [RegularExpression(@"^\+[0-9]{0,3}([0-9]{3}|[0-9]{2}/)([0-9]{2,3}|[0-9]{2}).[0-9]{2}.[0-9]{2}$", ErrorMessage = "Dit gsmnummer is niet van het juist formaat")]
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
            reader.Close();
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

        public static void AddContact(Contactpersoon cp)
        {
            if (cp.ID != 0)
            {
                UpdateContact(cp);
            }
            else
            {
                string sSql = "insert into Contactpersoon (Name, Company, Jobrole, Accesszone, City, Email, Phone, Cellphone, IsGeprint) values(@name, @company, @jobrole, @access, @city, @email, @phone, @cellphone, @isgeprint)";

                DbParameter p1 = DbAccess.AddParameter("@name", cp.Name);
                DbParameter p2 = DbAccess.AddParameter("@company", cp.Company);
                DbParameter p3 = DbAccess.AddParameter("@jobrole", cp.JobRole.ID);
                DbParameter p4 = DbAccess.AddParameter("@access", cp.Accesszone.Id);
                DbParameter p5 = DbAccess.AddParameter("@city", cp.City);
                DbParameter p6 = DbAccess.AddParameter("@email", cp.Email);
                DbParameter p7 = DbAccess.AddParameter("@phone", cp.Phone);
                DbParameter p8 = DbAccess.AddParameter("@cellphone", cp.Cellphone);
                DbParameter p9 = DbAccess.AddParameter("@isgeprint", cp.Print);

                DbAccess.ModifyData(sSql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
            }
        }

        public static void UpdateContact(Contactpersoon cp)
        {
            string sSql = "Update Contactpersoon Set Name = @name, Company = @company, Jobrole = @jobrole, Accesszone = @access, City = @city, Email = @email, Phone = @phone, Cellphone = @cellphone, IsGeprint = @isgeprint) where Id = @id";

            DbParameter p1 = DbAccess.AddParameter("@name", cp.Name);
            DbParameter p2 = DbAccess.AddParameter("@company", cp.Company);
            DbParameter p3 = DbAccess.AddParameter("@jobrole", cp.JobRole.ID);
            DbParameter p4 = DbAccess.AddParameter("@access", cp.Accesszone.Id);
            DbParameter p5 = DbAccess.AddParameter("@city", cp.City);
            DbParameter p6 = DbAccess.AddParameter("@email", cp.Email);
            DbParameter p7 = DbAccess.AddParameter("@phone", cp.Phone);
            DbParameter p8 = DbAccess.AddParameter("@cellphone", cp.Cellphone);
            DbParameter p9 = DbAccess.AddParameter("@isgeprint", cp.Print);
            DbParameter p10 = DbAccess.AddParameter("@id", cp.ID);

            DbAccess.ModifyData(sSql, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
        }

        public static void DeleteContact(Contactpersoon cp)
        {
            string sSql = "DELETE FROM Contactpersoon WHERE Id =@id";

            DbParameter p1 = DbAccess.AddParameter("@id", cp.ID);

            DbAccess.ModifyData(sSql, p1);
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
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null),null, true);
        }
    }
}
