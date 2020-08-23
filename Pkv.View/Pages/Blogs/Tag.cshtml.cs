using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

using Pkv.Common;
using Pkv.Github.Common;
using Pkv.View.Pages.Shared;
using Pkv.View.Pages.Shared.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Pkv.View.Pages.blogs
{
    public class TagModel : PageModel
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDebugInfoHelper _debugInfoHelper;
        private readonly GithubConfigModel _githubConfigs;

        [BindProperty]
        public IEnumerable<BlogIntroViewModel> Blogs { get; set; }

        public TagModel(IWebHostEnvironment hostingEnvironment, IOptions<GithubConfigModel> githubOptions, IDebugInfoHelper debugInfoHelper)
        {
            this.hostingEnvironment = hostingEnvironment;
            _debugInfoHelper = debugInfoHelper;
            _githubConfigs = githubOptions.Value;

        }
        public async System.Threading.Tasks.Task OnGetAsync(string catName)
        {
            var traceRef = _debugInfoHelper.Start("Blogs.Tags.Model");

            CommonLogic cl = new CommonLogic(hostingEnvironment, _githubConfigs, _debugInfoHelper);
            List<BlogIntroViewModel> items = await cl.GetListOfBlogs();

            Blogs = items.Where(x => !x.IsDraft && x.GetTags.Contains(catName));
            _debugInfoHelper.End(traceRef);
        }
    }
}
