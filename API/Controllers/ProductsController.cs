using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // This attribute means less code. Add it to every controller
    [ApiController]  // Also maps parameters passed into the methods below

    // Add a route to get to the controller. All routes start with api
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase // Derives from framework class called controllerbase. Allows http end-points
    {
        // Get access to db context so we can query db
        // Set up dependency injection on constructor
        // See's 'StoreContext' in constructor and gets the service from program .cs
        // Will have access to all the db methods inside controller
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo)
        {
            //_context = context; is the Same as this.context. Convention to use '_'
            _repo = repo;   
        }
        
        //End points
        [HttpGet]
        // Return an action result that can be used as a http response
        // Return a list of products
        // Make async by wrapping ActionResult inside a task
        // Each time a http request is made that will consume a thread on our web server
        // If it takes time for a thread to complete, that thread is now blocked
        // async avoids this. The thread is freed up while that request is processing.
        // Helps concurrent threading

        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }

        // To get a specific thing we use pass a route parameter and then send it to the function as an arg
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) // If a non integer is sent, the [Route] controller will flag an error
        {
            return await _repo.GetProductByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _repo.GetProductBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            return Ok(await _repo.GetProductTypesAsync());
        }
    }
}