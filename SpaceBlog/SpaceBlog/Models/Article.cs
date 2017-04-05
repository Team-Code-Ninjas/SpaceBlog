using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceBlog.Models
{
    public class Article
    {
        public Article()
        {
            Date = DateTime.Now;
            this.Comments = new List<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The Title must be text with maximum 50 characters.")]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }

        /// public virtual IEnumerable<Comment> Comments { get; set; }
        /// public Category Category { get; set; }
        /// public virtual IEnumerable<Tag> Tags  { get; set; }
        
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}