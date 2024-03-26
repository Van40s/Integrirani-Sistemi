namespace MovieApp.Models
{
    public class CreateOrderDTO
    {

        public EShopApplicationUser? user {  get; set; }

        public Guid ticketId { get; set; }

        public int quantity {  get; set; }

        public List<Ticket>? availableTickets {  get; set; }

    }
}
