using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pkv.View.Pages.Comments
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }


    public class AllComments
    {
        public Feedback[] feedback { get; set; }
    }

    public class Feedback
    {
        public string blog { get; set; }
        public Comments comments { get; set; }
    }

    public class Comments
    {
        public string id { get; set; }
        public string user { get; set; }
        public string text { get; set; }
        public string on { get; set; }
        public Reply reply { get; set; }
    }

    public class Reply
    {
        public string id { get; set; }
        public string user { get; set; }
        public string text { get; set; }
        public string on { get; set; }
        public Reply reply { get; set; }
    }
}
