using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class TicketInOrder
    {
        [Key]
        public Guid id { get; set; }

        public Guid orderId {  get; set; }
        public Order? order { get; set; }
        public Guid ticketId {  get; set; }
        public Ticket? ticket { get; set; }

        public int quantity {  get; set; }
    }
}
