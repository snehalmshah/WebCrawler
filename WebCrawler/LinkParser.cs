using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebCrawler
{
    /// <summary>
    /// Link parser class.
    /// </summary>
    public class LinkParser
    {
        private const string _LINK_REGEX = "href=\"[a-zA-Z./:&\\d_-]+\"";
        private List<Uri> _validUrls = new List<Uri>();
        private List<Uri> _otherUrls = new List<Uri>();
        private List<Uri> _externalUrls = new List<Uri>();

        public List<Uri> ValidUrls
        {
            get { return _validUrls; }
            set { _validUrls = value; }
        }

        public List<Uri> OtherUrls
        {
            get { return _otherUrls; }
            set { _otherUrls = value; }
        }

        public List<Uri> ExternalUrls
        {
            get { return _externalUrls; }
            set { _externalUrls = value; }
        }

        /// <summary>
        /// Method to retrieve list of URL on the page
        /// </summary>
        /// <param name="pageText">HTML content of the page</param>
        /// <param name="sourceUrl">URL of the page</param>
        public void ParseLinks(string pageText, Uri sourceUrl)
        {
            MatchCollection matches = Regex.Matches(pageText, _LINK_REGEX);

            for (int i = 0; i <= matches.Count - 1; i++)
            {
                Match anchorMatch = matches[i];

                if (anchorMatch.Value != String.Empty)
                {              
                    string foundHref = null;
                    try
                    {
                        foundHref = anchorMatch.Value.Replace("href=\"", "");
                        foundHref = foundHref.Substring(0, foundHref.IndexOf("\""));
                    }
                    catch (Exception)
                    {
                        // add code to find the list of broken url's
                    }

                    Uri uriFoundHref = new Uri(foundHref);

                    if (!ValidUrls.Contains(uriFoundHref))
                    {
                        if (foundHref != "/")
                        {
                            if (IsExternalUrl(foundHref))
                            {
                                _externalUrls.Add(uriFoundHref);
                            }
                            else if (!IsAWebPage(foundHref))
                            {
                                _otherUrls.Add(uriFoundHref);
                            }
                            else
                            {
                                if (foundHref.Substring(foundHref.Length - 1) != "/")
                                {
                                    foundHref = foundHref + "/";
                                }

                                if (!ValidUrls.Contains(uriFoundHref))
                                    ValidUrls.Add(uriFoundHref);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to verify is the URL a link to external site
        /// </summary>
        /// <param name="url">The URL to be verify</param>
        /// <returns></returns>
        private static bool IsExternalUrl(string url)
        {
            if (!url.Contains(ConfigurationManager.AppSettings["domain"]))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Method to verify is the URL a link or static content
        /// </summary>
        /// <param name="foundHref">The URL to checked</param>
        /// <returns></returns>
        private static bool IsAWebPage(string foundHref)
        {
            if (foundHref.IndexOf("javascript:") == 0)
                return false;

            string extension = foundHref.Substring(foundHref.LastIndexOf(".") + 1, foundHref.Length - foundHref.LastIndexOf(".") - 1);
            switch (extension)
            {
                case "jpg":
                case "css":
                case "php":
                case "png":
                case "gif":
                case "ico":
                    return false;
                default:
                    return true;
            }

        }
    }
}