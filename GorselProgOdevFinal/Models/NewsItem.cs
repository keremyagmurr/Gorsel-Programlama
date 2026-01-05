using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Text.RegularExpressions;

namespace GorselProgOdevFinal.Models
{
    public class NewsRoot
    {
        public string Status { get; set; }
        public List<NewsItem> Items { get; set; }
    }

    public class NewsItem
    {
        public string Title { get; set; }
        public string PubDate { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public string Author { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }

        public string CleanDescription
        {
            get
            {
                if (string.IsNullOrEmpty(Description)) return "";

                string noHtml = Regex.Replace(Description, "<.*?>", string.Empty);

                return WebUtility.HtmlDecode(noHtml).Trim();
            }
        }

        public string ShortDescription => CleanDescription.Length > 100 ? CleanDescription.Substring(0, 100) + "..." : CleanDescription;
    }
}