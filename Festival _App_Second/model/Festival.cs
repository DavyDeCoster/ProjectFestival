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
    public class Festival
    {
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        private string _rss;

        public string RSS
        {
            get { return _rss; }
            set { _rss = value; }
        }

        public static ObservableCollection<Festival> GetFestival()
        {
            ObservableCollection<Festival> ocFestival = new ObservableCollection<Festival>();
            string sSql = "Select * from Festival";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                Festival fest = new Festival()
                {
                    StartDate = (DateTime)reader[0],
                    EndDate = (DateTime)reader[1],
                    RSS = (string)reader[2]
                };
                ocFestival.Add(fest);
            }
            return ocFestival;
        }

    }
}
