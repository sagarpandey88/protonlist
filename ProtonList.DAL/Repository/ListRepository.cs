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
using Newtonsoft.Json;
using ProtonList.BO;

namespace ProtonList.DAL
{
    public class ListRepository
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
        public ListRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ListsConnectionString"].ConnectionString;

        }
        #endregion

        public List<ListInfo> GetListById(int id)
        {

            Database db = objDB = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_GetListItemById");
            // dbCommand.Parameters.Add();
            db.AddInParameter(dbCommand, "Id", DbType.Int32, id);

            List<ListInfo> list = new List<ListInfo>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    ListInfo obj = new ListInfo();

                    MapGetListTableColumns(dataReader, obj);


                    list.Add(obj);

                }

            }

            return list;

        }


        public bool InsertLists(ListInfo listInfo)
        {
            bool isSuccess = false;
            Database db = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_InsertListItem");

            db.AddInParameter(dbCommand, "ListTitle", DbType.String, listInfo.ListTitle);
            db.AddInParameter(dbCommand, "CreatedBy", DbType.String, listInfo.CreatedBy);
            //db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, listInfo.CreatedDate);
            //db.AddInParameter(dbCommand, "ModifiedDate", DbType.DateTime, listInfo.ModifiedDate);
            db.AddInParameter(dbCommand, "ModifiedBy", DbType.String, listInfo.ModifiedBy);
            db.AddInParameter(dbCommand, "IsAnonymous", DbType.Int32, listInfo.IsAnonymous);
            db.AddInParameter(dbCommand, "Passcode", DbType.String, listInfo.Passcode);
            db.AddInParameter(dbCommand, "ListData", DbType.String, listInfo.ListItemDetails);
            db.AddInParameter(dbCommand, "Status", DbType.String, listInfo.Status);
            db.AddInParameter(dbCommand, "Category", DbType.String, listInfo.Category);
            db.AddInParameter(dbCommand, "SubCategory", DbType.String, listInfo.SubCategory);
            db.AddInParameter(dbCommand, "Tags", DbType.String, listInfo.Tags);
            db.AddInParameter(dbCommand, "IsPublic", DbType.Int32, listInfo.IsPublic);
            db.AddInParameter(dbCommand, "ListId", DbType.String, listInfo.Id);
            db.AddInParameter(dbCommand, "OtherInfo", DbType.String, listInfo.OtherListInfo);


            using (DataSet ds = db.ExecuteDataSet(dbCommand))
            {

                isSuccess = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSuccess"]);
                listInfo.Id = Convert.ToInt32(ds.Tables[1].Rows[0]["GeneratedListId"]);

                //if (dataReader["IsSuccess"] != DBNull.Value) { isSuccess = Convert.ToBoolean(dataReader["IsSuccess"]); }
                //if (dataReader["GeneratedListId"] != DBNull.Value) { listInfo.Id = (int)dataReader["GeneratedListId"]; }
            }

            listInfo.ListUrl = GetListUrl(listInfo.ListTitle, listInfo.Id);

            return isSuccess;
        }


        public bool UpdateLists(ListInfo listInfo)
        {
            Database db = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_UpdateListItem");
            db.AddInParameter(dbCommand, "id", DbType.Int32, listInfo.Id);
            db.AddInParameter(dbCommand, "ListTitle", DbType.String, listInfo.ListTitle);
            //db.AddInParameter(dbCommand, "CreatedBy", DbType.String, listInfo.CreatedBy);
            //db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, listInfo.CreatedDate);
            //db.AddInParameter(dbCommand, "ModifiedDate", DbType.DateTime, listInfo.ModifiedDate);
            db.AddInParameter(dbCommand, "ModifiedBy", DbType.String, listInfo.ModifiedBy);
            db.AddInParameter(dbCommand, "IsAnonymous", DbType.Int32, listInfo.IsAnonymous);
            db.AddInParameter(dbCommand, "Passcode", DbType.String, listInfo.Passcode);
            db.AddInParameter(dbCommand, "ListData", DbType.String, listInfo.ListItemDetails);
            db.AddInParameter(dbCommand, "Status", DbType.String, listInfo.Status);
            db.AddInParameter(dbCommand, "Category", DbType.String, listInfo.Category);
            db.AddInParameter(dbCommand, "SubCategory", DbType.String, listInfo.SubCategory);
            db.AddInParameter(dbCommand, "Tags", DbType.String, listInfo.Tags);
            db.AddInParameter(dbCommand, "IsPublic", DbType.Int32, listInfo.IsPublic);
            db.AddInParameter(dbCommand, "ListId", DbType.String, listInfo.Id);
            db.AddInParameter(dbCommand, "OtherInfo", DbType.String, listInfo.OtherListInfo);

            listInfo.ListUrl = GetListUrl(listInfo.ListTitle, listInfo.Id);

            bool isSuccess = false;
            using (DataSet ds = db.ExecuteDataSet(dbCommand))
            {

                isSuccess = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSuccess"]);

            }

            return isSuccess;
        }




        /// <summary>
        /// Gets search results
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize">Maximum no of records to be fetched in one call</param>
        /// <param name="lastIndex"> Offset or start index of resultset if paged. 0 if first call</param>
        /// <returns></returns>
        public ListResultSet GetSearchResult(string keyword, int pageSize, int lastIndex = 0)
        {

            ListResultSet resultSet = new ListResultSet();

            Database db = objDB = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_SearchLists");
            // dbCommand.Parameters.Add();
            db.AddInParameter(dbCommand, "Keyword", DbType.String, keyword);
            db.AddInParameter(dbCommand, "MaxRecords", DbType.Int32, pageSize);
            db.AddInParameter(dbCommand, "OffSet", DbType.Int32, lastIndex);

            List<ListInfo> list = new List<ListInfo>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    ListInfo obj = new ListInfo();

                    MapGetListTableColumns(dataReader, obj);


                    list.Add(obj);

                }
                resultSet.ListInfo = list;

                if (dataReader.NextResult())
                {
                    while (dataReader.Read())
                    {
                        if (dataReader["TotalSearchResults"] != DBNull.Value) { resultSet.TotalNoOfItems = (int)dataReader["TotalSearchResults"]; }
                    }

                }
            }

            return resultSet;

        }



        public List<ListInfo> GetByCategory(string category, int pageSize, int lastIndex = 0)
        {
            Database db = objDB = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_GetListsByCategory");
            // dbCommand.Parameters.Add();
            db.AddInParameter(dbCommand, "Category", DbType.String, category);
            db.AddInParameter(dbCommand, "MaxRecords", DbType.Int32, pageSize);
            db.AddInParameter(dbCommand, "OffSet", DbType.Int32, lastIndex);

            List<ListInfo> list = new List<ListInfo>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    ListInfo obj = new ListInfo();

                    MapGetListTableColumns(dataReader, obj);


                    list.Add(obj);

                }

            }

            return list;

        }


        public List<ListInfo> GetByUserName(string userName, int pageSize, int lastIndex = 0)
        {
            Database db = objDB = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_GetListsByUserName");
            // dbCommand.Parameters.Add();
            db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
            db.AddInParameter(dbCommand, "MaxRecords", DbType.Int32, pageSize);
            db.AddInParameter(dbCommand, "OffSet", DbType.Int32, lastIndex);

            List<ListInfo> list = new List<ListInfo>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    ListInfo obj = new ListInfo();

                    MapGetListTableColumns(dataReader, obj);


                    list.Add(obj);

                }

            }

            return list;

        }


        protected void MapGetListTableColumns(IDataReader dataReader, ListInfo obj)
        {
            if (dataReader["Id"] != DBNull.Value) { obj.Id = (int)dataReader["Id"]; }
            if (dataReader["ListTitle"] != DBNull.Value) { obj.ListTitle = (string)dataReader["ListTitle"]; }
            if (dataReader["CreatedBy"] != DBNull.Value) { obj.CreatedBy = (string)dataReader["CreatedBy"]; }
            if (dataReader["CreatedDate"] != DBNull.Value) { obj.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
            if (dataReader["ModifiedDate"] != DBNull.Value) { obj.ModifiedDate = (DateTime)dataReader["ModifiedDate"]; }
            if (dataReader["ModifiedBy"] != DBNull.Value) { obj.ModifiedBy = (string)dataReader["ModifiedBy"]; }
            if (dataReader["IsAnonymous"] != DBNull.Value) { obj.IsAnonymous = (int)dataReader["IsAnonymous"]; }
            if (dataReader["Passcode"] != DBNull.Value) { obj.Passcode = (string)dataReader["Passcode"]; }
            if (dataReader["ListData"] != DBNull.Value) { obj.ListItemDetails = (string)dataReader["ListData"]; }
            if (dataReader["Status"] != DBNull.Value) { obj.Status = (string)dataReader["Status"]; }
            if (dataReader["Category"] != DBNull.Value) { obj.Category = (string)dataReader["Category"]; }
            if (dataReader["SubCategory"] != DBNull.Value) { obj.SubCategory = (string)dataReader["SubCategory"]; }
            if (dataReader["Tags"] != DBNull.Value) { obj.Tags = (string)dataReader["Tags"]; }
            if (dataReader["IsPublic"] != DBNull.Value) { obj.IsPublic = (int)dataReader["IsPublic"]; }
            if (dataReader["ListId"] != DBNull.Value) { obj.ListId = (string)dataReader["ListId"]; }
            if (dataReader["OtherInfo"] != DBNull.Value) { obj.OtherListInfo = (string)dataReader["OtherInfo"]; }
            if (dataReader["Views"] != DBNull.Value) { obj.Views = (int)dataReader["Views"]; }
            if (dataReader["Likes"] != DBNull.Value) { obj.Likes = (int)dataReader["Likes"]; }
            // if (dataReader["Views"] != DBNull.Value) { obj.dis = (int)dataReader["Views"]; }

            if (!string.IsNullOrEmpty(obj.OtherListInfo))
            {
                OtherListInfo otherListInfo = JsonConvert.DeserializeObject<OtherListInfo>(obj.OtherListInfo);
                obj.ImageUrl = otherListInfo.FeaturedImageUrl;
            }
            // obj.ListUrl = "ViewProton/" + obj.Id;
            //string updatedUrl = System.Text.RegularExpressions.Regex.Replace(obj.ListTitle, "[^0-9A-Za-z]+", "-");
            //if (updatedUrl.Length > 50)
            //{
            //    updatedUrl = updatedUrl.Substring(0, 50);
            //}
            //updatedUrl += "-Id=" + obj.Id;
            obj.ListUrl = GetListUrl(obj.ListTitle, obj.Id);  // "~/Proton/Show/" + updatedUrl;


        }


        public List<ListInfo> GetHomePageLists(string type, int maxRecords)
        {

            Database db = objDB = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_GetHomePageLists");
            db.AddInParameter(dbCommand, "Type", DbType.String, type);
            db.AddInParameter(dbCommand, "MaxRecords", DbType.Int32, maxRecords);


            List<ListInfo> list = new List<ListInfo>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    ListInfo obj = new ListInfo();

                    MapGetListTableColumns(dataReader, obj);


                    list.Add(obj);

                }

            }

            return list;


        }


        public void UpdateListAttribute(int Id, string attributeType, string value = null)
        {

            Database db = objDB = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_UpdateListAttributes");
            db.AddInParameter(dbCommand, "Id", DbType.Int32, Id);
            db.AddInParameter(dbCommand, "AttributeType", DbType.String, attributeType);
            if (value != null)
            {
                db.AddInParameter(dbCommand, "AttributeType", DbType.String, attributeType);
            }



            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                //while (dataReader.Read())
                //{

                //    ListInfo obj = new ListInfo();

                //    // MapGetListTableColumns(dataReader, obj);

                //    list.Add(obj);

                //}

            }

            // return list;


        }


        protected string GetListUrl(string listTitle, int Id)
        {
            string updatedUrl = System.Text.RegularExpressions.Regex.Replace(listTitle, "[^0-9A-Za-z]+", "-");
            if (updatedUrl.Length > 50)
            {
                updatedUrl = updatedUrl.Substring(0, 50);
            }
            updatedUrl += "-Id=" + Id;

            return "~/Proton/Show/" + updatedUrl;
        }
    }
}
