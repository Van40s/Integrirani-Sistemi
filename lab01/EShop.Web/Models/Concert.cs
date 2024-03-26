using System.ComponentModel.DataAnnotations;

namespace EShop.Web.Models
{
    public class Concert
    {
        [Key]
        public Guid id { get; set; }
        public string concertName { get; set; }
        public DateTime dateAndTime { get; set; }
        public string location { get; set; }
        public string pictureURI { get; set; }
        public int concertPrice { get; set; }
        public virtual List<ConcertTicket>? concertTickets { get; set; }

    }
}
