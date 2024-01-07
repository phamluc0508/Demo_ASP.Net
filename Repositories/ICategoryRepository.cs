using Demo.DTO;
using Demo.Models;

namespace Demo.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetAll();
        CategoryDTO GetById(int id);
        CategoryDTO Create(CategoryDTO model);
        CategoryDTO Update(int id, CategoryDTO model);
        void Delete(int id);
    }
}
