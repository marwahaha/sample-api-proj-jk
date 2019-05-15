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
    [Route("api/offers")]
    public class OfferController : Controller
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
            string[] values = new string[] { "Offer1", "Offer2" };
            return values;
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetOfferById(int id)
        {

            Offer Offer = await db.Offer.FindAsync(id);

            if (Offer == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Offer);
            }
        }

        // Products is Parent table of Offers
        // TODO: add filter on multiple fields
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<Offer>>> GetOffers([FromQuery]int product = 0, [FromQuery]string numterms = "")
        {
            List<Offer> items = new List<Offer>();
            String notFoundMessage = "";

            //does not currently enable searching on multiple fields
            //product is the alias for productid
            if (product != 0)
            {
                items = await FilterByProduct(product);
                notFoundMessage = $"No Offers for product={product} found";
            }
            else if (numterms != "")
            {
                items = await FilterByTerm(numterms);
                notFoundMessage = $"No Offers for number of terms={numterms} found";
            }
            else
            {
                items = await db.Offer.ToListAsync();
                notFoundMessage = "No Offers found";
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

        public async Task<List<Offer>> FilterByProduct(int id)
        {
            return await db.Offer
            .Where(e => e.ProductId.Equals(id))
            .ToListAsync();
        }

        public async Task<List<Offer>> FilterByTerm(string numberOferms)
        {
            return await db.Offer
            .Where(e => e.NumberOfTerms.Equals(numberOferms))
            .ToListAsync();
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Offer Offer)
        {

            db.Offer.Add(Offer);
            await db.SaveChangesAsync();
            return Ok(Offer);
        }


        // TODO: DX improvements - Offerid should not be provided
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Offer Offer)
        {

            //TODO: check if ID exists without using an instance of Offer
            // That generates the EF error for tracking objs of same id

            /* Boolean isValidId = false; //await IdExists(id);
            if(!isValidId) {
                return NotFound($"A Offer with id={id} is not found");
            }*/

            if (id != Offer.OfferId)
            {
                return BadRequest($"The Offer Id of {Offer.OfferId} doesn't match the endpoint of {id}");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(Offer);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("DbUpdateException", "Unable to save changes to database");
                }
            }
            return Ok(Offer);
        }

        // TODO: 1/6 Integration tests fail because DX for this one needs work
        // TODO: soft delete, cascading deletes
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var existingItem = await db.Offer.FindAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            db.Offer.Remove(existingItem);
            db.SaveChanges();
            return Ok(new Offer());
        }

    }
}
