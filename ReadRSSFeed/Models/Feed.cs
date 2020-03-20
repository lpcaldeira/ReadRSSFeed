using System;

namespace ReadRSSFeed.Models
{
    public class Feed
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Comments { get; set; }
        public DateTime PubDate { get; set; }
        public string Creator { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}