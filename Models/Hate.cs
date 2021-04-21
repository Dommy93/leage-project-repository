using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WillThisWork.Models
{
    public class Hate
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public string HatedSummoner { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
        public string ImagePath { get; set; }
        public string fileExtension { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public List<UserLike> UserLikes { get; set; } = new List<UserLike>(); 
        public List<Comment> Comments { get; set; }
        public Hate()
        {
            
        }
     

    }
}