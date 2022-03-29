using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtonList.Web.Models;
using ProtonList.BO;

namespace ProtonList.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            BLL.ListInfoBL listInfoBLL = new BLL.ListInfoBL();
            HomePageModel obj = new HomePageModel();

            List<ListInfo> featuredList = listInfoBLL.GetHomePageLists("Featured", 4);


            ProtonListModel plmFeatured = new ProtonListModel
            {
                SearchItems = featuredList,
                MaxItemsToDisplay = 4
            };


            List<ListInfo> mostViewedList = listInfoBLL.GetHomePageLists("MostViewed", 4);

            ProtonListModel plmMostViewed = new ProtonListModel
            {
                SearchItems = mostViewedList,
                MaxItemsToDisplay = 4
            };


            List<UserInfo> topContributorsList = new List<UserInfo>
            {
                 new UserInfo{ Title="Sagar Pandey" , Likes= 2, Views = 4 , ProfilePictureUrl="http://icons.iconarchive.com/icons/paomedia/small-n-flat/1024/user-female-alt-icon.png" },
                 new UserInfo{ Title="Akshata Pandey" , Likes= 2, Views = 4 , ProfilePictureUrl="http://icons.iconarchive.com/icons/paomedia/small-n-flat/1024/user-female-alt-icon.png" },

            };


            obj.Featured = plmFeatured;
            obj.MostViewed = plmMostViewed;
            obj.TopContributors = topContributorsList;
            return View(obj);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult FAQ()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult TermsOfUse()
        {
            return View("TermsOfUse");
        }

        public ActionResult PrivacyPolicy()
        {
            return View("PrivacyPolicy");
        }

    }


}