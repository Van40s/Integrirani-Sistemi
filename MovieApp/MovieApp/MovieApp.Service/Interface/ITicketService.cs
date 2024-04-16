using MovieApp.Domain.Models;

namespace MovieApp.Service.Interface
{
    public interface ITicketService
    {
        public IEnumerable<Ticket> GetTickets();
        public Ticket GetTicketById(Guid? id);
        public Ticket CreateNewTicket(string userId, Ticket product);
        public Ticket UpdateTicket(Ticket product);
        public Ticket DeleteTicket(Guid id);

    }
}
