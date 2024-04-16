using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.Domain.DTO;
using MovieApp.Domain.Models;
using MovieApp.Service.Interface;
using System.Net.Sockets;
using System.Security.Claims;

namespace MovieApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ITicketService _ticketService;
        private readonly ITicketInOrder _ticketInOrder;

        public OrdersController(IOrderService orderService, ITicketService ticketService, ITicketInOrder ticketInOrder)
        {
            _orderService = orderService;
            _ticketService = ticketService;
            _ticketInOrder = ticketInOrder;
        }

        public IActionResult Index()
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            
            return View(_orderService.GetOrder(loggedInUser));
        }

        public IActionResult AddToCart(Guid id)
        {
            Ticket ticket = _ticketService.GetTicketById(id);
            AddToOrderDTO dto = new AddToOrderDTO();
            dto.ticket = ticket;
            dto.ticketId = id;
            return View(dto);
        }

        [HttpPost]
        public IActionResult AddToCart(AddToOrderDTO dto)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
                _orderService.AddToCart(loggedInUser, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public IActionResult DeleteFromCart(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketInOrder.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _ticketInOrder.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PayCart()
        {
            return View();
        }

        [HttpPost, ActionName("Pay")]
        [ValidateAntiForgeryToken]
        public IActionResult PayConfirmed()
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _orderService.payOrder(loggedInUser);
            return RedirectToAction(nameof(Index));
        }
    }
}
