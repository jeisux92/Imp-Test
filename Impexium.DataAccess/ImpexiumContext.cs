using Impexium.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Impexium.DataAccess
{
    public class ImpexiumContext : IdentityDbContext<IdentityUser>
    {

        public ImpexiumContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}
