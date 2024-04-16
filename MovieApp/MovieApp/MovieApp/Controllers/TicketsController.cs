using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;
using MovieApp.Repository;
using MovieApp.Service.Interface;

namespace MovieApp.Controllers
{
    public class TicketsController : Controller
    {

        private readonly ITicketService _ticketService;
        private readonly IMovieService _movieService;

        public TicketsController(ITicketService ticketService, IMovieService movieService)
        {
            _ticketService = ticketService;
            _movieService = movieService;
        }


        // GET: Tickets
        public IActionResult Index()
        {
            return View(_ticketService.GetTickets());
        }

        // GET: Tickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_movieService.GetMovies(), "id", "MovieDescription");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Id,Price,MovieId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
                _ticketService.CreateNewTicket(loggedInUser, ticket);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_movieService.GetMovies(), "id", "MovieDescription", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_movieService.GetMovies(), "id", "MovieDescription", ticket.MovieId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Price,MovieId")] Ticket ticket)
        {
            if (id != ticket.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _ticketService.UpdateTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_movieService.GetMovies(), "id", "MovieDescription", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return _ticketService.GetTicketById(id) != null;
        }
    }
}
