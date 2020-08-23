using System;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pkv.View.Pages
{
    public class PrivacyModel : PageModel
    {
        public void OnGet()
        {
            Console.Out.WriteLine("in PrivacyModel");
        }
    }
}
