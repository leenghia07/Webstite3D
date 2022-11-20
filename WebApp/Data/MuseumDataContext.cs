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
        public DbSet<ExhibitionRoom> ExhibitionRoom{ get; set; }
        public DbSet<TypeOfArticle> TypeOfArticle { get; set; }
        public DbSet<Article> Article { get; set; }


    }
}
