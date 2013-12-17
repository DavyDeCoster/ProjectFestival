using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.Model;
using System.Collections.ObjectModel;

namespace Festival__App_Second.viewmodel
{
    class ContactVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Contacten"; }
        }

        private ObservableCollection<Contactpersoon> _contacts;

        public ObservableCollection<Contactpersoon> Contacts
        {
            get {
                    if (_contacts == null)
                     {
                         _contacts = Contactpersoon.GetContacts();
                    }

                    return _contacts; 
                }

            set { 
                Contacts = value; 
                OnPropertyChanged("Contacts");
                    
            }
        }

        private ObservableCollection<ContactpersoonType> _conType;

        public ObservableCollection<ContactpersoonType> ConType
        {
            get {
                    if (_conType == null)
                    {
                        _conType = ContactpersoonType.GetContactpersoonType();
                    }
                        return _conType; 
                }   

            set { _conType = value; }
        }

        #region DatabaseProps

        private Contactpersoon _selectedContact;

        public Contactpersoon SelectedContact
        {
            get { return _selectedContact; }
            set { _selectedContact = value; OnPropertyChanged("SelectedContact"); }
        }
        

        //private string _contactname;

        //public string ContactName
        //{
        //    get { return _name; }
        //    set { _name = value; }
        //}

        //private string _company;

        //public string Company
        //{
        //    get { return _company; }
        //    set { _company = value; }
        //}

        //private string _jobrole;

        //public string JobRole
        //{
        //    get { return _jobrole; }
        //    set { _jobrole = value; }
        //}

        //private string _access;

        //public string Access
        //{
        //    get { return _access; }
        //    set { _access = value; }
        //}

        //private string _city;

        //public string City
        //{
        //    get { return _city; }
        //    set { _city = value; }
        //}

        //private string _email;

        //public string Email
        //{
        //    get { return _email; }
        //    set { _email = value; }
        //}

        //private string _phone;

        //public string Phone
        //{
        //    get { return _phone; }
        //    set { _phone = value; }
        //}

        //private string _cellphone;

        //public string Cellphone
        //{
        //    get { return _cellphone; }
        //    set { _cellphone = value; }
        //}

        //private bool _isGeprint;

        //public bool IsGeprint
        //{
        //    get { return _isGeprint; }
        //    set { _isGeprint = value; }
        //}
        
        #endregion

    }
}
