using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System;

namespace Pkv.View.Pages.blogs
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string BlogUniqueName { get; set; }
        public void OnGet(string uniqueName)
        {
            Console.Out.WriteLine($"blogs/IndexModel/OnGet/{uniqueName}");
            BlogUniqueName = uniqueName;
        }
    }
}
