
namespace API_Demo_V2.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// EF-Core will use identity for byte
        public byte Id { get; set; }// Max is 255

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
