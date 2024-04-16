using MovieApp.Domain.Models;

namespace MovieApp.Service.Interface
{
    public interface IMovieService
    {
        public List<Movie> GetMovies();
        public Movie GetMovieById(Guid? id);
        public Movie CreateNewMovie(Movie product);
        public Movie UpdateMovie(Movie product);
        public Movie DeleteMovie(Guid id);

    }
}
