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
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductSize> ProductSize { get; set; }
        public DbSet<ProductTag> ProductTag { get; set; }
        public DbSet<ProductColor> ProductColor { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<MiniImage> MiniImages { get; set; }
        public DbSet<WebSiteSocial> WebSiteSocials { get; set; }
        public DbSet<HeaderBackground> HeaderBackgrounds { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogImage> BlogImage { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistProduct> WishlistProducts { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }

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
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Color>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Brand>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Size>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Tag>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<About>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Team>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Position>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Blog>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<BlogComment>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<BlogImage>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Author>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Subscribe>().HasQueryFilter(p => !p.SoftDelete);
        }
    }
}
