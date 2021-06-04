using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopBridgeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeApplication.Data
{
    public class ShopBridgeDbContext : IdentityDbContext
    {

        public ShopBridgeDbContext(DbContextOptions<ShopBridgeDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
    }
}
