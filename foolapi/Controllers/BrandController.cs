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
    [Route("api/brands")]
    public class BrandController : Controller
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
            string[] values = new string[] { "Brand1", "Brand2" };
            return values;
        }

        // TODO: create a GetBrandById function to work with this one
        [Produces("application/json")]
        [HttpGet("{code}")]
        public async Task<ActionResult<Brand>> GetBrandByCode(string code)
        {

            Brand Brand = await FindByBrand(code);

            if (Brand == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Brand);
            }
        }

        public async Task<Brand> FindByBrand(string brand)
        {
            return await db.Brand
            .Where(e => e.BrandCode.Equals(brand))
            .SingleOrDefaultAsync();
        }

        // No parent table
        // TODO: add filter on child fields
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            List<Brand> items = new List<Brand>();
            String notFoundMessage = "";

            items = await db.Brand.ToListAsync();
            notFoundMessage = "No Brands found";


            if ((items == null) || (items.Count < 1))
            {
                return NotFound(notFoundMessage);
            }
            else
            {
                return Ok(items);
            }

        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Brand Brand)
        {

            db.Brand.Add(Brand);
            await db.SaveChangesAsync();
            return Ok(Brand);
        }


        // TODO: DX improvements - Brandid should not be provided
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string code, [FromBody] Brand Brand)
        {

            //TODO: check if ID exists without using an instance of Brand
            // That generates the EF error for tracking objs of same id

            /* Boolean isValidId = false; //await IdExists(id);
            if(!isValidId) {
                return NotFound($"A Brand with id={id} is not found");
            }*/

            if (code != Brand.BrandCode)
            {
                return BadRequest($"The Brand Id of {Brand.BrandCode} doesn't match the endpoint of {code}");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(Brand);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("DbUpdateException", "Unable to save changes to database");
                }
            }
            return Ok(Brand);
        }

        // TODO: 1/6 Integration tests fail because DX for this one needs work
        // TODO: soft delete, cascading deletes
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var existingItem = await db.Brand.FindAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            db.Brand.Remove(existingItem);
            db.SaveChanges();
            return Ok(new Brand());
        }

    }
}
