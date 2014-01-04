using Festival.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival__App_Second.viewmodel
{
    class LineUpVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "LineUp"; }
        }

        private ObservableCollection<LineUp> _lineUp;

        public ObservableCollection<LineUp> LineUps
        {
            get
            {
                if (_lineUp == null)
                {
                    _lineUp = LineUp.GetLineUp();
                }

                return _lineUp;

            }
            set { _lineUp = value; OnPropertyChanged("LineUps"); }
        }

        private ObservableCollection<Stage> _stages;

        public ObservableCollection<Stage> Stages
        {
            get
            {
                if (_stages == null)
                {
                    _stages = Stage.GetStages();
                }

                return _stages;

            }
            set { _stages = value; OnPropertyChanged("Stages"); }
        }

        private ObservableCollection<DateTime> _days;

        public ObservableCollection<DateTime> Days
        {
            get
            {
                if (_days == null)
                {
                    _days = Festival.Model.Festival.GetUniqueDays();
                }

                return _days;

            }
            set { _days = value; OnPropertyChanged("Days"); }
        }

        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get
            {
                if (_bands == null)
                {
                    _bands = Band.GetBands();
                }
                return _bands;
            }
            set { _bands = value; OnPropertyChanged("Bands"); }
        }

        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set 
            { 
                _selectedBand = value;
                NewLineUp.Band = SelectedBand;
                OnPropertyChanged("SelectedBand"); }
        }

        private DateTime _selectedDay;

        public DateTime SelectedDay
        {
            get { return _selectedDay; }
            set { _selectedDay = value; }
        }
        

        private string _selectedFrom;

        public string SelectedFrom
        {
            get { return _selectedFrom; }
            set 
            { 
                _selectedFrom = value;
                NewLineUp.From = SelectedFrom;
                OnPropertyChanged("SelectedFrom"); }
        }

        private string _selectedUntil;

        public string SelectedUntil
        {
            get { return _selectedUntil; }
            set 
            { 
                _selectedUntil = value;
                NewLineUp.Until = SelectedUntil;

                OnPropertyChanged("SelectedUntil");
            
            }
        }

        private Stage _selectedStage;

        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set 
            { 
                _selectedStage = value;
                NewLineUp.Stage = SelectedStage;

                OnPropertyChanged("SelectedStage");       
            }
        }
        
        

        private LineUp _newLineUp;

        public LineUp NewLineUp
        {
            get 
            {
                if (_newLineUp == null)
                {
                    _newLineUp = new LineUp();
                }
                
                return _newLineUp; 
            }
            set { _newLineUp = value; OnPropertyChanged("NewLineUp"); }
        }



    }
}