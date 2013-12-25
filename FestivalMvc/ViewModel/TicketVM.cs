using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Festival.Model;
using System.Web.Mvc;

namespace FestivalMvc.ViewModel
{
    public class TicketVM
    {
        public Ticket Ticket { get; set; }
        public int SelectedTicketType { get; set; }
        public SelectList ListTicketType { get; set; }
    }
}   