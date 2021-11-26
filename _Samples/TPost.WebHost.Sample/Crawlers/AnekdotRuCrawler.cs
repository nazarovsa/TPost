using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using TPost.Core;

namespace TPost.Host.Sample.Crawlers
{
    public sealed class AnekdotRuCrawler : IPostCrawler
    {
        public Task<IReadOnlyCollection<IPost>> GetPosts(CancellationToken cancellationToken = default)
        {
            string url = "https://www.anekdot.ru/rss/export_j_non_burning.xml";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            return Task.FromResult<IReadOnlyCollection<IPost>>(
                feed.Items
                    .Select(x => new Post(x.Id, AnekdotCleaner.Clean(x.Summary.Text)))
                    .Where(x => !x.Content.Contains("$#"))
                    .ToArray());
        }
    }
}