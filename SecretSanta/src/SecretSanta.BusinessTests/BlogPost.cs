using System;

namespace SecretSanta.Business.Tests
{
    public class BlogPost
    {
       
        private string _Content;
        private DateTime _Date;
        

        public string Title { get; }

        public BlogPost(string title, string content, DateTime date)
        {
            this.Title = title;
            this._Content = content;
            this._Date = date;
        }
    }
}