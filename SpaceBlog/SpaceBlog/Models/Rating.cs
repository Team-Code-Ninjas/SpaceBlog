using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpaceBlog.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Range(0, 5)]
        public decimal Value { get; set; }

        public string AuthorId { get; set; }
 
        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }

        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}