using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvcParkNet.Models;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace mvcParkNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            try
            {
                List<homeModel> infos = new List<homeModel>();

                XmlDocument xmlDocument = new XmlDocument();
                
                xmlDocument.Load("/Users/capi/Desktop/Projects/C#/parknetApp/mobyDeneme.xml");

                var firsTenCount = xmlDocument.SelectNodes("/words/word").Cast<XmlElement>().OrderByDescending(c => Int32.Parse(c.Attributes["count"].Value)).Take(10);

                foreach (XmlNode node in firsTenCount)
                {
                    homeModel info = new homeModel();

                    info.count = int.Parse(node.Attributes["count"].InnerText);
                    info.text = node.Attributes["text"].InnerText;
                    infos.Add(info);
                }
                return View(infos);
            }
            catch
            {
                throw;
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
