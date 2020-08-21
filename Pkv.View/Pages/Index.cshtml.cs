using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pkv.Github.Common;
using Pkv.View.Pages.Shared;
using Pkv.View.Pages.Shared.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Pkv.View.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly GithubConfigModel _githubConfigs;

        [BindProperty]
        public String BlogUniqueName { get; set; }

        public IndexModel(IWebHostEnvironment hostingEnvironment, IOptions<GithubConfigModel> githubOptions)
        {
            this.hostingEnvironment = hostingEnvironment;
            _githubConfigs = githubOptions.Value;

        }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Console.Out.WriteLine($"Pages/IndexModel/OnGet/");

            CommonLogic cl = new CommonLogic(hostingEnvironment, _githubConfigs);
            List<BlogIntroViewModel> items = await cl.GetListOfBlogs();

            BlogUniqueName = items.Where(x => !x.IsDraft).OrderBy(x => x.PublishedDate).FirstOrDefault().Slug;
        }
    }
}
