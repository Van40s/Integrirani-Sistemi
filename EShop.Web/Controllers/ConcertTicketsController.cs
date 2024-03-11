using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Web.Data;
using EShop.Web.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EShop.Web.Controllers
{
    public class ConcertTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ConcertAppUser> _userManager; // Change ApplicationUser to your user class

        public ConcertTicketsController(ApplicationDbContext context, UserManager<ConcertAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ConcertTickets
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.tickets.Include("concert").Include("forUser").ToListAsync();
            return View(tickets);
        }

        // GET: ConcertTickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertTicket = await _context.tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concertTicket == null)
            {
                return NotFound();
            }

            return View(concertTicket);
        }

        // GET: ConcertTickets/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ConcertTicketDTO dto = new ConcertTicketDTO();
            dto.concerts = await _context.concerts.ToListAsync();
            return View(dto);
        }

        // POST: ConcertTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConcertTicketDTO dto)
        {
            if (ModelState.IsValid)
            {
                ConcertAppUser? user = await _userManager.GetUserAsync(User);

                var concertTicket = new ConcertTicket
                {
                    Id = Guid.NewGuid(),
                    numberOfPeople = dto.numberOfPeople,
                    concert = _context.concerts.FirstOrDefault(c => c.id == dto.concertID),
                    forUser = user
                };
                _context.Add(concertTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: ConcertTickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertTicket = await _context.tickets.FindAsync(id);
            if (concertTicket == null)
            {
                return NotFound();
            }
            return View(concertTicket);
        }

        // POST: ConcertTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,numberOfPeople")] ConcertTicket concertTicket)
        {
            if (id != concertTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concertTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertTicketExists(concertTicket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(concertTicket);
        }

        // GET: ConcertTickets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertTicket = await _context.tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concertTicket == null)
            {
                return NotFound();
            }

            return View(concertTicket);
        }

        // POST: ConcertTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var concertTicket = await _context.tickets.FindAsync(id);
            if (concertTicket != null)
            {
                _context.tickets.Remove(concertTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertTicketExists(Guid id)
        {
            return _context.tickets.Any(e => e.Id == id);
        }
    }
}
