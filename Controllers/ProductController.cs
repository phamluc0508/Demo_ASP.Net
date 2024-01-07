using Demo.Data;
using Demo.DTO;
using Demo.Models;
using Demo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductRepository _repository;
        public ProductController(IProductRepository productRepository) 
        {
            _repository = productRepository;
        }

        [HttpPost("/search")]
        public IActionResult Search(string? search, Pageable pageable) 
        {
            try
            {
                var result = _repository.Search(search, pageable);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);    
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _repository.GetById(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/get-by-categoryId")]
        public IActionResult GetByCategoryId(int categoryId) 
        {
            try
            {
                var data = _repository.GetByCategoryId(categoryId);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(ProductDTO model)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _repository.Create(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, ProductDTO model)
        {
            try
            {
                return Ok(_repository.Update(id, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
