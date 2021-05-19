using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillThisWork.Data;

namespace WillThisWork.Models
{
    public class HateAddModel
    {
    
        public Hate Hate { get; set; } = new Hate();

       // public int ChampionId { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public SelectList ChampionSelectListItems { get; set; }

        public virtual void Init(Repository repository)
        {
            ChampionSelectListItems = new SelectList(repository.getChampList().Items, "ChampId", "Name");
        }

    }
}