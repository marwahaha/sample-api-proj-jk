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

        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var items = await db.Product.ToListAsync();
            if (items == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(items);
            }
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


        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("If provided, ProductId must match endpoint");
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

        // TODO: Integration tests fail because DX for this one needs work
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
            return Ok(null);
        }

    }
}
