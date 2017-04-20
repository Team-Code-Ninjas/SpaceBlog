namespace SpaceBlog.Models
{
    using System;
    using System.Collections.Generic;

    public class UserViewModel
    {
        public string Id
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public string FullName
        {
            get; set;
        }

        public int ArticlesCreated
        {
            get; set;
        }

        public int CommentsMade
        {
            get; set;
        }

        public List<Article> Articles
        {
            get; set;
        }

        public List<Comment> Comments
        {
            get; set;
        }

        public string UserType
        {
            get; set;
        }

        public Status Status
        {
            get; set;
        }

        public DateTime DateRegistered
        {
            get; set;
        }
    }
}