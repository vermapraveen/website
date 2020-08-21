
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Pkv.Github.Common;
using Pkv.View.Pages.Shared;
using Pkv.View.Pages.Shared.ViewModels;

using System;
using System.IO;
using System.Threading.Tasks;

namespace Pkv.View.Components
{
    public class BlogPageViewComponent : ViewComponent
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly GithubConfigModel _githubConfigs;

        public BlogPageViewComponent(IWebHostEnvironment hostingEnvironment, IOptions<GithubConfigModel> githubOptions)
        {
            this.hostingEnvironment = hostingEnvironment;
            _githubConfigs = githubOptions.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync(string blogUniqueName)
        {
            Console.Out.WriteLine($"Components/BlogPageViewComponent/Invoke/{blogUniqueName}");

            Console.Out.WriteLine(hostingEnvironment.EnvironmentName);

            CommonLogic cl = new CommonLogic(hostingEnvironment, _githubConfigs);

            BlogDataViewModel blogData = await cl.GetBlogData(blogUniqueName);

            return View(blogData);
        }


    }
}
