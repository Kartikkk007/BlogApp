using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int BlogPostId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string AuthorName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Comment content is required")]
        [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public BlogPost? BlogPost { get; set; }
    }
}
