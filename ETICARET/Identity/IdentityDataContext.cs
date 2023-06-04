using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETICARET.Identity
{
    public class IdentityDataContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext(): base("Server=102-26\\SQLDERS;Database=ETICARET;Integrated Security=true")
        {

        }

        public System.Data.Entity.DbSet<ETICARET.Models.RegisterModel> RegisterModels { get; set; }
    }
}