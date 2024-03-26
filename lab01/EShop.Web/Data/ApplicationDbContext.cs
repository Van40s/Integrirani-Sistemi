using EShop.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ConcertAppUser>
    {
        public DbSet<Concert> concerts {  get; set; }
        public DbSet<ConcertTicket> tickets {  get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
