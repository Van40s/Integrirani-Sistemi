using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Interface
{
    public interface ITicketInOrder
    {
        TicketInOrder GetTicketById(Guid id);
        public TicketInOrder DeleteTicket(Guid id);
    }
}
