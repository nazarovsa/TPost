using System;

namespace TPost.Core
{
    public class Post : IPost
    {
        private readonly string _key;

        public Post(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));

            Content = content;
        }

        public Post(string key, string content) : this(content)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            _key = key;
        }

        public string Key => string.IsNullOrWhiteSpace(_key) ? Content : _key;

        public string Content { get; }
    }
}