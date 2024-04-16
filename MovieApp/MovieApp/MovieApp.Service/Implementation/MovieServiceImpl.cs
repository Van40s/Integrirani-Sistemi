using MovieApp.Domain.Models;
using MovieApp.Repository.Interface;
using MovieApp.Service.Interface;

namespace MovieApp.Service.Implementation
{
    public class MovieServiceImpl : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieServiceImpl(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }


        public Movie CreateNewMovie(Movie movie)
        {
            return _movieRepository.Insert(movie);
        }

        public Movie DeleteMovie(Guid id)
        {
            var ticketToDelete = this.GetMovieById(id);
            return _movieRepository.Delete(ticketToDelete);
        }

        public Movie GetMovieById(Guid? id)
        {
            return _movieRepository.Get(id);
        }

        public List<Movie> GetMovies()
        {
            return _movieRepository.GetAll().ToList();
        }

        public Movie UpdateMovie(Movie movie)
        {
            return _movieRepository.Update(movie);
        }
    }
}
