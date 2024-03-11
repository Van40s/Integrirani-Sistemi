namespace EShop.Web.Models
{
    public class ConcertTicketDTO
    {
        public List<Concert>? concerts { get; set; }

        public Guid concertID { get; set; }

        public int numberOfPeople { get; set; }
    }
}
