using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonList.BO
{
    public class ListInfo
    {
        public int Views { get; set; }
        public int Likes { get; set; }
        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public string ListTitle { get; set; }
        public string ListUrl { get; set; }

        public string Tags { get; set; }

        public int Id { get; set; }

        public string ListItemDetails { get; set; }

        public string Status { get; set; }

        public string SubCategory { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int IsAnonymous { get; set; }

        public string Passcode { get; set; }

        public int IsPublic { get; set; }

        public string ListId { get; set; }

        public string OtherListInfo { get; set; }

    }



    public class ListItemDetails
    {
        public int Id { get; set; }
        public string ItemTitle { get; set; }
        public string LinkUrl { get; set; }

        public string ItemDescription { get; set; }

        public string ItemImageUrl { get; set; }

    }

    public class UserInfo
    {
        public string Title { get; set; }

        public int Likes { get; set; }

        public int Views { get; set; }

        public string ProfilePictureUrl { get; set; }
    }


    public class OtherListInfo
    {
        public string FeaturedImageUrl { get; set; }
        public bool IsListNoRequired { get; set; }
    }


    public class ListResultSet
    {
        public List<ListInfo> ListInfo { get; set; }
        public int TotalNoOfItems { get; set; }
    }
}
