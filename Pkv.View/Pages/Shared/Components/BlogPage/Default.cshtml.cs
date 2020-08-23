
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Pkv.Common;
using Pkv.Github.Common;
using Pkv.View.Pages.Shared;
using Pkv.View.Pages.Shared.ViewModels;

using System.Threading.Tasks;

namespace Pkv.View.Components
{
    public class BlogPageViewComponent : ViewComponent
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDebugInfoHelper debugInfoHelper;
        private readonly GithubConfigModel _githubConfigs;

        public BlogPageViewComponent(IWebHostEnvironment hostingEnvironment, IOptions<GithubConfigModel> githubOptions, IDebugInfoHelper debugInfoHelper)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.debugInfoHelper = debugInfoHelper;
            _githubConfigs = githubOptions.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync(string uniqueName)
        {
            var trace = debugInfoHelper.Start("BlogPageVc.Model");
            CommonLogic cl = new CommonLogic(hostingEnvironment, _githubConfigs, debugInfoHelper);

            BlogDataViewModel blogData = await cl.GetBlogData(uniqueName);

            debugInfoHelper.End(trace);


            return View(blogData);
        }


    }
}
