using Demo.DTO;
using Demo.Models;

namespace Demo.Repositories
{
    public interface IProductRepository
    {
        List<ProductDTO> Search(string search, Pageable pageable);
        List<ProductDTO> GetAll();
        ProductDTO GetById(int id);
        List<ProductDTO> GetByCategoryId(int categoryId);
        ProductDTO Create(ProductDTO model);
        ProductDTO Update(int id, ProductDTO model);
        void Delete(int id);
    }
}
