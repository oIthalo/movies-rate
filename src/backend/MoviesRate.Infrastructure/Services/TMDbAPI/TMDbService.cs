using Azure;
using MoviesRate.Domain.Entities;
using MoviesRate.Domain.Interfaces;
using MoviesRate.Domain.Repositories.Reviews;

namespace MoviesRate.Infrastructure.Services.TMDbAPI;

public class TMDbService : ITMDbService
{
    private readonly ITMDbApi _api;
    private readonly IReadReviewRepository _readReviewsRepository;

    public TMDbService(
        ITMDbApi api,
        IReadReviewRepository readReviewsRepository)
    {
        _api = api;
        _readReviewsRepository = readReviewsRepository;
    }

    public async Task<MoviesList> GetAllMoviesToDashboard(int page)
    {
        // getting movies
        var response = await _api.GetAllMoviesToDashboard(page);
        var randomMovies = response.Movies.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

        // getting genres
        var genres = await _api.GetGenres();

        foreach (var movie in randomMovies)
        {
            // adding genres
            var movieGenres = genres.Genres.Where(x => movie!.GenreIds.Contains(x.Id)).ToList();
            movie!.Genres = movieGenres;

            // getting movie reviews
            var movieReviews = await _readReviewsRepository.GetReviewsByMovieId(movie.Id);

            // adding note average
            var ratings = movieReviews!.Select(x => x.Rating).ToList();
            movie.NoteAverage = ratings.Count != 0 ? ratings.Average() : 0;

            // adding comments
            var comments = movieReviews!.Select(x => x.Comments).ToList();
            movie.Comments = comments.Select(commentText => new Comment { Text = commentText }).ToList();
        }

        return new MoviesList()
        {
            Page = response.Page,
            Movies = randomMovies ?? [],
            TotalPages = response.TotalPages,
            TotalResults = response.TotalResults,
        };
    }

    public async Task<Movie> GetRandomRecommendedMovieToDashboard()
    {
        // getting movies
        var response = await _api.GetRandomRecommendedMovieToDashboard();
        var movie = response.Movies.OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();

        // getting genres
        var genres = await _api.GetGenres();

        // adding movie genres
        var movieGenres = genres.Genres.Where(x => movie!.GenreIds.Contains(x.Id)).ToList();
        movie!.Genres = movieGenres;

        // getting movie reviews
        var movieReviews = await _readReviewsRepository.GetReviewsByMovieId(movie.Id);

        // adding movie reviews
        var ratings = movieReviews!.Select(x => x.Rating).ToList();
        movie.NoteAverage = ratings.Count != 0 ? ratings.Average() : 0;

        // adding comments
        var comments = movieReviews!.Select(x => x.Comments).ToList();
        movie.Comments = comments.Select(commentText => new Comment { Text = commentText }).ToList();

        return movie!;
    }

    public async Task<MoviesList> Get10RandomTopRatedMovies()
    {
        // getting movies
        var response = await _api.GetTopRated();
        var randomMovies = response.Movies.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

        // getting genres
        var genres = await _api.GetGenres();

        foreach (var movie in randomMovies)
        {
            // adding movie genres
            var movieGenres = genres.Genres.Where(x => movie!.GenreIds.Contains(x.Id)).ToList();
            movie!.Genres = movieGenres;

            // getting movie reviews
            var movieReviews = await _readReviewsRepository.GetReviewsByMovieId(movie.Id);

            // adding movie reviews
            var ratings = movieReviews!.Select(x => x.Rating).ToList();
            movie.NoteAverage = ratings.Count != 0 ? ratings.Average() : 0;

            // adding comments
            var comments = movieReviews!.Select(x => x.Comments).ToList();
            movie.Comments = comments.Select(commentText => new Comment { Text = commentText }).ToList();
        }

        return new MoviesList()
        {
            Page = response.Page,
            Movies = randomMovies ?? [],
            TotalPages = response.TotalPages,
            TotalResults = response.TotalResults,
        };
    }

    public async Task<MoviesList> Get10RandomPopularMovies()
    {
        // getting movies
        var response = await _api.GetPopular();
        var randomMovies = response.Movies.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

        // getting genres
        var genres = await _api.GetGenres();

        foreach (var movie in randomMovies)
        {
            // adding genres
            var movieGenres = genres.Genres.Where(x => movie!.GenreIds.Contains(x.Id)).ToList();
            movie!.Genres = movieGenres;

            // getting movie reviews
            var movieReviews = await _readReviewsRepository.GetReviewsByMovieId(movie.Id);

            //adding note average
            var ratings = movieReviews!.Select(x => x.Rating).ToList();
            movie.NoteAverage = ratings.Count != 0 ? ratings.Average() : 0;

            // adding comments
            var comments = movieReviews!.Select(x => x.Comments).ToList();
            movie.Comments = comments.Select(commentText => new Comment { Text = commentText }).ToList();
        }

        return new MoviesList()
        {
            Page = response.Page,
            Movies = randomMovies ?? [],
            TotalPages = response.TotalPages,
            TotalResults = response.TotalResults,
        };
    }

    public async Task<Movie> GetMovieById(int id)
    {
        // getting movie
        var movie = await _api.GetMovieById(id);

        // getting movie reviews
        var movieReviews = await _readReviewsRepository.GetReviewsByMovieId(movie.Id);

        // adding note average
        var ratings = movieReviews!.Select(x => x.Rating).ToList();
        movie.NoteAverage = ratings.Count != 0 ? ratings.Average() : 0;

        // adding comments
        var comments = movieReviews!.Select(x => x.Comments).ToList();
        movie.Comments = comments.Select(commentText => new Comment { Text = commentText }).ToList();

        return movie;
    }

    public async Task<MoviesList> GetMoviesBySearch(string query)
    {
        // getting movies
        var response = await _api.GetMoviesBySearch(query);
        var movies = response.Movies.ToList();

        // getting genres
        var genres = await _api.GetGenres();

        foreach (var movie in movies)
        {
            // adding genres
            var movieGenres = genres.Genres.Where(x => movie!.GenreIds.Contains(x.Id)).ToList();
            movie!.Genres = movieGenres;

            // getting movie reviews
            var movieReviews = await _readReviewsRepository.GetReviewsByMovieId(movie.Id);

            // adding note average
            var ratings = movieReviews!.Select(x => x.Rating).ToList();
            movie.NoteAverage = ratings.Count != 0 ? ratings.Average() : 0;

            // adding comments
            var comments = movieReviews!.Select(x => x.Comments).ToList();
            movie.Comments = comments.Select(commentText => new Comment { Text = commentText }).ToList();
        }

        return new MoviesList()
        {
            Page = response.Page,
            Movies = movies ?? [],
            TotalPages = response.TotalPages,
            TotalResults = response.TotalResults,
        };
    }
}