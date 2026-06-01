using Microsoft.EntityFrameworkCore;
using BlogApp.Data;
using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Services
{
    public class BlogService
    {
        private readonly IDbContextFactory<BlogDbContext> _contextFactory;

        public BlogService(IDbContextFactory<BlogDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<BlogPost>> GetPublishedPostsAsync(string? category = null, string? search = null)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var query = context.BlogPosts
                .Where(p => p.IsPublished)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p => p.Category == category);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                string searchLower = search.ToLower();
                query = query.Where(p => p.Title.ToLower().Contains(searchLower) 
                                      || p.Summary.ToLower().Contains(searchLower) 
                                      || p.Content.ToLower().Contains(searchLower));
            }

            return await query
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetAllPostsForAdminAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.BlogPosts
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<BlogPost?> GetPostByIdAsync(int id, bool includeComments = false)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var query = context.BlogPosts.AsQueryable();
            
            if (includeComments)
            {
                query = query.Include(p => p.Comments);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<BlogPost> CreatePostAsync(BlogPost post)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            post.CreatedAt = DateTime.UtcNow;
            context.BlogPosts.Add(post);
            await context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> UpdatePostAsync(BlogPost post)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var existing = await context.BlogPosts.FindAsync(post.Id);
            if (existing == null) return false;

            existing.Title = post.Title;
            existing.Summary = post.Summary;
            existing.Content = post.Content;
            existing.Category = post.Category;
            existing.IsPublished = post.IsPublished;
            existing.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var post = await context.BlogPosts.FindAsync(id);
            if (post == null) return false;

            context.BlogPosts.Remove(post);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Comment> AddCommentAsync(int postId, Comment comment)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            comment.BlogPostId = postId;
            comment.CreatedAt = DateTime.UtcNow;
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.BlogPosts
                .Where(p => p.IsPublished)
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();
        }
    }
}
