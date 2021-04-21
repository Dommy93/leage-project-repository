using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WillThisWork.Models
{
    public class Comment
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public DateTime TimeStamp { get; set; }
        public int HateId { get; set; }
        public int EntityId { get; set; }
        public int RecordId { get; set; }

    }
}