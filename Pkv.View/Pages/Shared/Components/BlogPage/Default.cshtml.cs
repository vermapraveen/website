
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Pkv.Github.Common;
using Pkv.View.Pages.Shared.ViewModels;

using System;
using System.IO;
using System.Threading.Tasks;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.Utilities;

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

            string blogContent = null;
            if (hostingEnvironment.IsDevelopment())
            {
                var localFile = Path.Combine(hostingEnvironment.ContentRootPath, "blogs/test.md");
                blogContent = await File.ReadAllTextAsync(localFile); ;
            }
            else
            {
                GithubFileProvider githubFileProvider = new GithubFileProvider();
                blogContent = await githubFileProvider.GetFile($"{blogUniqueName}.md", _githubConfigs);
            }

            var blogIntro = blogContent.GetFrontMatter<BlogIntroViewModel>();

            return View(new BlogDataViewModel
            {
                BlogIntro = blogIntro,
                BlogContent = blogContent
            });
        }

        public object? Deserialize(IParser parser, Type type)
        {
            IValueDeserializer valueDeserializer = new DeserializerBuilder().BuildValueDeserializer();
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var hasStreamStart = parser.TryConsume<StreamStart>(out var _);

            var hasDocumentStart = parser.TryConsume<DocumentStart>(out var _);

            object? result = null;
            if (!parser.Accept<DocumentEnd>(out var _) && !parser.Accept<StreamEnd>(out var _))
            {
                using (var state = new SerializerState())
                {
                    result = valueDeserializer.DeserializeValue(parser, type, state, valueDeserializer);
                    state.OnDeserialization();
                }
            }

            if (hasDocumentStart)
            {
                parser.Consume<DocumentEnd>();
            }

            if (hasStreamStart)
            {
                parser.Consume<StreamEnd>();
            }

            return result;
        }
    }
}
