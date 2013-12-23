using Festival.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FestivalMvc.Models.DAL
{
    public class LineUpRepository
    {
        public static ObservableCollection<LineUp> GetLineUps()
        {
            return LineUp.GetLineUp();
        }
    }
}