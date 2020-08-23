using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pkv.View.Pages.blogs
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string UniqueName { get; set; }
        public void OnGet(string uniqueName)
        {
            UniqueName = uniqueName;
        }
    }
}
