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
    public class ContactpersoonType
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public static ObservableCollection<ContactpersoonType> GetContactpersoonType()
        {
            ObservableCollection<ContactpersoonType> ocCpt = new ObservableCollection<ContactpersoonType>();
            string sSql = "Select * from ContactpersoonType";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                ContactpersoonType tt = new ContactpersoonType()
                {
                    ID = (int)reader[0],
                    Name = (string)reader[1],
                };
                ocCpt.Add(tt);
            }
            reader.Close();
            return ocCpt;
            
        }

        public static ContactpersoonType GetCpTypeById(int id)
        {
            ObservableCollection<ContactpersoonType> ocCpType = GetContactpersoonType();
            foreach (ContactpersoonType cpType in ocCpType)
            {
                if (cpType.ID == id)
                {
                    return cpType;
                }
            }

            return null;
        }

        public static void AddContactType(ContactpersoonType ct)
        {
            if(ct.ID != 0)
            {
                UpdateContactType(ct);
            }
            else
            {            
            string sSql = "Insert into ContactpersoonType (Name) values(@name)";

            DbParameter p1 = DbAccess.AddParameter("@name", ct.Name);
            DbAccess.ModifyData(sSql, p1);
            }
        }

        public static void UpdateContactType(ContactpersoonType ct)
        {
            string sSql = "Update ContactpersoonType Name=@name where Id=@id";

            DbParameter p1 = DbAccess.AddParameter("@name", ct.Name);
            DbParameter p2 = DbAccess.AddParameter("@id", ct.ID);
            DbAccess.ModifyData(sSql, p1, p2);
        }

        public static void DeleteContactType(ContactpersoonType ct)
        {
            string sSql = "DELETE FROM ContactpersoonType Where Id=@id";

            DbParameter p1 = DbAccess.AddParameter("@id", ct.ID);
            DbAccess.ModifyData(sSql, p1);
        }
    }
}
