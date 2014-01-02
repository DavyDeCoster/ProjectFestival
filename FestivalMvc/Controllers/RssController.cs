using FestivalMvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Festival.model;
using System.Collections.ObjectModel;
using System.ServiceModel.Syndication;

namespace FestivalMvc.Controllers
{
    public class RssController : Controller
    {
        //
        // GET: /Rss/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(RssVM vm)
        {
            Message m = vm.NewMessage;
            Message.AddMessage(m);

            return RedirectToAction("Index", "Home");
        }

        public Rss20FeedFormatter MakeRss()
        {
            var Rss = new SyndicationFeed("RSS Feed", "The feed for u", new Uri("http://localhost:7273/RSS/MakeFeed"));

            Rss.Language = "nl-be";
            Rss.Authors.Add(new SyndicationPerson("Davy De Coster"));
            Rss.Categories.Add(new SyndicationCategory("Nieuws"));

            ObservableCollection<Message> ocMessages = Message.GetMessages();
            ObservableCollection<SyndicationItem> ocFeed = new ObservableCollection<SyndicationItem>();

            foreach (Message m in ocMessages)
            {
                SyndicationItem item = new SyndicationItem(m.Title, m.MessageText, new Uri("http://localhost:7273/RSS/MakeFeed"));

                ocFeed.Add(item);
            }

            Rss.Items = ocFeed;

            return new Rss20FeedFormatter(Rss);
        }
    }
}
