﻿namespace SpaceBlog.Models
{
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

        public string UserType
        {
            get; set;
        }

        public Status Status
        {
            get; set;
        }
    }
}