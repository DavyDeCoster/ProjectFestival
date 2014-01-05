using Festival.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Festival.Model
{
    public class Access : IDataErrorInfo
    {
        private string _name;

        [Required(ErrorMessage = "De naam van een Accesszone is verplicht")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                    {
                        MemberName = columnName
                    });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }

        public static ObservableCollection<Access> GetAccess()
        {
            ObservableCollection<Access> ocAccess = new ObservableCollection<Access>();
            string sSql = "Select * from Access";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                Access a = new Access()
                {
                    Id = (int)reader[0],
                    Name = (string)reader[1]
                };
                ocAccess.Add(a);
            }
            reader.Close();
            return ocAccess;
        }

        public static Access GetAccessById(int id)
        {
            ObservableCollection<Access> ocAccess = GetAccess();
            foreach (Access a in ocAccess)
            {
                if (a.Id == id)
                {
                    return a;
                }
            }
            return null;
        }

        public static void AddAccess(Access a)
        {
            if (a.Id != 0)
            {
                UpdateAccess(a);
            }
            else
            {
                string sSql = "Insert Into Access(Name) VALUES (@name);";

                DbParameter p1 = DbAccess.AddParameter("@name", a.Name);

                DbAccess.ModifyData(sSql, p1);
            }
        }

        public static void UpdateAccess(Access a)
        {
            string sSql = "UPDATE Access SET Name=@name WHERE Id=@id";

            DbParameter p1 = DbAccess.AddParameter("@name", a.Name);
            DbParameter p2 = DbAccess.AddParameter("@id", a.Id);

            DbAccess.ModifyData(sSql, p1, p2);
        }

        public static void DeleteAccess(Access a)
        {
            string sSql = "Delete from Access Where Id=@id";

            DbParameter p1 = DbAccess.AddParameter("@id", a.Id);

            DbAccess.ModifyData(sSql, p1);
        }

        public string Error
        {
            get { return "Dit object is niet goed gevalideerd"; }
        }
    }
}
