using Festival.Model;
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
    public class LineUp : IDataErrorInfo
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private DateTime _date;

        [Required(ErrorMessage = "De datum van een Line Up is verplicht")]
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private String _from;

        [Required(ErrorMessage = "De starttijd van een Line Up is verplicht")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Dit tijdstip is niet juist van formaat! HH:MM")]
        public String From
        {
            get { return _from; }
            set { _from = value; }
        }

        private String _until;
        [Required(ErrorMessage = "De eindtijd van een Line Up is verplicht")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Dit tijdstip is niet juist van formaat! HH:MM")]
        public String Until
        {
            get { return _until; }
            set { _until = value; }
        }

        private Stage _stage;

        [Required(ErrorMessage = "De stage van een line up is verplicht")]
        public Stage Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }

        private Band _band;
        [Required(ErrorMessage = "De band van een line up is verplicht")]
        public Band Band
        {
            get { return _band; }
            set { _band = value; }
        }
        
        public static ObservableCollection<LineUp> GetLineUp()
        {
            ObservableCollection<LineUp> ocLineUp = new ObservableCollection<LineUp>();
            string sSql = "Select * from LineUp Order by Date ASC, Start ASC";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                LineUp Line = new LineUp()
                {
                    Id = (int)reader[0],
                    Date = (DateTime)reader[1],
                    From = (string)reader[2],
                    Until = (string)reader[3],
                    Stage = Stage.GetStageById((int)reader[4]),
                    Band = Band.GetBandById((int)reader[5]),
                };
                ocLineUp.Add(Line);
            }
            reader.Close();
            return ocLineUp;
        }

        public static LineUp GetLineUpById(int id)
        {
            ObservableCollection<LineUp> ocLineUp = GetLineUp();
            foreach (LineUp l in ocLineUp)
            {
                if (l.Id == id)
                {
                    return l;
                }
            }

            return null;
        }

        public static ObservableCollection<LineUp> GetLineUpByStage(Stage sStage)
        {
            ObservableCollection<LineUp> ocLineUp = GetLineUp();
            ObservableCollection<LineUp> ocLineUpStage = new ObservableCollection<LineUp>();
            foreach (LineUp lu in ocLineUp)
            {
                if (lu.Stage.Name == sStage.Name)
                {
                    ocLineUpStage.Add(lu);
                }
            }

            return ocLineUpStage;
        }

        public static void AddLineUp(LineUp l)
        {
            string sSql = "insert into LineUp (Date, Start, Stop, Stage, Band) values(@date, @from, @until, @stage, @band)";

            DbParameter p1 = DbAccess.AddParameter("@date", l.Date);
            DbParameter p2 = DbAccess.AddParameter("@from", l.From);
            DbParameter p3 = DbAccess.AddParameter("@until", l.Until);
            DbParameter p4 = DbAccess.AddParameter("@stage", l.Stage.ID);
            DbParameter p5 = DbAccess.AddParameter("@band", l.Band.ID);

            DbAccess.ModifyData(sSql, p1, p2, p3, p4, p5);
        }

        public static void RemoveLineUp(LineUp l)
        {
            string sSql = "Delete from LineUp where Id = @id";

            DbParameter p1 = DbAccess.AddParameter("@id", l.Id);
            DbAccess.ModifyData(sSql, p1);
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

        public static LineUp GetLineUpByBand(int id)
        {
            string sSql = "Select * from LineUp Where Band = @band Order by Date ASC, Start ASC";

            DbParameter p1 = DbAccess.AddParameter("@band", id);

            DbDataReader reader = DbAccess.GetData(sSql, p1);
            while (reader.Read())
            {
                LineUp Line = new LineUp()
                {
                    Id = (int)reader[0],
                    Date = (DateTime)reader[1],
                    From = (string)reader[2],
                    Until = (string)reader[3],
                    Stage = Stage.GetStageById((int)reader[4]),
                    Band = Band.GetBandById((int)reader[5]),
                };

                return Line;
            }

            reader.Close();
            return null;
        }
    }
}