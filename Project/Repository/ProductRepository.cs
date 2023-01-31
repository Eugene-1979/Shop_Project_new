using Microsoft.EntityFrameworkCore;
using Shop_Project.Db;
using Shop_Project.Models;
using System.Reflection.Metadata;

namespace Shop_Project.Repository
{
    public class ProductRepository : IReposytory<Product>
    {
        internal readonly AppDbContent _context;

        public ProductRepository(AppDbContent context)
        {
            _context = context;
        }




        async public Task ModelAddAsync(Product model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        async public Task<IEnumerable<Product>> ModelAllAsync() => await _context.Products.Include(p => p.Category).ToListAsync();


        async public Task ModelDeleteAsync(Product model)
        {
           
            if (model != null)
            {
                _context.Products.Remove(model);
            }

            await _context.SaveChangesAsync();
        }

        public bool ModelExist(int id) => (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
      

        async public Task<Product> ModelFirstofDefaultAsync(int? id) => await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);


        async public Task<Product> ModelIdAsync(int? id) => await _context.Products.FindAsync(id);


        async public Task ModelUpdateAsync(Product model)
        {
            _context.ChangeTracker.Clear();
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public (bool, string) CheckModel(Product model)
        {

            if (model.Sale < ShopRepository._minSale || model.Sale > ShopRepository._maxSale) return (false, "incorrect sale");
            if (_context.Products.ToList().Contains(model)) return (false, "Product is Exist");


            return (true, "ok");

        }

















        public Product AddBlog(string name)
            {
            var blog = _context.Products.Add(new Product { Name = name});
            _context.SaveChanges();

            return blog.Entity;
            }




        public List<Product> GetAllProducts()
        {
            var query = from b in _context.Products
                        orderby b.Name
                        select b;

            return query.ToList();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var query = from b in _context.Products
                        orderby b.Name
                        select b;

            return await query.ToListAsync();
        }


   




        }
}
