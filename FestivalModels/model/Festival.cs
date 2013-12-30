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

        public static Festival GetFestival()
        {
            string sSql = "Select TOP 1 * from Festival order by Id Desc";
            DbDataReader reader = DbAccess.GetData(sSql);
            reader.Read();

            Festival fest = new Festival()
            {
                StartDate = (DateTime)reader[0],
                EndDate = (DateTime)reader[1],
                RSS = (string)reader[2]
            };

            return fest;
        }

        public static void AddFestival(Festival f)
        {
            string sSql = "insert into Festival (StartDate, EndDate, Rss) values(@startdate, @enddate, @rss)";

            DbParameter p1 = DbAccess.AddParameter("@startdate", f.StartDate);
            DbParameter p2 = DbAccess.AddParameter("@enddate", f.EndDate);
            DbParameter p3 = DbAccess.AddParameter("@rss", f.RSS);

            DbAccess.ModifyData(sSql, p1, p2, p3);
        }
    }
}
