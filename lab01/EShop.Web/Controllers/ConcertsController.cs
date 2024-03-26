using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Web.Data;
using EShop.Web.Models;

namespace EShop.Web.Controllers
{
    public class ConcertsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConcertsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Concerts
        public async Task<IActionResult> Index()
        {
            return View(await _context.concerts.ToListAsync());
        }

        // GET: Concerts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.concerts
                .FirstOrDefaultAsync(m => m.id == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // GET: Concerts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,concertName,dateAndTime,location,pictureURI,concertPrice")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                concert.id = Guid.NewGuid();
                _context.Add(concert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concert);
        }

        // GET: Concerts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.concerts.FindAsync(id);
            if (concert == null)
            {
                return NotFound();
            }
            return View(concert);
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,concertName,dateAndTime,location,pictureURI,concertPrice")] Concert concert)
        {
            if (id != concert.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertExists(concert.id))
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
            return View(concert);
        }

        // GET: Concerts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.concerts
                .FirstOrDefaultAsync(m => m.id == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var concert = await _context.concerts.FindAsync(id);
            if (concert != null)
            {
                _context.concerts.Remove(concert);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertExists(Guid id)
        {
            return _context.concerts.Any(e => e.id == id);
        }
    }
}
