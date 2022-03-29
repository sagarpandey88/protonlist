using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtonList.BO;
using ProtonList.DAL;

namespace ProtonList.BLL
{
   public class MasterDataBL
    {

        public List<Category> GetCategories()
        {
            return (new MasterDataRepository()).GetCategoryMaster();
        }



        public List<SubCategory> GetSubCategories()
        {
            return (new MasterDataRepository()).GetSubCategoryMaster();
        }
    }
}
