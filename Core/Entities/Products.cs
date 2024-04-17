namespace Core.Entities
{
    public class Products : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductBrand Brand { get; set; }

        public ProductType ProductType { get; set; }

        public int ProductBrandId { get; set; }

        public int ProductTypeId { get; set; }
        public int Available { get; set; }

        public string ImageUrl { get; set; }







    }
}