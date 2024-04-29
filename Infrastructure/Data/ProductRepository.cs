using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(Products product)
        {
            _context.Set<Products>().Add(product);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<Products>> GetProductsAsync()
        {
            

            return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.Brand)
            .ToListAsync();
        }

        public async Task<Products> GetProductsByIdAsync(int id)
        {
            return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public void Update(Products product)
        {
            _context.Set<Products>().Attach(product);
            _context.Entry(product).State = EntityState.Modified;
        
        }

        public void Delete(Products product)
        {
            _context.Set<Products>().Remove(product);
        }
    }
}