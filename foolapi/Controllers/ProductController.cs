using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using foolapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace foolapi.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        private DataContext db = new DataContext();

        // baseline operation for integration tests, no reliance on external data
        [HttpGet("nondata")]
        public ActionResult<IEnumerable<string>> GetNonData()
        {
            string[] values = GetNonDataValues();
            return Ok(values);
        }

        public string[] GetNonDataValues()
        {
            string[] values = new string[] { "product1", "product2" };
            return values;
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {

            Product product = await db.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        // Brands is Parent table of Products
        // TODO: add filter on multiple fields
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts([FromQuery]string brand = "", [FromQuery]string term = "")
        {
            List<Product> items = new List<Product>();
            String notFoundMessage = "";

            //does not currently enable searching on multiple fields
            if (brand != "")
            {
                items = await FilterByBrand(brand);
                notFoundMessage = $"No products for brand={brand} found";
            }
            else if (term != "")
            {
                items = await FilterByTerm(term);
                notFoundMessage = $"No products for term={term} found";
            }
            else
            {
                items = await db.Product.ToListAsync();
                notFoundMessage = "No products found";
            }

            if ((items == null) || (items.Count < 1))
            {
                return NotFound(notFoundMessage);
            }
            else
            {
                return Ok(items);
            }

        }

        public async Task<List<Product>> FilterByBrand(string brand)
        {
            return await db.Product
            .Where(e => e.Brand.Equals(brand))
            .ToListAsync();
        }

        public async Task<List<Product>> FilterByTerm(string term)
        {
            return await db.Product
            /* starts with enables using m,month,monthly and a,annual,annually */
            .Where(e => e.Term.StartsWith(term))
            .ToListAsync();
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {

            db.Product.Add(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }


        // TODO: DX improvements - productid should not be provided
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Product product)
        {

            //TODO: check if ID exists without using an instance of product
            // That generates the EF error for tracking objs of same id

            /* Boolean isValidId = false; //await IdExists(id);
            if(!isValidId) {
                return NotFound($"A product with id={id} is not found");
            }*/

            if (id != product.ProductId)
            {
                return BadRequest($"The Product Id of {product.ProductId} doesn't match the endpoint of {id}");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(product);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("DbUpdateException", "Unable to save changes to database");
                }
            }
            return Ok(product);
        }

        // TODO: 1/6 Integration tests fail because DX for this one needs work
        // TODO: soft delete, cascading deletes
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var existingItem = await db.Product.FindAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            db.Product.Remove(existingItem);
            db.SaveChanges();
            return Ok(new Product());
        }

    }
}
