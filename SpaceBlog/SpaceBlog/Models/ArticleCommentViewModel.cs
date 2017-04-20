namespace SpaceBlog.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ArticleCommentViewModel
    {
        [Required]
        public int ArticleId { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public int Rating { get; set; }
    }
}