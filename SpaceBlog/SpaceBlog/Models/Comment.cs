using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceBlog.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public ApplicationUser Author { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Article Article { get; set; }
    }

    public class CommentViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public string AuthorId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }

}