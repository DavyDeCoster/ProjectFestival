using Festival.Model;
using FestivalMvc.Models.DAL;
using FestivalMvc.ViewModel;
using System;
using System.Collections.Generic;
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
            TicketVM Tvm = new TicketVM();
            Tvm.ListTicketType = new SelectList(TicketRepository.GetTicketTypes().ToList(), "Id", "Name");
            return View("Index",Tvm);
        }

        [HttpPost]
        public ActionResult Book(TicketVM Tvm)
        {
            Ticket newTicket = Tvm.NewTicket;
            newTicket.TicketType = TicketType.GetTicketTypeById(Tvm.SelectedTicketType);
            TicketRepository.BookTicket(Tvm.NewTicket);

            return RedirectToAction("Overview", "Ticket", new { ticketId = newTicket.ID });
        }

        public ActionResult Overview(int id)
        {
            Ticket ticket = TicketRepository.GetTicketById(id);
            return View(ticket);
        }
    }
}
