using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Festival__App_Second.viewmodel
{
    class HoofdmenuVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Hoofdmenu"; }
        }

        private ObservableCollection<Message> _messages;

        public ObservableCollection<Message> Messages
        {
            get {
                if (_messages == null)
                {
                    _messages = Message.GetMessages();
                }
                
                return _messages; }
            set { _messages = value; OnPropertyChanged("Messages"); }
        }

        private Festival.Model.Festival _newFestival;

        public Festival.Model.Festival NewFestival
        {
            get {

                if (_newFestival == null)
                {
                    _newFestival = new Festival.Model.Festival();
                }
                return _newFestival; 
            
            }
            set { _newFestival = value; OnPropertyChanged("NewFestival"); }
        }

        private DateTime _selectedStartDate;

        public DateTime SelectedStartDate
        {
            get 
            {
                if (_selectedStartDate == null)
                {
                    _selectedStartDate = new DateTime();
                }
                return _selectedStartDate; 
            }
            set { _selectedStartDate = value; OnPropertyChanged("SelectedStartDate");}
        }

        private DateTime _selectedEndDate;

        public DateTime SelectedEndDate
        {
            get 
            {
                if (_selectedEndDate == null)
                {
                    _selectedEndDate = new DateTime();
                }
                return _selectedEndDate;
            }
            set { _selectedEndDate = value; OnPropertyChanged("SelectedStartDate");}
        }
        

        public ICommand SaveFestivalCommand
        {
            get { return new RelayCommand(SaveFestival, NewFestival.IsValid); }
        }

        public ICommand OpenInstellingenCommand
        {
            get { return new RelayCommand(OpenInstellingen); }
        }

        private void OpenInstellingen()
        {
            Festival__App_Second.view.Instellingen winInst = new Festival__App_Second.view.Instellingen();
            winInst.Show();
        }

        private void SaveFestival()
        {
            NewFestival.StartDate = SelectedStartDate;
            NewFestival.EndDate = SelectedEndDate;
            Festival.Model.Festival.AddFestival(NewFestival);
        }
    }
}
