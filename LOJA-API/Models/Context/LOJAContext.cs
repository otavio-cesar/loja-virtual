using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Models.Context
{
    public class LOJAContext : DbContext
    {
        public LOJAContext(DbContextOptions<LOJAContext> options)
                 : base(options)
        { }

        public DbSet<Produto> Produto { get; set; }
    }
}
