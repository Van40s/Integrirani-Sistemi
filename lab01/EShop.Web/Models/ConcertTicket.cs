using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Web.Models
{
    public class ConcertTicket
    {
        [Key]
        public Guid Id { get; set; }

        public int numberOfPeople { get; set; }

        public virtual Concert concert {  get; set; }

        public virtual ConcertAppUser forUser { get; set; }
    }
}
