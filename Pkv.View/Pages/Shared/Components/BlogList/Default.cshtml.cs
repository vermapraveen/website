
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        private readonly GithubConfigModel _githubConfigs;

        public BlogListViewComponent(IWebHostEnvironment hostingEnvironment, IOptions<GithubConfigModel> githubOptions)
        {
            this.hostingEnvironment = hostingEnvironment;
            _githubConfigs = githubOptions.Value;

        }

        public async System.Threading.Tasks.Task<IViewComponentResult> InvokeAsync()
        {
            Console.Out.WriteLine($"Components/BlogListViewComponent/Invoke/");

            CommonLogic cl = new CommonLogic(hostingEnvironment, _githubConfigs);
            List<BlogIntroViewModel> items = await cl.GetListOfBlogs();

            return View(items.Where(x => !x.IsDraft));
        }
    }
}
