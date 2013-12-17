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
    public class Stage
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

        private string _color;

        public string Color
        {
            get { return _color; }
            set { _color = value; }
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
                    Color = (string)reader[2]
                };
                ocStages.Add(Stage);
            }
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
    }
}
