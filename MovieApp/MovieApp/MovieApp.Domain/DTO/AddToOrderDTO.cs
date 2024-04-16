using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.DTO
{
    public class AddToOrderDTO
    {
        public Ticket? ticket {  get; set; }

        public int quantity {  get; set; }

        public Guid ticketId {  get; set; }
    }
}
