using Festival.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival__App_Second.viewmodel
{
    class LineUpVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "LineUp"; }
        }

        private ObservableCollection<LineUp> _lineUps;

        public ObservableCollection<LineUp> LineUps
        {
            get
            {
                if (_lineUps == null)
                {
                    _lineUps = LineUp.GetLineUp();
                }

                return _lineUps;

            }
            set { _lineUps = value; }
        }

        private ObservableCollection<DateTime> _days;

        public ObservableCollection<DateTime> Days
        {
            get
            {
                if (_days == null)
                {
                    _days = LineUp.GetUniqueDays();
                }

                return _days;

            }
            set { _days = value; }
        }
    }
}

