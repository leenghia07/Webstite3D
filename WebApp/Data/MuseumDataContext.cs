using Microsoft.EntityFrameworkCore;
using WebApp.Models;
namespace WebApp.Data
{
    public class MuseumDataContext : DbContext
    {
        public MuseumDataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Artifact> Aritifact { get; set; }
        public DbSet<TypeOfArtifact> TypeOfArtifact { get; set; }
        public DbSet<Museum> Museum { get; set; }
    }
}
