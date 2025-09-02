using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;

    public class EcommerceAppContext : DbContext
    {
        public EcommerceAppContext (DbContextOptions<EcommerceAppContext> options)
            : base(options)
        {
        }

        public DbSet<Ecommerce.Models.Product> Product { get; set; } = default!;
    }
