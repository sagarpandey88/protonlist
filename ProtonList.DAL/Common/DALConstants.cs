using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonList.DAL.Common
{
    public class DALConstants
    {
        public class StoredProcedures
        {

            public const string usp_GetListItemById = "usp_GetListItemById";
            public const string usp_InsertListItem = "usp_InsertListItem";
            public const string usp_UpdateListItem = "usp_UpdateListItem";
            public const string usp_SearchLists = "usp_SearchLists";
            public const string usp_GetListsByCategory = "usp_GetListsByCategory";
            public const string usp_GetListsByUserName = "usp_GetListsByUserName";
            public const string usp_GetHomePageLists = "usp_GetHomePageLists";
            public const string usp_UpdateListAttributes = "usp_UpdateListAttributes";

        }


        public class Tables
        {
            public const string Lists = "Lists";
            public const string CategoryMaster = "CategoryMaster";
            public const string DisplayTemplateMaster = "DisplayTemplateMaster";
            public const string PLSettings = "PLSettings";
            public const string StatusMaster = "StatusMaster";
            public const string SubCategoryMaster = "SubCategoryMaster";


            public class ListsColumns
            {
                public const string Id = "Id";
                public const string ListTitle = "ListTitle";
                public const string CreatedBy = "CreatedBy";
                public const string CreatedDate = "CreatedDate";
                public const string ModifiedDate = "ModifiedDate";
                public const string ModifiedBy = "ModifiedBy";
                public const string IsAnonymous = "IsAnonymous";
                public const string Passcode = "Passcode";
                public const string ListData = "ListData";
                public const string Status = "Status";
                public const string Category = "Category";
                public const string SubCategory = "SubCategory";
                public const string Tags = "Tags";
                public const string IsPublic = "IsPublic";
                public const string ListId = "ListId";
                public const string OtherInfo = "OtherInfo";
                public const string Views = "Views";
                public const string Likes = "Likes";
                public const string Dislikes = "Dislikes";


            }


            public class PLSettingsColumns
            {
                public const string Id = "Id";
                public const string Key = "Key";
                public const string Value = "Value";
                public const string Remarks = "Remarks";
            }

            public class CategoryMasterColumns
            {

                public const string Id = "Id";
                public const string CategoryName = "CategoryName";

            }

            public class DisplayTemplateMasterColumns
            {

                public const string Id = "Id";
                public const string TemplateName = "TemplateName";

            }

            public class StatusMasterColumns
            {

                public const string Id = "Id";
                public const string Status = "Status";

            }

            public class SubCategoryMasterColumns
            {

                public const string Id = "Id";
                public const string SubCategoryName = "SubCategoryName";
                public const string CategoryId = "CategoryId";

            }


        }

    }
}
