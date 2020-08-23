
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Pkv.Common;
using Pkv.Github.Common;
using Pkv.View.Pages.Shared.ViewModels;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pkv.View.Pages.Shared
{
    public class CommonLogic
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly GithubConfigModel _githubConfigs;
        private readonly IDebugInfoHelper _debugInfoHelper;

        public CommonLogic(IWebHostEnvironment hostingEnvironment, GithubConfigModel githubOptions, IDebugInfoHelper debugInfoHelper)
        {
            this.hostingEnvironment = hostingEnvironment;
            _githubConfigs = githubOptions;
            _debugInfoHelper = debugInfoHelper;
        }

        public async Task<List<BlogIntroViewModel>> GetListOfBlogs()
        {
            string blogContent;
            if (hostingEnvironment.IsDevelopment())
            {
                var refTrace1 = _debugInfoHelper.Start("GetListOfBlogs.Local");
                string localFile = GetLocalFile("blogs/blogList.yaml");
                blogContent = await File.ReadAllTextAsync(localFile);
                _debugInfoHelper.End(refTrace1);
            }
            else
            {
                var refTrace1 = _debugInfoHelper.Start("GetListOfBlogs.Network");
                GithubFileProvider githubFileProvider = new GithubFileProvider();
                blogContent = await githubFileProvider.GetBlogList($"blogList.yaml", _githubConfigs);
                _debugInfoHelper.End(refTrace1);
            }

            var refTrace3 = _debugInfoHelper.Start("DeserializeYamlList");
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var items = deserializer.Deserialize<List<BlogIntroViewModel>>(blogContent);
            _debugInfoHelper.End(refTrace3);

            return items;
        }

        public async Task<BlogDataViewModel> GetBlogData(string uniqueName)
        {

            string blogContent;
            if (hostingEnvironment.IsDevelopment())
            {
                var refTrace1 = _debugInfoHelper.Start("GetBlogData.Local");
                string localFile = GetLocalFile("blogs/test.md");
                blogContent = await File.ReadAllTextAsync(localFile);
                _debugInfoHelper.End(refTrace1);
            }
            else
            {
                var refTrace1 = _debugInfoHelper.Start("GetBlogData.Network");
                GithubFileProvider githubFileProvider = new GithubFileProvider();
                blogContent = await githubFileProvider.GetFile($"{uniqueName}.md", _githubConfigs);
                _debugInfoHelper.End(refTrace1);
            }

            var refTrace3 = _debugInfoHelper.Start("DeserializeYamlIntro");
            var blogIntro = blogContent.GetFrontMatter<BlogIntroViewModel>();
            _debugInfoHelper.End(refTrace3);

            var blogData = new BlogDataViewModel
            {
                BlogIntro = blogIntro,
                BlogContent = blogContent
            };

            return blogData;
        }

        private string GetLocalFile(string filePath)
        {
            return Path.Combine(hostingEnvironment.ContentRootPath, filePath);
        }
    }
}
