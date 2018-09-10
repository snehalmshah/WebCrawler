using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;

namespace WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting crawling of {0}", ConfigurationManager.AppSettings["url"]);

            Crawler.CrawlPage(new Uri(ConfigurationManager.AppSettings["url"]));

            ExportContentofListtoFile(Crawler.SameDomainPage, "SiteMap.txt");
            ExportContentofListtoFile(Crawler.ExternalPages, "ExternalUrls.txt");
            ExportContentofListtoFile(Crawler.StaticContent, "StaticContent.txt");

            Console.WriteLine("Crawling of {0} completed succefully.", ConfigurationManager.AppSettings["url"]);
        }

        private static void ExportContentofListtoFile(List<Uri> listofUrls, string fileName)
        {
            using (TextWriter tw = new StreamWriter(fileName))
            {
                foreach (Uri s in listofUrls)
                    tw.WriteLine(s);
            }
        }
    }
}
