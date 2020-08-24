using Pkv.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using YamlDotNet.Serialization;

using static System.Environment;

namespace Pkv.Github.Common
{
    public interface IGithubEventProcessor
    {
        Task Process(Payload payload, GithubConfigModel githubConfig);
    }

    public class GithubEventProcessor : IGithubEventProcessor
    {


        public async Task Process(Payload payload, GithubConfigModel githubConfig)
        {
            List<BlogIntroViewModel2> updatedBlogList = new List<BlogIntroViewModel2>();
            List<BlogIntroViewModel2> removeBlogList = new List<BlogIntroViewModel2>();

            GithubFileProvider githubFileProvider = new GithubFileProvider();
            var blogList = await githubFileProvider.GetBlogList("blogList.yaml", githubConfig);

            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var blogListItems = deserializer.Deserialize<List<BlogIntroViewModel2>>(blogList);

            foreach (var item in payload.head_commit.modified)
            {
                if (!item.EndsWith(".md"))
                    continue;

                var blogContent = await githubFileProvider.GetFile(item, githubConfig);

                var blogIntro = blogContent.GetFrontMatter<BlogIntroViewModel2>();
                updatedBlogList.Add(blogIntro);

                var savedFolderPath = GetFolderPath(SpecialFolder.UserProfile, SpecialFolderOption.DoNotVerify);
                string appDataPath = Path.Combine(savedFolderPath, $"data/{item}");

                await File.WriteAllTextAsync(appDataPath, blogContent);
            }

            foreach (var item in payload.head_commit.added)
            {
                if (!item.EndsWith(".md"))
                    continue;

                var blogContent = await githubFileProvider.GetFile(item, githubConfig);

                var blogIntro = blogContent.GetFrontMatter<BlogIntroViewModel2>();
                updatedBlogList.Add(blogIntro);

                var savedFolderPath = GetFolderPath(SpecialFolder.UserProfile, SpecialFolderOption.DoNotVerify);
                string appDataPath = Path.Combine(savedFolderPath, $"data/{item}");

                await File.WriteAllTextAsync(appDataPath, blogContent);
            }

            if (payload.head_commit.removed != null && payload.head_commit.removed.Any())
            {
                foreach (var item in payload.head_commit.removed)
                {
                    if (!item.EndsWith(".md"))
                        continue;

                    if (blogListItems.Any(b => string.Compare(b.Slug, item) == 0))
                    {
                        removeBlogList.Add(blogListItems.First(b => string.Compare(b.Slug, item) == 0));
                    }
                }
            }

            foreach (var toUpd in updatedBlogList)
            {
            }
            // get filesUpdated in head_commit-->modified []
            // foreach file -->
            //// Get Latest from github
            //// update local
            //// Get Yaml fronENd
            //// Update list from yaml formatter

            // get filesUpdated in head_commit-->modified []
            // foreach file -->
            //// Get Latest from github
            //// update local
            //// Get Yaml fronENd
            //// Update list from yaml formatter

            // get filesUpdated in head_commit-->modified []
            // foreach file -->
            //// Get Latest from github
            //// update local
            //// Get Yaml fronENd
            //// Update list from yaml formatter
            throw new NotImplementedException();
        }
    }


    public class Payload
    {
        public Head_Commit head_commit { get; set; }
    }

    public class Head_Commit
    {
        public string id { get; set; }
        public DateTime timestamp { get; set; }
        public string[] added { get; set; }
        public string[] removed { get; set; }
        public string[] modified { get; set; }
    }

    // TODO: Remove duplication
    public class BlogIntroViewModel2
    {
        [YamlMember(Alias = "title")]
        public string Title { get; set; }

        [YamlMember(Alias = "tags")]
        public string Tags { get; set; }

        [YamlMember(Alias = "slug")]
        public string Slug { get; set; }

        [YamlMember(Alias = "intro")]
        public string Intro { get; set; }

        [YamlMember(Alias = "publishedDate")]
        public DateTime PublishedDate { get; set; }

        [YamlMember(Alias = "isDraft")]
        public bool IsDraft { get; set; }

        [YamlIgnore]
        public string[] GetTags => Tags?
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
    }
}
