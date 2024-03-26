namespace EShop.Web.Models
{
    public class ConcertTicketDTO
    {
        public List<Concert>? concerts { get; set; } // se cuvat site koncerti

        public Guid concertID { get; set; } // ID-to na izbraniot koncert

        public int numberOfPeople { get; set; } // za kolku lugje zimash karti
    }
}
