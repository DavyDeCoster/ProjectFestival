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
            string sSql = "SELECT * FROM FestivalDate Order By Id DESC";
            DbDataReader reader = DbAccess.GetData(sSql);

            Festival fest = new Festival();

                    fest.StartDate = Convert.ToDateTime(reader[0]);
                    fest.EndDate = Convert.ToDateTime(reader[1]);
                    fest.RSS = (string)reader[2];

            reader.Close();
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

        public static ObservableCollection<DateTime> GetUniqueDays()
        {
            Festival f = GetFestival();

            DateTime EndDate = f.EndDate;
            DateTime StartDate = f.StartDate;

            int iDays = CalculateDays(StartDate, EndDate);

            ObservableCollection<DateTime> ocDate = GenerateDays(StartDate, iDays);

            return ocDate;
        }

        private static ObservableCollection<DateTime> GenerateDays(DateTime StartDate, int iDays)
        {
            ObservableCollection<DateTime> ocDate = new ObservableCollection<DateTime>();

            for (int i = 0; i <= iDays; i++)
            {
                DateTime newDate = StartDate;
                newDate.AddDays(i);

                ocDate.Add(newDate);
            }


            return ocDate;
        }

        private static int CalculateDays(DateTime StartDate, DateTime EndDate)
        {
            //bron --> http://social.msdn.microsoft.com/Forums/vstudio/en-US/0625cefa-461b-4a3c-b7f0-d39d06741b70/how-do-i-calculate-the-number-of-days-between-two-dates-in-c?forum=csharpgeneral
            long tickDiff = EndDate.Ticks - StartDate.Ticks;
            tickDiff = tickDiff / 10000000;
            int iDays = (int)(tickDiff / 86400);
            return iDays;
        }
    }
}
