using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

using Pkv.Common;
using Pkv.Github.Common;
using Pkv.View.Pages.Shared;
using Pkv.View.Pages.Shared.ViewModels;

namespace Pkv.View.Pages
{
    public class BlogsModel : PageModel
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDebugInfoHelper _debugInfoHelper;
        private readonly GithubConfigModel _githubConfigs;

        [BindProperty]
        public IEnumerable<BlogIntroViewModel> Blogs { get; set; }

        public BlogsModel(IWebHostEnvironment hostingEnvironment, IOptions<GithubConfigModel> githubOptions, IDebugInfoHelper debugInfoHelper)
        {
            this.hostingEnvironment = hostingEnvironment;
            _debugInfoHelper = debugInfoHelper;
            _githubConfigs = githubOptions.Value;

        }

        public async Task OnGetAsync()
        {
            var traceRef = _debugInfoHelper.Start("BlogsModel");

            CommonLogic cl = new CommonLogic(hostingEnvironment, _githubConfigs, _debugInfoHelper);
            List<BlogIntroViewModel> items = await cl.GetListOfBlogs();
            Blogs = items.Where(x => !x.IsDraft);
            _debugInfoHelper.End(traceRef);
        }
    }
}
