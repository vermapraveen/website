namespace Pkv.Github.Common
{
    public class GithubConfigModel
    {
        public const string GithubSection = "github";

        public string ContentFolderPath { get; set; }
        public string RepoName { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }

    }
}
