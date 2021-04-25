using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WillThisWork.Models
{
    public class UpdateHateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HatedSummoner { get; set; }
    }
}