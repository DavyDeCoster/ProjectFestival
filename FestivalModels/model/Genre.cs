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
            reader.Close();
            return ocGenre;
        }

        public static Genre GetGenreById(int id)
        {
            ObservableCollection<Genre> ocGenre = GetGenres();

            foreach (Genre g in ocGenre)
            {
                if (id == g.ID)
                {
                    return g;
                }
            }

            return null;
        }

        public static int GetIdByGenres(ObservableCollection<Genre> ocGenre)
        {

            if (ocGenre.Count != 2)
            {
                return 0;
            }

            string sSql = "Select * from BandGenre Where Genre1 = @genre1 and Genre2 = @genre2";

            DbParameter p1 = DbAccess.AddParameter("@genre1", ocGenre[0].ID);
            DbParameter p2 = DbAccess.AddParameter("@genre2", ocGenre[1].ID);

            DbDataReader Reader = DbAccess.GetData(sSql, p1, p2);
            Reader.Read();

            if (Reader.HasRows == false)
            {
                AddBandGenre(ocGenre);
                DbParameter p3 = DbAccess.AddParameter("@genre1", ocGenre[0].ID);
                DbParameter p4 = DbAccess.AddParameter("@genre2", ocGenre[1].ID);
                Reader = DbAccess.GetData(sSql, p3, p4);
                Reader.Read();
            }

            return (int)Reader[0];
        }

        public static void AddBandGenre(ObservableCollection<Genre> ocGenre)
        {
            if (ocGenre.Count == 2)
            {
                string sSql = "insert into BandGenre (genre1, genre2) values(@genre1, @genre2)";

                DbParameter p1 = DbAccess.AddParameter("@genre1", ocGenre[0].ID);
                DbParameter p2 = DbAccess.AddParameter("@genre2", ocGenre[1].ID);

                DbAccess.ModifyData(sSql, p1, p2);
            }
        }

        public static void AddGenre(Genre g)
        {
            string sSql = "Insert into Genre(Name) values (@name)";

            DbParameter p1 = DbAccess.AddParameter("@name", g.Name);

            DbAccess.ModifyData(sSql, p1);
        }
    }
}
