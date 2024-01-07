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
    }
}
