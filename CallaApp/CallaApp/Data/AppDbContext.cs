using CallaApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CallaApp.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<Decor> Decors { get; set; }
        public DbSet<MiniImage> MiniImages { get; set; }
        public DbSet<WebSiteSocial> WebSiteSocials { get; set; }
        public DbSet<HeaderBackground> HeaderBackgrounds { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Slider>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Banner>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Advertising>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Decor>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<HeaderBackground>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Settings>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<MiniImage>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<WebSiteSocial>().HasQueryFilter(p => !p.SoftDelete);



        }
    }
}
