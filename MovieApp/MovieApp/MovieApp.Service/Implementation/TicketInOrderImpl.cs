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
    public class TicketInOrderImpl : ITicketInOrder
    {
        private readonly IRepository<TicketInOrder> _ticketInOrder;

        public TicketInOrderImpl(IRepository<TicketInOrder> ticketInOrder)
        {
            _ticketInOrder = ticketInOrder;
        }

        public TicketInOrder DeleteTicket(Guid id)
        {

            var ticketToDelete = this.GetTicketById(id);
            return _ticketInOrder.Delete(ticketToDelete);
        }

        public TicketInOrder GetTicketById(Guid id)
        {
            return _ticketInOrder.Get(id);
        }
    }
}
