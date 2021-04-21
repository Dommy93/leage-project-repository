using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WillThisWork.Models
{
    public class UserLike
    {

        public int Id { get; set; }
        public int HateId { get; set; }
        public string UserName { get; set; }

        public UserLike()
        {

        }

     
    }
}