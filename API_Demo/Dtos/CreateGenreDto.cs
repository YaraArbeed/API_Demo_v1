namespace API_Demo_V2.Dtos
{
    public class CreateGenreDto
    {
        [MaxLength(100)]
        public  string Name { get; set; }
    }
}
