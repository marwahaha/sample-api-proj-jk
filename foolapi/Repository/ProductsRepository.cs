using System.Collections.Generic;
using System.Linq;
using foolapi.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// currently unused - for later use
namespace foolapi.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        DataContext db;
        public ProductsRepository(DataContext dataContext)
        {
            db = dataContext;
        }      

        public async Task Add(Product item)
        {            
            await db.Product.AddAsync(item);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await db.Product.ToListAsync();
        }

        public bool CheckValidUserKey(string reqkey)
        {
            var userkeyList = new List<string>
            {
                "28236d8ec201df516d0f6472d516d72d",
                "38236d8ec201df516d0f6472d516d72c",
                "48236d8ec201df516d0f6472d516d72b"
            };

            if (userkeyList.Contains(reqkey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Product> Find(int id)
        {
            return await db.Product
                .Where(e => e.ProductId.Equals(id))
                .SingleOrDefaultAsync();
        }       

        public async Task Remove(int id)
        {
            var itemToRemove = await db.Product.SingleOrDefaultAsync(r => r.ProductId == id);
            if (itemToRemove != null)
            {
                db.Product.Remove(itemToRemove);
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(Product item)
        {            
            var itemToUpdate = await db.Product.SingleOrDefaultAsync(r => r.ProductId == item.ProductId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Name = item.Name;
                await db.SaveChangesAsync();
            }
        }

    }
}