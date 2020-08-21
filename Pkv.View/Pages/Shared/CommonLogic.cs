
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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

        public CommonLogic(IWebHostEnvironment hostingEnvironment, GithubConfigModel githubOptions)
        {
            this.hostingEnvironment = hostingEnvironment;
            _githubConfigs = githubOptions;
        }

        public async Task<List<BlogIntroViewModel>> GetListOfBlogs()
        {
            string blogContent;
            if (hostingEnvironment.IsDevelopment())
            {
                string localFile = GetLocalFile("blogs/blogList.yaml"); 
                blogContent = await File.ReadAllTextAsync(localFile);
            }
            else
            {
                GithubFileProvider githubFileProvider = new GithubFileProvider();
                blogContent = await githubFileProvider.GetBlogList($"blogList.yaml", _githubConfigs);
            }

            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var items = deserializer.Deserialize<List<BlogIntroViewModel>>(blogContent);
            return items;
        }

        public async Task<BlogDataViewModel> GetBlogData(string blogUniqueName)
        {
            string blogContent;
            if (hostingEnvironment.IsDevelopment())
            {
                string localFile = GetLocalFile("blogs/test.md");
                blogContent = await File.ReadAllTextAsync(localFile); ;
            }
            else
            {
                GithubFileProvider githubFileProvider = new GithubFileProvider();
                blogContent = await githubFileProvider.GetFile($"{blogUniqueName}.md", _githubConfigs);
            }

            var blogIntro = blogContent.GetFrontMatter<BlogIntroViewModel>();

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
