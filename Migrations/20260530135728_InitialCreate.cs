using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 

namespace BlogApp.Migrations
{
    
    public partial class InitialCreate : Migration
    {
    
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "IsPublished", "Summary", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Tech", "### Welcome!\n\nThis is your first blog post. Blazor is a powerful framework from Microsoft that lets you build interactive web UIs using C# instead of JavaScript.\n\n#### Key Features of this application:\n1. **Blazor Interactive Server**: Seamless real-time UI updates powered by SignalR.\n2. **SQL Server Integration**: Clean, relational database persistence using Entity Framework Core.\n3. **Modern CSS & Bootstrap**: Sleek styling with minimal clean styles.\n4. **Admin Dashboard**: Manage your posts, create new articles, and edit existing content.\n\nFeel free to log in as an administrator to write new posts, edit this post, or delete it. Add comments below to test the interactivity!", new DateTime(2026, 5, 30, 12, 0, 0, 0, DateTimeKind.Utc), true, "Discover how easy and powerful it is to build modern web applications using Blazor and .NET 10.", "Welcome to your new Blazor Blog!", null },
                    { 2, ".NET", "### The Future is Here with .NET 10\n\n.NET 10 continues the incredible journey of providing the fastest, most unified platform for modern development.\n\n#### Highlights:\n- **Enhanced C# Features**: Writing code is more intuitive and requires less boilerplate.\n- **Performance**: Significant reductions in memory allocations and faster JIT compiler throughput.\n- **Blazor Enhancements**: Better state management and hybrid capabilities out of the box.\n\nEnjoy writing clean, zero-error code in your brand-new application!", new DateTime(2026, 5, 30, 10, 0, 0, 0, DateTimeKind.Utc), true, "A brief look into the latest features and performance enhancements in .NET 10.", "Getting Started with .NET 10", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogPostId",
                table: "Comments",
                column: "BlogPostId");
        }

      
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "BlogPosts");
        }
    }
}
