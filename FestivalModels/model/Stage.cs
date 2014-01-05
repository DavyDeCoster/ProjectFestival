using Festival.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Festival.Model
{
    public class Stage : IDataErrorInfo
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _name;

        [Required(ErrorMessage = "De naam van de stage is verplicht")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        

        public static ObservableCollection<Stage> GetStages()
        {
            ObservableCollection<Stage> ocStages = new ObservableCollection<Stage>();
            string sSql = "Select * from Stage";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                Stage Stage = new Stage()
                {
                    ID = (int)reader[0],
                    Name = (string)reader[1],
                };
                ocStages.Add(Stage);
            }
            reader.Close();
            return ocStages;
        }



        public static Stage GetStageById(int id)
        {
            ObservableCollection<Stage> ocStages= GetStages();
            foreach (Stage s in ocStages)
            {
                if (s.ID == id)
                {
                    return s;
                }
            }
            return null;
        }

        public static void AddStage(Stage s)
        {
            string sSql = "insert into Stage (Name) values (@name)";

            DbParameter p1 = DbAccess.AddParameter("@name", s.Name);

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
    }
}
