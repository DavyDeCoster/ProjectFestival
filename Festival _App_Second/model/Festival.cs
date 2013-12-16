using DBHelper;
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
            DbDataReader reader = Database.GetData(sSql);
            while (reader.Read())
            {
                Festival fest = new Festival()
                {
                    ID = (int)reader[0],
                    StartDate = (DateTime)reader[1],
                    EndDate = (DateTime)reader[2],
                    RSS = (string)reader[3]
                };
                ocFestival.Add(fest);
            }
            return ocFestival;
        }

    }
}
