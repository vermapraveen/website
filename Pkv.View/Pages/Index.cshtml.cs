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
using Pkv.Common;

namespace Pkv.View.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDebugInfoHelper _debugInfoHelper;
        private readonly GithubConfigModel _githubConfigs;

        [BindProperty]
        public string BlogUniqueName { get; set; }

        public IndexModel(IWebHostEnvironment hostingEnvironment, IOptions<GithubConfigModel> githubOptions, IDebugInfoHelper debugInfoHelper)
        {
            this.hostingEnvironment = hostingEnvironment;
            _debugInfoHelper = debugInfoHelper;
            _githubConfigs = githubOptions.Value;

        }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var traceRef = _debugInfoHelper.Start("Index");

            CommonLogic cl = new CommonLogic(hostingEnvironment, _githubConfigs, _debugInfoHelper);
            List<BlogIntroViewModel> items = await cl.GetListOfBlogs();

            BlogUniqueName = GetLatestBlogSlug(items);
            _debugInfoHelper.End(traceRef);
        }

        private static string GetLatestBlogSlug(List<BlogIntroViewModel> items)
        {
            return items.Where(x => !x.IsDraft).OrderBy(x => x.PublishedDate).FirstOrDefault().Slug;
        }
    }
}
