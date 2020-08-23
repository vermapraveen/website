
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Pkv.Common;
using Pkv.Github.Common;
using Pkv.View.Pages.Shared.ViewModels;

using System.Collections.Generic;

namespace Pkv.View.Components
{
    public class BlogListViewComponent : ViewComponent
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDebugInfoHelper debugInfoHelper;
        private readonly GithubConfigModel _githubConfigs;

        public BlogListViewComponent(IWebHostEnvironment hostingEnvironment, IOptions<GithubConfigModel> githubOptions, IDebugInfoHelper debugInfoHelper)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.debugInfoHelper = debugInfoHelper;
            _githubConfigs = githubOptions.Value;

        }

        public IViewComponentResult Invoke(IEnumerable<BlogIntroViewModel> blogIntros)
        {
            var trace = debugInfoHelper.Start("BlogListVc.Model");
            debugInfoHelper.End(trace);

            return View(blogIntros);
        }
    }
}
