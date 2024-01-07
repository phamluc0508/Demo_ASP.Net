namespace Demo.Models
{
    public class Pageable
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 1;
        public string Sort { get; set; } = "asc";

    }
}
