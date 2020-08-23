using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using static System.Environment;

namespace Pkv.Github.Common
{
    public class GithubFileProvider
    {
        public async Task<string> GetFile(string fileName, GithubConfigModel githubConfig)
        {
            string folderPath = githubConfig.ContentFolderPath;

            var savedFolderPath = GetFolderPath(SpecialFolder.UserProfile, SpecialFolderOption.DoNotVerify);
            string appDataPath = Path.Combine(savedFolderPath, $"data{folderPath}/{fileName}");
            Console.Out.WriteLine($"path to read {appDataPath}");

            if (File.Exists(appDataPath))
            {
                Console.Out.WriteLine($"File exist in data folder");
                return await File.ReadAllTextAsync(appDataPath);
            }

            Console.Out.WriteLine($"File from Github");
            string githubBaseUrl = "https://api.github.com/repos";
            string userName = githubConfig.UserName;
            string repoName = githubConfig.RepoName;
            string token = githubConfig.Token;

            string filePath = $"{githubBaseUrl}/{userName}/{repoName}/contents{folderPath}/{fileName}";
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("GET"), filePath);
            request.Headers.TryAddWithoutValidation("Accept", "application/vnd.github.v3.raw");
            request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            request.Headers.TryAddWithoutValidation("Authorization", $"token {token}");

            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");//Set the User Agent to "request"

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            Console.Out.WriteLine($"got file from github");

            await File.WriteAllTextAsync(appDataPath, content);
            Console.Out.WriteLine($"saved to data folder");

            return content;
        }

        public async Task<string> GetBlogList(string fileName, GithubConfigModel githubConfig)
        {
            string folderPath = "";

            var savedFolderPath = GetFolderPath(SpecialFolder.UserProfile, SpecialFolderOption.DoNotVerify);
            string appDataPath = Path.Combine(savedFolderPath, $"data{folderPath}/{fileName}");
            Console.Out.WriteLine($"path to read {appDataPath}");

            if (File.Exists(appDataPath))
            {
                Console.Out.WriteLine($"File exist in data folder");
                return await File.ReadAllTextAsync(appDataPath);
            }

            Console.Out.WriteLine($"File from Github");
            string githubBaseUrl = "https://api.github.com/repos";
            string userName = githubConfig.UserName;
            string repoName = githubConfig.RepoName;
            string token = githubConfig.Token;

            string filePath = $"{githubBaseUrl}/{userName}/{repoName}/contents{folderPath}/{fileName}";
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("GET"), filePath);
            request.Headers.TryAddWithoutValidation("Accept", "application/vnd.github.v3.raw");
            request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            request.Headers.TryAddWithoutValidation("Authorization", $"token {token}");

            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");//Set the User Agent to "request"

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            Console.Out.WriteLine($"got file from github");

            await File.WriteAllTextAsync(appDataPath, content);
            Console.Out.WriteLine($"saved to data folder");

            return content;
        }
    }
}
