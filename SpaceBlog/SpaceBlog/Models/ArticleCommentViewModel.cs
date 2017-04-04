using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpaceBlog.Models
{
    public class ArticleCommentViewModel
    {
        [Required]
        public int ArticleId { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}