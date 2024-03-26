using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<EShopApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<EShopApplicationUser> _userManager)
        {
            _context = context;
            this._userManager = _userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        //// GET: Orders/Create
        //public IActionResult Create()
        //{
        //    ViewData["userId"] = new SelectList(_context.Set<EShopApplicationUser>(), "Id", "FirstName");
        //    return View();
        //}

        //// POST: Orders/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("id,userId")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        order.id = Guid.NewGuid();
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["userId"] = new SelectList(_context.Set<EShopApplicationUser>(), "Id", "Id", order.userId);
        //    return View(order);
        //}


        [Authorize]
        public async Task<IActionResult> Create()
        {
            CreateOrderDTO createOrderDTO = new CreateOrderDTO();
            createOrderDTO.availableTickets = await _context.Tickets.Include("Movie").ToListAsync();
            EShopApplicationUser? user = await _userManager.GetUserAsync(User);
            createOrderDTO.user = user;
            return View(createOrderDTO);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateOrderDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                EShopApplicationUser user = await _userManager.Users
                    .Include(u => u.order) // Include the order
                    .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

                if (user.order == null)
                {
                    Order order = new Order();
                    user.order = order;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }


                TicketInOrder ticketInOrder = new TicketInOrder();
                ticketInOrder.quantity = orderDTO.quantity;
                ticketInOrder.ticket = await _context.Tickets.FindAsync(orderDTO.ticketId);
                ticketInOrder.order = user?.order;
                _context.Add(ticketInOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDTO);
        }



        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["userId"] = new SelectList(_context.Set<EShopApplicationUser>(), "Id", "Id", order.userId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,userId")] Order order)
        {
            if (id != order.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.id))
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
            ViewData["userId"] = new SelectList(_context.Set<EShopApplicationUser>(), "Id", "Id", order.userId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();

            Order newOrder = new Order();

            EShopApplicationUser? user = await _userManager.GetUserAsync(User);

            user.order = newOrder;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.id == id);
        }

        //[Authori]
        //public async Task<IActionResult> CreateNewOrder(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders
        //        .Include(o => o.user)
        //        .FirstOrDefaultAsync(m => m.id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> CreateNewOrder(Guid id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order != null)
        //    {
        //        _context.Orders.Remove(order);
        //    }

        //    await _context.SaveChangesAsync();

        //    Order newOrder = new Order();

        //    EShopApplicationUser? user = await _userManager.GetUserAsync(User);

        //    user.order = newOrder;

        //    _context.Update(user);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
