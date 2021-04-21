using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WillThisWork.Models
{
    public class HateLikeModel
    {
        public List<Hate> Hates { get; set; }
        public List<UserLike> UserLikes { get; set; }
    }
}