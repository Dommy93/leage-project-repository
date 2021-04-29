using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public List<Likes> LikeList { get; set; } = new List<Likes>();
        public List<Dislikes> DislikeList { get; set; } = new List<Dislikes>();

        public bool isWaitingRoom { get; set; } = true;

        public List<Comment> Comments { get; set; }

        public int ChampionId { get; set; }
        public Champion Champion { get; set; }




        public Hate()
        {
            
        }

     

    }
}