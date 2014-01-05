using Festival.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Festival.Model
{
    public class Festival : IDataErrorInfo
    {
        private DateTime _startDate;

        [Required(ErrorMessage = "De startdatum van een festival is verplicht")]
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;
        [Required(ErrorMessage = "De einddatum van een festival is verplicht")]
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public static Festival GetFestival()
        {
            string sSql = "SELECT * FROM FestivalDate Order By Id DESC";
            DbDataReader reader = DbAccess.GetData(sSql);
            reader.Read();

            Festival fest = new Festival();

                    fest.StartDate = reader.GetDateTime(1);
                    fest.EndDate = reader.GetDateTime(2);

            reader.Close();
            return fest;
        }

        public static void AddFestival(Festival f)
        {
            string sSql = "insert into FestivalDate (StartDate, EndDate) values(@startdate, @enddate)";

            DbParameter p1 = DbAccess.AddParameter("@startdate", f.StartDate);
            DbParameter p2 = DbAccess.AddParameter("@enddate", f.EndDate);

            DbAccess.ModifyData(sSql, p1, p2);
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
                DateTime newDate = StartDate.AddDays(i);

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

        public string Error
        {
            get { return "Dit object is niet als juist gevalideerd"; }
        }

        public string this[string columName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                    {
                        MemberName = columName
                    });
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                return string.Empty;
            }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
