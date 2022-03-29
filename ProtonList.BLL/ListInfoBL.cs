using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtonList.BO;
using ProtonList.DAL;


namespace ProtonList.BLL
{
    public class ListInfoBL
    {
        ListRepository listRepository = new ListRepository();

        /// <summary>
        /// 
        /// </summary>
        public void InsertLists(ListInfo listInfo)
        {

            listRepository.InsertLists(listInfo);

        }


        public ListInfo GetListInfo(int Id)
        {
            return listRepository.GetListById(Id).First();

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
            return listRepository.GetSearchResult(keyword, pageSize, lastIndex);

        }


        /// <summary>
        /// Gets lists by category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="pageSize">Maximum no of records to be fetched in one call</param>
        /// <param name="lastIndex"> Offset or start index of resultset if paged. 0 if first call</param>
        /// <returns></returns>
        public List<ListInfo> GetByCategory(string category, int pageSize, int lastIndex = 0)
        {
            return listRepository.GetByCategory(category, pageSize, lastIndex);

        }


        /// <summary>
        /// Gets lists by username
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageSize">Maximum no of records to be fetched in one call</param>
        /// <param name="lastIndex"> Offset or start index of resultset if paged. 0 if first call</param>
        /// <returns></returns>
        public List<ListInfo> GetByUserName(string userName, int pageSize, int lastIndex = 0)
        {
            return listRepository.GetByUserName(userName, pageSize, lastIndex);

        }


        public List<ListInfo> GetHomePageLists(string type, int maxRecords)
        {
            return listRepository.GetHomePageLists(type, maxRecords);

        }



        public void IncrementView(int Id)
        {

            listRepository.UpdateListAttribute(Id, "Views");


        }


        public bool UpdateList(ListInfo listInfo)
        {
            return listRepository.UpdateLists(listInfo);
        }


    }
}
