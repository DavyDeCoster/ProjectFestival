using Festival.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FestivalMvc.Models.DAL
{
    public class BandRepository
    {
        public static ObservableCollection<Band> GetBands()
        {
            return Band.GetBands();
        }

        public static Band GetBandById(int id)
        {
            return Band.GetBandById(id);
        }
    }
}