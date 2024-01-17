using Demo.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public string? Description { get; set; }
        public int? CategoryId {  get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        } 

        public static ProductDTO ToDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
            };
        }

        public ProductDTO ToDTO()
        {
            return ToDTO(this);
        }

        public static Product Of(ProductDTO dto)
        {
            return new Product
            {
                Id = dto.Id,
                Code = dto.Code,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
            };
        }
    }
}
