using ETICARET.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ETICARET.Context
{
    public class DataContext:DbContext
    {
        public DataContext():base("Server=102-26\\SQLDERS;Database=ETICARET;Integrated Security=true")
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}