using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class Ticket : BaseEntity
    {
        [Required]
        public double Price { get; set; }
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }
        public virtual EShopApplicationUser? CreatedBy { get; set; }

        public virtual ICollection<TicketInOrder>? TicketInOrders { get; set; }

    }
}
