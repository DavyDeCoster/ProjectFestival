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
    public class LineUp
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private String _from;

        public String From
        {
            get { return _from; }
            set { _from = value; }
        }

        private String _until;

        public String Until
        {
            get { return _until; }
            set { _until = value; }
        }

        private Stage _stage;

        public Stage Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }

        private Band _band;

        public Band Band
        {
            get { return _band; }
            set { _band = value; }
        }
        
        public static ObservableCollection<LineUp> GetLineUp()
        {
            ObservableCollection<LineUp> ocLineUp = new ObservableCollection<LineUp>();
            string sSql = "Select * from LineUp";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                LineUp Line = new LineUp()
                {
                    Id = (int)reader[0],
                    Date = (DateTime)reader[1],
                    From = (DateTime)reader[2],
                    Until = (DateTime)reader[3],
                    Stage = Stage.GetStageById((int)reader[4]),
                    Band = Band.GetBandById((int)reader[5]),
                };
                ocLineUp.Add(Line);
            }
            return ocLineUp;
        }

        public static LineUp GetLineUpById(int id)
        {
            ObservableCollection<LineUp> ocLineUp = GetLineUp();
            foreach (LineUp l in ocLineUp)
            {
                if (l.ID == id)
                {
                    return l;
                }
            }

            return null;
        }
        
    }
}
