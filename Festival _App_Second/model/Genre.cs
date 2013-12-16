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
    public class Genre
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

        public static ObservableCollection<Genre> GetGenres()
        {
            ObservableCollection<Genre> ocGenre = new ObservableCollection<Genre>();
            string sSql = "Select * from Genre";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                Genre g = new Genre()
                {
                    ID = (int)reader[0],
                    Name = (string)reader[1]
                };
                ocGenre.Add(g);
            }
            return ocGenre;
        }
    }
}
