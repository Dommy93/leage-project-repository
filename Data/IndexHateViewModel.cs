using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WillThisWork.Models;

namespace WillThisWork.Data
{
    public class IndexHateViewModel
    {
        public List<Hate> Hates { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}