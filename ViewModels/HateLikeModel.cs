using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WillThisWork.Models
{
    public class HateLikeModel
    {

        public int Page { get; set; }
        public List<Hate> Hates { get; set; }
        public List<Likes> Likes { get; set; }
        public List<Dislikes> Dislikes { get; set; }
    }
}