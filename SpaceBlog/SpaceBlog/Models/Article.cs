using System;

namespace SpaceBlog.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        //public virtual IEnumerable<Comment> Comments { get; set; }
        //public Category Category { get; set; }
        //public virtual IEnumerable<Tag> Tags  { get; set; }
        public ApplicationUser Author { get; set; }
    }
}