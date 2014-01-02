using Festival.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Festival.Model
{
    public class Access
    {
        private string _name;

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

        public static void AddAccess(Access A)
        {
            string sSql = "Insert Into Access(Name) VALUES (@name);";

            DbParameter p1 = DbAccess.AddParameter("@name", A.Name);

            DbAccess.ModifyData(sSql, p1);
        }

        public static void UpdateAccess(Access a)
        {
            string sSql = "UPDATE Access SET Name=@name WHERE Id=@id";

            DbParameter p1 = DbAccess.AddParameter("@name", a.Name);
            DbParameter p2 = DbAccess.AddParameter("@id", a.Id);

            DbAccess.ModifyData(sSql, p1, p2);
        }
    }
}
