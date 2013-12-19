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
            set { _lineUp = value; }
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
            set { _stages = value; }
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

