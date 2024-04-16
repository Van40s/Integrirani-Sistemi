using MovieApp.Domain.Models;
using MovieApp.Repository.Interface;
using MovieApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Implementation
{
    public class TicketServiceImpl : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IUserRepository _userRepository;

        public TicketServiceImpl(IRepository<Ticket> ticketRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }



        public Ticket CreateNewTicket(string userId, Ticket ticket)
        {
            var createdBy = _userRepository.Get(userId);
            ticket.CreatedBy = createdBy;
            return _ticketRepository.Insert(ticket);
        }

        public Ticket DeleteTicket(Guid id)
        {
            var ticketToDelete = this.GetTicketById(id);
            return _ticketRepository.Delete(ticketToDelete);
        }

        public Ticket GetTicketById(Guid? id)
        {
            return _ticketRepository.Get(id);
        }



        public IEnumerable<Ticket> GetTickets()
        {
            return _ticketRepository.GetAll();
        }

        public Ticket UpdateTicket(Ticket product)
        {
            return _ticketRepository.Update(product);
        }
    }
}
