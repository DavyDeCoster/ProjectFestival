using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.Model;

namespace Festival.Model
{
    public class Contactpersoon
    {
        private String _id;

        public String ID
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
        
        
    }
}
