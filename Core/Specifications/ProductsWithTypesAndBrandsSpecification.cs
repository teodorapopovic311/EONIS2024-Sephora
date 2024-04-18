using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Products>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Brand);

        }

        public ProductsWithTypesAndBrandsSpecification(int id)
         : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Brand);
        }
    }
}