using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false; // Default is not deleted

        [Required(ErrorMessage = "Title is required")]
        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Summary is required")]
        [StringLength(300, ErrorMessage = "Summary cannot exceed 300 characters")]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        public string Category { get; set; } = "General";

        public bool IsPublished { get; set; } = true;

        public List<Comment> Comments { get; set; } = new();
    }
}
