using API.Data;
using API.Dto;
using API.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController: BaseController
    {

        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
            
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return  Ok(await _context.Products.Where(a => a.Id == id).FirstOrDefaultAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(ProductDto data)
        {
            var product = new Product
            {
                Name = data.Name,
            };
            await  _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
    }
}