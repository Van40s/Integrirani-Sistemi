using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Models
{
    public class Order : BaseEntity
    {
        public List<TicketInOrder>? ticketInOrder { get; set; }
        public string userId {  get; set; }
        public EShopApplicationUser user {  get; set; }
    }
}
