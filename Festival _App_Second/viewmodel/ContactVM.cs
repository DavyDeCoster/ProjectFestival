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

            set { _conType = value; OnPropertyChanged("ConType"); }
        }

        private ContactpersoonType _selectedConType;

	    public ContactpersoonType SelectedConType
	    {
		    get { return _selectedConType;}
            set { _selectedConType = value;

            if (_selectedConType != null)
            {
                NewConType = SelectedConType;
            }
                OnPropertyChanged("SelectedConType"); }
	    }

        private Access _selectedAccess;

	    public Access SelectedAccess
	    {
		    get { return _selectedAccess;}
            set { _selectedAccess = value;
            if (_selectedAccess != null)
            {
                NewAccess = SelectedAccess;
            }
                
                OnPropertyChanged("SelectedAccess"); }
	    }
	

        private Contactpersoon _selectedContact;

        public Contactpersoon SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                if (_selectedContact != null)
                {
                    SelectedConType = SelectedContact.JobRole;
                    SelectedAccess = SelectedContact.Accesszone;
                    NewContact = SelectedContact;
                }
                OnPropertyChanged("SelectedContact"); }
        }

        private Contactpersoon _newContact;

        public Contactpersoon NewContact
        {
            get {
                if (_newContact == null)
                {
                    _newContact = new Contactpersoon();
                }

                return _newContact; }
            set { _newContact = value; OnPropertyChanged("NewContact"); }
        }

        private Access _newAccess;

        public Access NewAccess
        {
            get {
                if (_newAccess == null)
                {
                    _newAccess = new Access();
                }

                return _newAccess; 
            }
            set { _newAccess = value; OnPropertyChanged("NewAccess"); }
        }

        private ContactpersoonType _newConType;

        public ContactpersoonType NewConType
        {
            get {
                if (_newConType == null)
                {
                    _newConType = new ContactpersoonType();
                }
                return _newConType; 
            }
            set { _newConType = value; OnPropertyChanged("NewConType"); }
        }

        private ObservableCollection<Access> _accesses;

        public ObservableCollection<Access> Accesses
        {
            get {

                if (_accesses == null)
                {
                    _accesses = Access.GetAccess();
                }
                return _accesses;
            }

            set { _accesses = value; OnPropertyChanged("Accesses"); }
        }

        #region Commands

        public ICommand SaveCommand
        {
            get { return new RelayCommand(SaveContact); }
        }

        public void SaveContact()
        {
           NewContact.JobRole = SelectedConType;
           NewContact.Accesszone = SelectedAccess;

            Contactpersoon.AddContact(NewContact);
        }

        public ICommand DeleteCommand
        {
            get { return new RelayCommand(DeleteContact); }
        }

        private void DeleteContact()
        {
            Contactpersoon.DeleteContact(SelectedContact);
        }

        public ICommand JobSaveCommand
        {
            get { return new RelayCommand(SaveConType); }
        }

        public void SaveConType()
        {
            ContactpersoonType.AddContactType(NewConType);
            ConType = ContactpersoonType.GetContactpersoonType();
        }

        public ICommand JobDeleteCommand
        {
            get { return new RelayCommand(DeleteConType); }
        }

        public void DeleteConType()
        {
            ContactpersoonType.DeleteContactType(NewConType);
            ConType = ContactpersoonType.GetContactpersoonType();
        }

        public ICommand AccessSaveCommand
        {
            get { return new RelayCommand(SaveAccess); }
        }

        public void SaveAccess()
        {
            Access.AddAccess(NewAccess);
            Accesses = Access.GetAccess();
        }

        public ICommand AccessDeleteCommand
        {
            get { return new RelayCommand(DeleteAccess); }
        }

        public void DeleteAccess()
        {
            Access.DeleteAccess(NewAccess);
            Accesses = Access.GetAccess();
        }
        #endregion
    }
}
