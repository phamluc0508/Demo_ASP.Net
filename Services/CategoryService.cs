using Demo.Data;
using Demo.DTO;
using Demo.Models;
using Demo.Repositories;

namespace Demo.Services
{
    public class CategoryService : ICategoryRepository
    {

        public readonly MyDbContext _db;
        public CategoryService(MyDbContext myDbContext)
        {
            _db = myDbContext;
        }

        public CategoryDTO Create(CategoryDTO model)
        {
            var codeExist = _db.Categories.FirstOrDefault(p => p.Name == model.Name);
            if (codeExist != null)
            {
                throw new Exception("name-existed");
            }
            var category = new Category
            {
                Name = model.Name
            };
            _db.Categories.Add(category);
            _db.SaveChanges();
            return new CategoryDTO 
            { 
                Id = category.Id, 
                Name = category.Name 
            };
        }

        public void Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("not-found-category");
            }
            var used = _db.Products.Any(p => p.CategoryId == id);
            if (used)
            {
                throw new Exception("category-using");
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }

        public List<CategoryDTO> GetAll()
        {
            var categories = _db.Categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
            });
            return categories.ToList();
        }

        public CategoryDTO GetById(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("not-found-category");
            }
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public CategoryDTO Update(int id, CategoryDTO model)
        {
            var category = _db.Categories.FirstOrDefault(p => p.Name == model.Name);
            if (category == null)
            {
                category = _db.Categories.FirstOrDefault(x => x.Id == id);
                if (category == null)
                {
                    throw new Exception("not-found");
                }
            }
            else if (category.Id != id)
            {
                throw new Exception("name-existed");
            }
            category.Name = model.Name;

            _db.SaveChanges();

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
