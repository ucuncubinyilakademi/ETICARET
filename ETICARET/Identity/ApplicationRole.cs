using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETICARET.Identity
{
    public class ApplicationRole:IdentityRole
    {
        public string Description { get; set; }

        // Her class tanımında görünmeyen ama gizli olarak tanımlı boş bir constructor metot vardır.
        public ApplicationRole()
        {

        }
        public ApplicationRole(string rolename, string description)
        {
            this.Description = description;
        }
    }
}