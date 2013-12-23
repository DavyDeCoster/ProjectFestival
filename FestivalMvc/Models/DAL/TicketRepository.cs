using Festival.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FestivalMvc.Models.DAL
{
    public class TicketRepository
    {
        public static ObservableCollection<TicketType> GetTicketTypes()
        {
            return TicketType.GetTicketType();
        }

        public static void BookTicket(Ticket t)
        {
            Console.WriteLine("lol");
        }

        public static Ticket GetTicketById(int id)
        {
            return Ticket.GetTicketById(id);
        }
    }
}