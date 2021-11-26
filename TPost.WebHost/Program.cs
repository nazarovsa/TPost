using Microsoft.Extensions.Hosting;

namespace TPost.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DefaultHostBuilder.GetBuilder(args).Build().Run();
        }
    }
}