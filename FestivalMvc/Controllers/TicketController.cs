using Festival.Model;
using FestivalMvc.Models.DAL;
using FestivalMvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestivalMvc.Controllers
{
    public class TicketController : Controller
    {
        //
        // GET: /Ticket/

        public ActionResult Index()
        {
            TicketVM VM = new TicketVM();
            VM.ListTicketType = new SelectList(TicketRepository.GetTicketTypes().ToList(), "Id", "Name");
            return View("Index", VM);
        }

        [HttpPost]
        public ActionResult Book(TicketVM VM)
        {
            Ticket Ticket = VM.Ticket;
            Ticket.TicketType = TicketType.GetTicketTypeById(VM.SelectedTicketType);
            TicketRepository.BookTicket(VM.Ticket);
            Ticket.ID = TicketRepository.GetLastId();

            return RedirectToAction("Details", "Ticket", new { ticketId = Ticket.ID });
        }

        public ActionResult Details(int ticketId)
        {
            Ticket ticket = TicketRepository.GetTicketById(ticketId);
            return View(ticket);
        }

        public ActionResult List()
        {
            ObservableCollection<Ticket> ocTickets = TicketRepository.GetTickets();

            return View(ocTickets);
        }

        public ActionResult AvailableTickets()
        {
            ObservableCollection<TicketType> ocTicketType = TicketType.GetTicketType();

            return View(ocTicketType);
        }
    }
}
