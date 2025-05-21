namespace API_Demo_V2.Services
{
    public interface IMoviesService
    {
        //Signature of all methods in the Movie controller
        Task<IEnumerable<Movie>> GetAll(byte genreId=0);// we can get all if we pass genreId or not 
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
        Movie Update(Movie movie);// No need to be async 
        Movie Delete(Movie movie);// No need to be async 
        
    }
}
