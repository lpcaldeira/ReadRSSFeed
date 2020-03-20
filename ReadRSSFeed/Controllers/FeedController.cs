using ReadRSSFeed.Models;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;  
using System.Web.Mvc;  
using System.Xml.Linq;  
  
namespace ReadRSSFeed.Controllers
{
    public class FeedController : Controller
    {
        /*public ActionResult Index()
        {
            return View(new List<Feed>());
        }
        [HttpPost]*/
        public ActionResult Index(string RSSURL = "http://deen.com.br/blog/feed/")
        {
            WebClient wclient = new WebClient();
            //string RSSData = wclient.DownloadString(RSSURL);
            var RSSData = wclient.DownloadData(RSSURL);
            string RSSDataDecode = Encoding.UTF8.GetString(RSSData);


            string pattern = @"(?i)<img.*?src=""(?<url>.*?)"".*?>";
            Regex rx = new Regex(pattern);


            XDocument xml = XDocument.Parse(RSSDataDecode);
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new Feed
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Comments = ((string)x.Element("comments")),
                                   PubDate = ((DateTime)x.Element("pubDate")),
                                   Creator = x.Elements().Where(y => y.Name.LocalName == "creator").FirstOrDefault().Value,
                                   Category = ((string)x.Element("category")),
                                   Description = ((string)x.Element("description")),
                                   Image = rx.Match(x.Elements().Where(y => y.Name.LocalName == "encoded").FirstOrDefault().Value).Value
                               })
                               .OrderByDescending(x => x.PubDate);

            return View(RSSFeedData.ToList());
        }
    }
}