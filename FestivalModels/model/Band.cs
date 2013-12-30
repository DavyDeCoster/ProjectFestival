using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Common;
using Festival.Model.DAL;

namespace Festival.Model
{
    public class Band
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

        private String _picture;

        public String Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        private String _description;

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private String _twitter;

        public String Twitter
        {
            get { return _twitter; }
            set { _twitter = value; }
        }

        private String _facebook;

        public String Facebook
        {
            get { return _facebook; }
            set { _facebook = value; }
        }

        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; }
        }
        
        public static ObservableCollection<Band> GetBands()
        {
            ObservableCollection<Band> ocFestival = new ObservableCollection<Band>();
            string sSql = "Select * from Band INNER JOIN BandGenre on Band.Genre = BandGenre.Id";
            DbDataReader reader = DbAccess.GetData(sSql);
            ObservableCollection<Genre> ocGenre = new ObservableCollection<Genre>();
            while (reader.Read())
            {
                ocGenre.Add(Genre.GetGenreById((int)reader[8]));
                ocGenre.Add(Genre.GetGenreById((int)reader[7]));
                Band b = new Band()
                {
                    ID = (int)reader[0],
                    Name = (string)reader[1],
                    Picture = (string)reader[2],
                    Description = (string)reader[3],
                    Twitter = (string)reader[4],
                    Facebook = (string)reader[5],
                    Genres = ocGenre
                };
                ocFestival.Add(b);
            }
            return ocFestival;
        }

        public static Band GetBandById(int id)
        {
            ObservableCollection<Band> ocBands = GetBands();

            foreach (Band b in ocBands)
            {
                if (b.ID == id)
                {
                    return b;
                }
            }

            return null;
        }

        public static void AddBand(Band b)
        {
            string sSql = "Insert Into Band(Name, Picture, Description, Twitter, Facebook, Genre) VALUES (@name, @picture, @description, @twitter, @facebook, @genre);";

            DbParameter p1 = DbAccess.AddParameter("@name", b.Name);
            DbParameter p2 = DbAccess.AddParameter("@picture", b.Picture);
            DbParameter p3 = DbAccess.AddParameter("@description", b.Description);
            DbParameter p4 = DbAccess.AddParameter("@twitter", b.Twitter);
            DbParameter p5 = DbAccess.AddParameter("@facebook", b.Facebook);
            DbParameter p6 = DbAccess.AddParameter("@genre", Genre.GetIdByGenres(b.Genres));

            DbAccess.ModifyData(sSql, p1, p2, p3, p4, p5, p6);
        }

        public static void UpdateBand(Band b)
        {
            string sSql = "UPDATE Band SET Name=@name, Picture=@picture, Description=@description, Twitter=@twitter, Facebook=@facebook, Genre=@genre WHERE Id=@id";

            DbParameter p1 = DbAccess.AddParameter("@name", b.Name);
            DbParameter p2 = DbAccess.AddParameter("@picture", b.Picture);
            DbParameter p3 = DbAccess.AddParameter("@description", b.Description);
            DbParameter p4 = DbAccess.AddParameter("@twitter", b.Twitter);
            DbParameter p5 = DbAccess.AddParameter("@facebook", b.Facebook);
            DbParameter p6 = DbAccess.AddParameter("@genre", Genre.GetIdByGenres(b.Genres));
            DbParameter p7 = DbAccess.AddParameter("@id", b.ID);

            DbAccess.ModifyData(sSql, p1, p2, p3, p4, p5, p6, p7);
        }
    }
}
