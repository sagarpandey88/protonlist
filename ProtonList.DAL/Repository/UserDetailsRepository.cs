using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonList.DAL
{
    public class UserDetailsRepository
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
        public UserDetailsRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ListsConnectionString"].ConnectionString;

        }
        #endregion



        //public List<Userd=> GetUserDetails()
        //{



        //    Database db = objDB = new SqlDatabase(ConnectionString);
        //    DbCommand dbCommand = db.GetStoredProcCommand("usp_GetUserDetailsByUserName");
        //    // dbCommand.Parameters.Add();
        //    db.AddInParameter(dbCommand, "@TableName", DbType.String, "SubCategory");
        //    db.AddInParameter(dbCommand, "MaxRecords", DbType.Int32, 1000);


        //    List<SubCategory> subCategoryList = new List<SubCategory>();
        //    using (DataSet dataset = db.ExecuteDataSet(dbCommand))
        //    {
        //        DataTable dtSubCategory = dataset.Tables[0];
        //        foreach (DataRow dr in dtSubCategory.Rows)
        //        {

        //            SubCategory sb = new SubCategory
        //            {
        //                Id = Convert.ToInt32(dr["Id"]),
        //                SubCategoryName = Convert.ToString(dr["SubCategoryName"]),
        //                ParentCategoryId = Convert.ToInt32(dr["CategoryId"])

        //            };
        //            subCategoryList.Add(sb);

        //        }



        //    }


        //    //usp_GetMasterData
        //    return subCategoryList;
        //}
    }
}
