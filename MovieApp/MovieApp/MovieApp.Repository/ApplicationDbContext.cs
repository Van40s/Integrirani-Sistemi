using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;

namespace MovieApp.Repository
{
    public class ApplicationDbContext : IdentityDbContext<EShopApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<TicketInOrder> TicketsInOrder {  get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
