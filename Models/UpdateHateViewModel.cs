using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillThisWork.Data;

namespace WillThisWork.Models
{
    public class UpdateHateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HatedSummoner { get; set; }

        public int ChampionId { get; set; }

        public SelectList ChampionSelectListItems { get; set; }

        public virtual void Init(Repository repository)
        {
            ChampionSelectListItems = new SelectList(repository.getChampList().Items, "ChampionId", "Name", "");
        }

    }
}