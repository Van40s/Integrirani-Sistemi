using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Models
{
    public class TicketInOrder : BaseEntity
    {
        public Guid ticketId {  get; set; }

        public Ticket? ticket { get; set; }

        public Guid orderId {  get; set; }
        public Order? order {  get; set; }

        public int quantity {  get; set; }
    }
}
