using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WillThisWork.Models
{
    public class CommentViewModel
    {
        public string Text { get; set; }
        public int EntityId { get; set; }
        public int RecordId { get; set; }
    }
}