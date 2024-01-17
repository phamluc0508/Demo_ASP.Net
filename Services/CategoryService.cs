using Demo.Data;
using Demo.DTO;
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
                throw new Exception("category-name-existed");
            }
            Category category = Category.Of(model);
            _db.Categories.Add(category);

            List<Product> products = new List<Product>();
            foreach(ProductDTO productDTO in model.ProductDTOs)
            {
                Product product = Product.Of(productDTO);
                bool isExist = products.Any(p => p.Code == product.Code);
                if(isExist)
                {
                    throw new Exception("product-code-duplicated");
                }
                products.Add(product);
            }
            _db.SaveChanges();
            CategoryDTO categoryDTO = category.ToDTO();

            products.ForEach(p => p.CategoryId = category.Id);
            _db.Products.AddRange(products);
            _db.SaveChanges();

            categoryDTO.ProductDTOs = products.Select(p => p.ToDTO()).ToList();

            return categoryDTO;
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
            var categoryDTOs = _db.Categories.Select(c => c.ToDTO()).ToList();
            foreach (CategoryDTO categoryDTO in categoryDTOs)
            {
                categoryDTO.ProductDTOs = _db.Products.Where(p => p.CategoryId == categoryDTO.Id)
                                                        .Select(p => p.ToDTO()).ToList();
            }
            return categoryDTOs;
        }

        public CategoryDTO GetById(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("not-found-category");
            }
            CategoryDTO categoryDTO = category.ToDTO();
            var products = _db.Products.Where(p => p.CategoryId == category.Id);
            foreach(var product in products)
            {
                categoryDTO.ProductDTOs.Add(product.ToDTO());
            }
            return categoryDTO;
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

            CategoryDTO categoryDTO = Category.ToDTO(category);
            List<ProductDTO> productDTOs = UpdateProduct(id, model.ProductDTOs);
            foreach(var productDTO in productDTOs)
            {
                categoryDTO.ProductDTOs.Add(productDTO);
            }

            _db.SaveChanges();

            return categoryDTO;
        }

        public List<ProductDTO> UpdateProduct(int categoryId, List<ProductDTO> productDTOs)
        {
            List<Product> currProducts = _db.Products.Where(p => p.CategoryId == categoryId).ToList();
            List<Product> updateProduct = new List<Product>();
            List<ProductDTO> updateProductDTO = new List<ProductDTO>();

            foreach(var productDTO in productDTOs)
            {
                productDTO.CategoryId = categoryId;
                Product product = Product.Of(productDTO);
                bool isExist = updateProduct.Any(p => p.Code == product.Code);
                if (isExist)
                {
                    throw new Exception("product-code-duplicated");
                }
                Product curr = currProducts.FirstOrDefault(p => p.Id == product.Id);
                if(curr == null)
                {
                    _db.Products.Add(product);
                    updateProduct.Add(product);
                } else
                {
                    currProducts.Remove(curr);

                    curr.Code = product.Code;
                    curr.Name = product.Name;
                    curr.Description = product.Description;
                    curr.Price = product.Price;
                    curr.CategoryId = categoryId;

                    updateProduct.Add(product);
                }
            }
            if(currProducts.Count > 0)
            {
                _db.Products.RemoveRange(currProducts);
            }
            _db.SaveChanges();
            updateProductDTO = updateProduct.Select(p => p.ToDTO()).ToList();

            return updateProductDTO;
        }
    }
}
