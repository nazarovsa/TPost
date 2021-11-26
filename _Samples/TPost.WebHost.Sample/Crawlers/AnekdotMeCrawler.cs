using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TPost.Core;

namespace TPost.Host.Sample.Crawlers
{
    public sealed class AnekdotMeCrawler : IPostCrawler
    {
        public async Task<IReadOnlyCollection<IPost>> GetPosts(CancellationToken cancellationToken = default)
        {
            var web = new HtmlWeb();
            var htmlDoc = await web.LoadFromWebAsync("http://anekdotme.ru/random", cancellationToken);
            var posts = htmlDoc.DocumentNode
                .SelectNodes(".//div")
                .Where(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "anekdot_text"))
                .Select(x => new Post(AnekdotCleaner.Clean(x.InnerHtml)))
                .Where(x => !x.Content.Contains("$#"))
                .ToArray();
            
            return posts;
        }
    }
}