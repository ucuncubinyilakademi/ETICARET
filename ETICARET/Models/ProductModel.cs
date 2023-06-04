using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETICARET.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}