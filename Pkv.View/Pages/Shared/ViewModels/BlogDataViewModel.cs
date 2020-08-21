using System;
using System.Linq;

using YamlDotNet.Serialization;

namespace Pkv.View.Pages.Shared.ViewModels
{
    public class BlogDataViewModel
    {
        public BlogIntroViewModel BlogIntro { get; set; }
        public string BlogContent { get; set; }
    }

    public class BlogIntroViewModel
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
