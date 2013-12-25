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
        public ActionResult Index(TicketVM VM)
        {
            Ticket Ticket = VM.Ticket;
            Ticket.TicketType = TicketType.GetTicketTypeById(VM.SelectedTicketType);
            TicketRepository.BookTicket(VM.Ticket);

            return RedirectToAction("Details", "Ticket", new { ticketId = Ticket.ID });
        }

        public ActionResult Details(int id)
        {
            Ticket ticket = TicketRepository.GetTicketById(id);
            return View(ticket);
        }

        public ActionResult List()
        {
            ObservableCollection<Ticket> ocTickets = TicketRepository.GetTickets();

            return View(ocTickets);
        }
    }
}
