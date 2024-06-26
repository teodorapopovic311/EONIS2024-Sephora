using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; } 
        public String ProductName { get; set; }  

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public string Brand { get; set; }

        public string ProductType { get; set; }


    }
}