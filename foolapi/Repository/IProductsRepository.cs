using foolapi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

// currently unused - for later use
namespace foolapi.Repository
{
    public interface IProductsRepository
    {
        Task Add(Product item);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Find(int id);
        Task Remove(int id);
        Task Update(Product item);

        bool CheckValidUserKey(string reqkey);
    }
}
