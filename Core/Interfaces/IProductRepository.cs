using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Products> GetProductsByIdAsync(int id);
        Task<IReadOnlyList<Products>> GetProductsAsync();

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

        void Add(Products product);

        void Update(Products product);

        void Delete(Products product);
    }
}