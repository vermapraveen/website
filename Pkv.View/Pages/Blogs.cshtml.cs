using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Pkv.View.Pages
{
    public class BlogsModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public BlogsModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.Out.WriteLine("in BlogsModel");
        }
    }
}
