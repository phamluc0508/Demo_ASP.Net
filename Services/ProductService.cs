using Demo.Data;
using Demo.DTO;
using Demo.Models;
using Demo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
    public class ProductService : IProductRepository
    {   
        public readonly MyDbContext _db;
        public ProductService(MyDbContext myDbContext)
        {
            _db = myDbContext;
        }

        public ProductDTO Create(ProductDTO productDTO)
        {
            var codeExist = _db.Products.FirstOrDefault(p => p.Code == productDTO.Code);
            if (codeExist != null)
            {
                throw new Exception("code-existed");
            }
            var product = new Product
            {
                Code = productDTO.Code,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Description = productDTO.Description,
                CategoryId = productDTO.CategoryId,
            };
            _db.Products.Add(product);
            _db.SaveChanges();

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

        public void Delete(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new Exception("not-found-product");
            }
            
            _db.Products.Remove(product);
            _db.SaveChanges();
            
        }

        public List<ProductDTO> GetAll()
        {
            var products = _db.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CategoryId = p.CategoryId
            });

            return products.ToList();
        }

        public List<ProductDTO> GetByCategoryId(int categoryId)
        {
            var products = _db.Products.Where(p => p.CategoryId == categoryId)
                            .Select(p => new ProductDTO
                            {
                                Id = p.Id,
                                Code = p.Code,
                                Name = p.Name,
                                Price = p.Price,
                                Description = p.Description,
                                CategoryId = categoryId
                            });
            return products.ToList();
        }

        public ProductDTO GetById(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new Exception("not-found-product");
            }
            return new ProductDTO
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId
            };
        }

        public List<ProductDTO> Search(string search, Pageable pageable)
        {
            var products = _db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search));
            }
            switch (pageable.Sort)
            {
                case "asc":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                default:
                    products = products.OrderBy(p => p.Id);
                    break;
            }
            var result = PaginatedList<Product>.Search(products, pageable);
            return result.Select(r => new ProductDTO
            {
                Id = r.Id,
                Code = r.Code,
                Name = r.Name,
                Price = r.Price,
                Description = r.Description,
                CategoryId = r.CategoryId
            }).ToList();
        }

        public ProductDTO Update(int id, ProductDTO productDTO)
        {
            var product = _db.Products.FirstOrDefault(p => p.Code == productDTO.Code);
            if (product == null)
            {
                product = _db.Products.FirstOrDefault(x => x.Id == id);
                if(product == null)
                {
                    throw new Exception("not-found");
                }
            } else if (product.Id != id)
            {
                throw new Exception("code-existed");
            }
            product.Code = productDTO.Code;
            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
            product.Description = productDTO.Description;
            product.CategoryId = productDTO.CategoryId;

            _db.SaveChanges();

            return new ProductDTO
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId
            };
        }
    }
}
