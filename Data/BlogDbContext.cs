using Microsoft.EntityFrameworkCore;
using BlogApp.Models;

namespace BlogApp.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         
            modelBuilder.Entity<BlogPost>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.BlogPost)
                .HasForeignKey(c => c.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Title = "Welcome to your new Blazor Blog!",
                    Summary = "Discover how easy and powerful it is to build modern web applications using Blazor and .NET 10.",
                    Content = "### Welcome!\n\nThis is your first blog post. Blazor is a powerful framework from Microsoft that lets you build interactive web UIs using C# instead of JavaScript.\n\n#### Key Features of this application:\n1. **Blazor Interactive Server**: Seamless real-time UI updates powered by SignalR.\n2. **SQL Server Integration**: Clean, relational database persistence using Entity Framework Core.\n3. **Modern CSS & Bootstrap**: Sleek styling with minimal clean styles.\n4. **Admin Dashboard**: Manage your posts, create new articles, and edit existing content.\n\nFeel free to log in as an administrator to write new posts, edit this post, or delete it. Add comments below to test the interactivity!",
                    CreatedAt = new DateTime(2026, 5, 30, 12, 0, 0, DateTimeKind.Utc),
                    Category = "Tech",
                    IsPublished = true
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "Getting Started with .NET 10",
                    Summary = "A brief look into the latest features and performance enhancements in .NET 10.",
                    Content = "### The Future is Here with .NET 10\n\n.NET 10 continues the incredible journey of providing the fastest, most unified platform for modern development.\n\n#### Highlights:\n- **Enhanced C# Features**: Writing code is more intuitive and requires less boilerplate.\n- **Performance**: Significant reductions in memory allocations and faster JIT compiler throughput.\n- **Blazor Enhancements**: Better state management and hybrid capabilities out of the box.\n\nEnjoy writing clean, zero-error code in your brand-new application!",
                    CreatedAt = new DateTime(2026, 5, 30, 10, 0, 0, DateTimeKind.Utc),
                    Category = ".NET",
                    IsPublished = true
                }
            );
        }
    }
}
