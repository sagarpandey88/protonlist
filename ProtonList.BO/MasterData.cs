using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonList.BO
{

    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public List<SubCategory> SubCategories { get; set; }

    }

    public class SubCategory
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }

        public Category ParentCategory { get; set; }
        public int ParentCategoryId { get; set; }


    }

}
