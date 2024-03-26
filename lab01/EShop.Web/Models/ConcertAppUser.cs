using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EShop.Web.Models
{
    public class ConcertAppUser : IdentityUser
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string address { get; set; }

        public virtual ICollection<ConcertTicket>? tickets { get; set; }
    }
}
