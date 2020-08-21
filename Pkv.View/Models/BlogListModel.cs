using System;

namespace Pkv.View.Models
{
    public class BlogListModel
    {
        public BlogIntroModel[] blogList { get; set; }
    }

    public class BlogIntroModel
    {
        public string slug { get; set; }
        public string title { get; set; }
        public string[] tags { get; set; }
        public DateTime date { get; set; }
        public string intro { get; set; }
        public bool read { get; set; }
    }
}
