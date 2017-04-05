using System;
using System.ComponentModel.DataAnnotations;

namespace SpaceBlog.Models
{
    public class Comment
    {   
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime DateTimeComment { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public Article Article { get; set; }
    }
}