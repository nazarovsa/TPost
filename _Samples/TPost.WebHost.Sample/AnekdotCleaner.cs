namespace TPost.Host.Sample
{
    public static class AnekdotCleaner
    {
        public static string Clean(string plainAnekdot)
        {
            return plainAnekdot
                .Trim()
                .Replace("<br> ", "\n")
                .Replace("<br>", "\n")
                .Replace("--", "-")
                .Replace("&mdash;", "-")
                .Replace("  ", " ")
                .Replace("\n\n", "\n");
        }
    }
}