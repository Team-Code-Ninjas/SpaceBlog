﻿namespace SpaceBlog.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

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
        public string AuthorId { get; set; }

        public int ArticleId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}