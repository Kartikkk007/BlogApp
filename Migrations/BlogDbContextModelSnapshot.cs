using System;
using BlogApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogApp.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    partial class BlogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlogApp.Models.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BlogPosts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Tech",
                            Content = "### Welcome!\n\nThis is your first blog post. Blazor is a powerful framework from Microsoft that lets you build interactive web UIs using C# instead of JavaScript.\n\n#### Key Features of this application:\n1. **Blazor Interactive Server**: Seamless real-time UI updates powered by SignalR.\n2. **SQL Server Integration**: Clean, relational database persistence using Entity Framework Core.\n3. **Modern CSS & Bootstrap**: Sleek styling with minimal clean styles.\n4. **Admin Dashboard**: Manage your posts, create new articles, and edit existing content.\n\nFeel free to log in as an administrator to write new posts, edit this post, or delete it. Add comments below to test the interactivity!",
                            CreatedAt = new DateTime(2026, 5, 30, 12, 0, 0, 0, DateTimeKind.Utc),
                            IsPublished = true,
                            Summary = "Discover how easy and powerful it is to build modern web applications using Blazor and .NET 10.",
                            Title = "Welcome to your new Blazor Blog!"
                        },
                        new
                        {
                            Id = 2,
                            Category = ".NET",
                            Content = "### The Future is Here with .NET 10\n\n.NET 10 continues the incredible journey of providing the fastest, most unified platform for modern development.\n\n#### Highlights:\n- **Enhanced C# Features**: Writing code is more intuitive and requires less boilerplate.\n- **Performance**: Significant reductions in memory allocations and faster JIT compiler throughput.\n- **Blazor Enhancements**: Better state management and hybrid capabilities out of the box.\n\nEnjoy writing clean, zero-error code in your brand-new application!",
                            CreatedAt = new DateTime(2026, 5, 30, 10, 0, 0, 0, DateTimeKind.Utc),
                            IsPublished = true,
                            Summary = "A brief look into the latest features and performance enhancements in .NET 10.",
                            Title = "Getting Started with .NET 10"
                        });
                });

            modelBuilder.Entity("BlogApp.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BlogApp.Models.Comment", b =>
                {
                    b.HasOne("BlogApp.Models.BlogPost", "BlogPost")
                        .WithMany("Comments")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlogPost");
                });

            modelBuilder.Entity("BlogApp.Models.BlogPost", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
