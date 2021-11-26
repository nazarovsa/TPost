using Telegram.Bot.Types.Enums;

namespace TPost.Publishers.Telegram
{
    public sealed class TelegramPublisherOptions
    {
        public string Token { get; set; }
        
        /// <summary>
        /// Parse mode
        /// </summary>
        /// <remarks>Html by default</remarks>
        public ParseMode ParseMode { get; set; } = ParseMode.Html;
        
        public long[] ReceiverIds { get; set; }
    }
}