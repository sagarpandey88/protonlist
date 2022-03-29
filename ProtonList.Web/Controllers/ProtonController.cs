using ProtonList.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtonList.BO;
using Newtonsoft.Json;
using ProtonList.BLL;
using ProtonList.Common;

namespace ProtonList.Web.Controllers
{
    public class ProtonController : Controller
    {
        ListInfoBL listInfoBL = new ListInfoBL();
        // GET: Proton
        public ActionResult Index()
        {
            return View("Error");
        }


        #region GET

        [Authorize]
        public ActionResult Create(string password)
        {
            ViewBag.Categories = GetCategorties("Choose Category");
            return View("NewProton");

        }

        [Authorize]
        public ActionResult Edit(int Id)
        {
            ProtonModel pm = GetList(Id);
            pm.CategoryMaster = GetCategorties(pm.ListInfo.SubCategory);
            return View("EditProton", pm);
        }


        [HttpGet, ValidateHeaderAntiForgeryToken]
        public string Test(string testParam)
        {
            return testParam + "Test";
        }

        public ActionResult ViewProton(int Id)
        {

            Logger.InfoTrace("my trace");
            ProtonModel pm = GetList(Id);
            return View("ViewProton", pm);

        }

        [Route("Proton/Show/{listUrl}")]
        public ActionResult Show(string listUrl)
        {
            var arr = listUrl.Split('=');
            int Id = Convert.ToInt32(arr[1]);
            ProtonModel pm = GetList(Id);
            return View("ViewProton", pm);

        }


        public ActionResult Typeahead(string keyword)
        {
            int pageSize = 5;

            List<ListInfo> listInfo = listInfoBL.GetSearchResult(keyword, pageSize).ListInfo;


            return Json(listInfo.Select(x => x.ListTitle), JsonRequestBehavior.AllowGet);
        }


        #region Search and GetByCategory
        public ActionResult SearchProton(string keyword, int page = 0)
        {
            int pageSize = 30;
            int lastIndex = page * pageSize;
            ListResultSet listResult = listInfoBL.GetSearchResult(keyword, pageSize, lastIndex);
            List<ListInfo> listInfo = listResult.ListInfo;
            var pager = new Pager(listResult.TotalNoOfItems, page, pageSize);
            SearchPageModel spm = new SearchPageModel
            {
                ProtonList = new ProtonListModel { SearchItems = listInfo, MaxItemsToDisplay = pageSize, Pager = pager },
                Keyword = keyword,
                TotalNumberOfRecords = listResult.TotalNoOfItems,


            };
            ViewBag.SearchKeyword = keyword;
            return View("Search", spm);

        }


        public ActionResult GetByCategory(string category)
        {
            int pageSize = 300;
            List<ListInfo> listInfo = listInfoBL.GetByCategory(category, pageSize);

            SearchPageModel spm = new SearchPageModel
            {
                ProtonList = new ProtonListModel { SearchItems = listInfo, MaxItemsToDisplay = pageSize },
                Keyword = category
            };
            ViewBag.Category = category;
            return View("Category", spm);

        }


        [Authorize]
        public ActionResult GetMyLists()
        {
            List<ListInfo> listInfo = listInfoBL.GetByUserName(User.Identity.Name, 30);

            SearchPageModel spm = new SearchPageModel
            {
                ProtonList = new ProtonListModel { SearchItems = listInfo, MaxItemsToDisplay = 30 },
                Keyword = ""
            };
            // ViewBag.Category = category;
            return View("MyProtonList", spm.ProtonList);

        }
        #endregion

        #endregion

        #region POST

        [HttpPost, ValidateHeaderAntiForgeryToken]
        public string CreateList(ProtonModel proton)
        {

            try
            {

                ListInfo listInfo = new ListInfo();
                listInfo = proton.ListInfo;

                ApplicationDbContext context = new ApplicationDbContext();
                string emailId = context.Users.Where(x => x.UserName == User.Identity.Name).First().Email;

                listInfo.ListItemDetails = JsonConvert.SerializeObject(proton.ListItemDetails);
                listInfo.OtherListInfo = JsonConvert.SerializeObject(proton.OtherListInfo);
                listInfo.CreatedBy = User.Identity.Name;
                listInfo.ModifiedBy = User.Identity.Name;
                listInfo.Status = "Active";
                SetCategory(listInfo);

                listInfo.ListUrl = "Some Custom Url";//not required while create

                listInfo.IsAnonymous = 1;

                listInfoBL.InsertLists(listInfo);


                if (listInfo.Id > 0)
                {
                    return Convert.ToString(listInfo.ListUrl.Replace("~/Proton/", ""));
                }

            }
            catch (Exception ex)
            {
                Logger.LogFailErrorMessage("Error Occured in CreateList", ex);
                Logger.InfoTrace("Error Occured in CreateList" + ex.ToString());
                return "Error:" + ex.ToString();
            }

            return "Error";

        }


        [HttpPost, ValidateHeaderAntiForgeryToken]
        public string EditList(ProtonModel proton)
        {
            ListInfo listInfo = new ListInfo();
            listInfo = proton.ListInfo;

            listInfo.ListItemDetails = JsonConvert.SerializeObject(proton.ListItemDetails);
            listInfo.OtherListInfo = JsonConvert.SerializeObject(proton.OtherListInfo);
            listInfo.ModifiedBy = User.Identity.Name;
            listInfo.Status = "Active";
            SetCategory(listInfo);


            listInfo.IsAnonymous = 1;

            bool isSuccess = listInfoBL.UpdateList(listInfo);

            if (isSuccess)
            {
                return Convert.ToString(listInfo.ListUrl.Replace("~/Proton/", "../"));
            }


            return "Error";

        }


        [HttpPost]
        public string UploadFile()
        {

            bool isSuccess = false;
            string filePath = string.Empty;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];

                try
                {
                    if (file.ContentLength > 0)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString("N") + System.IO.Path.GetExtension(file.FileName);
                        string _FileName = System.IO.Path.GetFileName(file.FileName);

                        string _path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), uniqueFileName);
                        file.SaveAs(_path);
                        isSuccess = true;
                        filePath = "/UploadedFiles/" + uniqueFileName;
                    }


                }
                catch (Exception ex)
                {
                    filePath = "Error";
                }
            }

            return filePath;

        }


        #endregion

        #region Helper

        public void SetCategory(ListInfo listInfo)
        {

            int subCategoryId = Convert.ToInt32(listInfo.Category);
            //listInfo.SubCategory = listInfo.Category;

            MasterDataBL masterBL = new MasterDataBL();

            List<Category> categories = masterBL.GetCategories();
            List<SubCategory> subCategories = masterBL.GetSubCategories();


            SubCategory subCategory = subCategories.Where(x => x.Id == subCategoryId).First();
            int categoryId = subCategory.ParentCategoryId;
            listInfo.SubCategory = subCategory.SubCategoryName;

            string categoryName = categories.Where(x => x.Id == categoryId).First().CategoryName;
            listInfo.Category = categoryName;


        }


        protected List<SelectListItem> GetCategorties(string selectedCategory)
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem { Text = "Choose Category", Disabled = true });

            List<SubCategory> categoryMasterList = (new MasterDataBL()).GetSubCategories();

            foreach (SubCategory sc in categoryMasterList)
            {
                bool isSelected = false;
                if (sc.SubCategoryName == selectedCategory)
                {
                    isSelected = true;

                }
                categories.Add(new SelectListItem
                {
                    Text = sc.SubCategoryName,
                    Value = Convert.ToString(sc.Id),
                    Selected = isSelected

                });
            }

            //  ViewBag.CategoryMaster = categories;

            //    categories.Where(x => x.Text == selectedCategory).First().Selected = true;

            return categories;

        }

        protected ProtonModel GetList(int Id)
        {
            ListInfo listInfo = listInfoBL.GetListInfo(Id);

            List<ListItemDetails> listItemDetails = JsonConvert.DeserializeObject<List<ListItemDetails>>(listInfo.ListItemDetails);
            OtherListInfo otherListInfo = JsonConvert.DeserializeObject<OtherListInfo>(listInfo.OtherListInfo);

            ProtonModel pm = new ProtonModel { ListInfo = listInfo, ListItemDetails = listItemDetails, OtherListInfo = otherListInfo };
            listInfoBL.IncrementView(Id);

            return pm;
        }


        #endregion

    }

}