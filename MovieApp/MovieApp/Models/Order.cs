using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class Order
    {
        [Key]
        public Guid id { get; set; }

        public string? userId {  get; set; } 
        public EShopApplicationUser? user { get; set; }

        public virtual ICollection<TicketInOrder>? ticketInOrders { get; set; }
    }
}
