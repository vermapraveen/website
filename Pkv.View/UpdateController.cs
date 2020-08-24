using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

using Pkv.Github.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pkv.View
{
    [Route("webhooks/github")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private const string Sha1Prefix = "sha1=";
        private readonly GithubConfigModel _githubOptions;
        private readonly IGithubEventProcessor _githubEventProcessor;

        public UpdateController(IOptions<GithubConfigModel> githubOptions, IGithubEventProcessor githubEventProcessor)
        {
            _githubOptions = githubOptions.Value;
            this._githubEventProcessor = githubEventProcessor;
        }

        [HttpPost("")]
        public async Task<IActionResult> Receive()
        {
            Console.Out.WriteLine("Received Update from github...");
            Request.Headers.TryGetValue("X-GitHub-Delivery", out StringValues gitHubDeliveryId);
            Request.Headers.TryGetValue("X-GitHub-Event", out StringValues gitHubEvent);
            Request.Headers.TryGetValue("X-Hub-Signature", out StringValues gitHubSignature);

            using (var reader = new StreamReader(Request.Body))
            {
                var txt = await reader.ReadToEndAsync();

                Console.Out.WriteLine("checking IsGitHubSignatureValid....");

                if (IsGitHubSignatureValid(txt, gitHubSignature))
                {
                    var payload = System.Text.Json.JsonSerializer.Deserialize<Payload>(txt);
                    Console.Out.WriteLine("IsGitHubSignatureValid. True");
                    await _githubEventProcessor.Process(payload, _githubOptions);
                    return Ok("works with configured secret!");
                }
            }

            Console.Out.WriteLine("returning Unauthorized");
            return Unauthorized();
        }

        private bool IsGitHubSignatureValid(string payload, string signatureWithPrefix)
        {
            if (string.IsNullOrWhiteSpace(payload))
                throw new ArgumentNullException(nameof(payload));
            if (string.IsNullOrWhiteSpace(signatureWithPrefix))
                throw new ArgumentNullException(nameof(signatureWithPrefix));

            if (signatureWithPrefix.StartsWith(Sha1Prefix, StringComparison.OrdinalIgnoreCase))
            {
                var signature = signatureWithPrefix.Substring(Sha1Prefix.Length);
                var secret = Encoding.ASCII.GetBytes(_githubOptions.WebhookSecret);
                var payloadBytes = Encoding.ASCII.GetBytes(payload);

                using (var hmacsha1 = new HMACSHA1(secret))
                {
                    var hash = hmacsha1.ComputeHash(payloadBytes);

                    var hashString = ToHexString(hash);

                    if (hashString.Equals(signature))
                        return true;
                }
            }

            return false;
        }

        public static string ToHexString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                builder.AppendFormat("{0:x2}", b);
            }

            return builder.ToString();
        }
    }
}
