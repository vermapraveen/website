
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pkv.Common;
using Pkv.Github.Common;
using Pkv.View.Pages.Shared;
using Pkv.View.Pages.Shared.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;

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

        public async System.Threading.Tasks.Task<IViewComponentResult> InvokeAsync()
        {
            var trace = debugInfoHelper.Start("BlogListVC Model");

            CommonLogic cl = new CommonLogic(hostingEnvironment, _githubConfigs, debugInfoHelper);
            List<BlogIntroViewModel> items = await cl.GetListOfBlogs();

            debugInfoHelper.End(trace);
            return View(items.Where(x => !x.IsDraft));
        }
    }
}
