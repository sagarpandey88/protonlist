using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using ProtonList.BO;

namespace ProtonList.DAL
{
    public class MasterDataRepository
    {

        #region Variable  
        /// <summary>  
        /// Specify the Database variable  
        /// </summary>  
        Database objDB;
        /// <summary>  
        /// Specify the static variable  
        /// </summary>  
        static string ConnectionString;
        #endregion

        #region Constructor  
        /// <summary>  
        /// This constructor is used to get the connectionstring from the config file  
        /// </summary>  
        public MasterDataRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ListsConnectionString"].ConnectionString;

        }
        #endregion


        public List<Category> GetCategoryMaster()
        {
            List<Category> categoryList = new List<Category>();


            Database db = objDB = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_GetMasterData");
            // dbCommand.Parameters.Add();
            db.AddInParameter(dbCommand, "@TableName", DbType.String, "Category");
            db.AddInParameter(dbCommand, "MaxRecords", DbType.Int32, 1000);



            using (DataSet dataset = db.ExecuteDataSet(dbCommand))
            {

                DataTable dtCategory = dataset.Tables[0];
                DataTable dtSubCategory = dataset.Tables[1];
                foreach (DataRow dr in dtCategory.Rows)
                {
                    Category category = new Category();
                    category.Id = Convert.ToInt32(dr["Id"]);
                    category.CategoryName = Convert.ToString(dr["CategoryName"]);
                    category.SubCategories = new List<SubCategory>();

                    foreach (DataRow drSc in dtSubCategory.Select("CategoryId=" + category.Id))
                    {
                        SubCategory sb = new SubCategory
                        {
                            Id = Convert.ToInt32(drSc["Id"]),
                            SubCategoryName = Convert.ToString(drSc["SubCategoryName"]),
                            ParentCategoryId = category.Id

                        };

                        category.SubCategories.Add(sb);
                    }

                    categoryList.Add(category);

                }


            }




            //usp_GetMasterData
            return categoryList;
        }


        public List<SubCategory> GetSubCategoryMaster()
        {



            Database db = objDB = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_GetMasterData");
            // dbCommand.Parameters.Add();
            db.AddInParameter(dbCommand, "@TableName", DbType.String, "SubCategory");
            db.AddInParameter(dbCommand, "MaxRecords", DbType.Int32, 1000);


            List<SubCategory> subCategoryList = new List<SubCategory>();
            using (DataSet dataset = db.ExecuteDataSet(dbCommand))
            {
                DataTable dtSubCategory = dataset.Tables[0];
                foreach (DataRow dr in dtSubCategory.Rows)
                {

                    SubCategory sb = new SubCategory
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        SubCategoryName = Convert.ToString(dr["SubCategoryName"]),
                        ParentCategoryId = Convert.ToInt32(dr["CategoryId"])

                    };
                    subCategoryList.Add(sb);

                }



            }







            //usp_GetMasterData
            return subCategoryList;
        }

    }
}
