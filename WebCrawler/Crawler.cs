using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Configuration;

namespace WebCrawler
{
    class Crawler
    {
        private const string _LINK_REGEX = "href=\"[a-zA-Z./:&\\d_-]+\"";
        private static List<Uri> _pages = new List<Uri>();
        private static List<Uri> _sameDomainPage = new List<Uri>();
        private static List<Uri> _externalPages = new List<Uri>();
        private static List<Uri> _staticContent = new List<Uri>();

        public static List<Uri> SameDomainPage
        {
            get { return _sameDomainPage; }
            set { _sameDomainPage = value; }
        }

        public static List<Uri> ExternalPages
        {
            get { return _externalPages; }
            set { _externalPages = value; }
        }

        public static List<Uri> StaticContent
        {
            get { return _staticContent; }
            set { _staticContent = value; }
        }

        /// <summary>
        /// Recursive method to go through all the link
        /// </summary>
        /// <param name="url"></param>
        public static void CrawlPage(Uri url)
        {
            if (!PageHasBeenCrawled(url))
            {              
                var htmlText = getPageContent(url);

                var linkParser = new LinkParser();

                _pages.Add(url);

                linkParser.ParseLinks(htmlText, url);

                AddRangeButNoDuplicates(_sameDomainPage, linkParser.ValidUrls);
                AddRangeButNoDuplicates(_externalPages, linkParser.ExternalUrls);
                AddRangeButNoDuplicates(_staticContent, linkParser.OtherUrls);

                //Crawl all the links found on the page.
                foreach (Uri link in linkParser.ValidUrls)
                {
                    try
                    {
                        if (link.ToString() != String.Empty && link.ToString() != ConfigurationManager.AppSettings["url"]  &&
                            link != url)
                        {
                            CrawlPage(link);
                        }
                    }
                    catch (Exception)
                    {
                        // add code to find the list of broken url's
                    }
                }
            }
        }

        /// <summary>
        /// Method to retrieve HTML content of the page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string getPageContent(Uri url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream);
            string htmlText = reader.ReadToEnd();
            return htmlText;
        }

        /// <summary>
        /// Method to compare the list of URL visit
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool PageHasBeenCrawled(Uri url)
        {
            foreach (Uri page in _pages)
            {
                if (page == url)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Method to remove duplicate link from  the list
        /// </summary>
        /// <param name="targetList"></param>
        /// <param name="sourceList"></param>
        private static void AddRangeButNoDuplicates(List<Uri> targetList, List<Uri> sourceList)
        {
            foreach (Uri str in sourceList)
            {
                if (!targetList.Contains(str) && !targetList.Contains(new Uri(str.ToString() + "/")))
                    targetList.Add(str);
            }
        }
    }
}
