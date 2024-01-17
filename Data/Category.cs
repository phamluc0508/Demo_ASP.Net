using Demo.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Data
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public static CategoryDTO ToDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                ProductDTOs = new List<ProductDTO>(),
            };
        }

        public CategoryDTO ToDTO()
        {
            return ToDTO(this);
        }

        public static Category Of(CategoryDTO dto)
        {
            return new Category
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
