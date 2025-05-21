namespace API_Demo_V2.Services
{
    public interface IGenresService
    {
        //Signature of all methods in the Genre controller
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(byte id);
        Task<Genre> Add(Genre genre);
        Genre Update(Genre genre);// No need to be async 
        Genre Delete(Genre genre);// No need to be async 
        Task<bool> IsvalidGenre(byte id);

    }
}
