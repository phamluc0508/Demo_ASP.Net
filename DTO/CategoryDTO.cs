namespace Demo.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductDTO> ProductDTOs { get; set; }
    }
}
