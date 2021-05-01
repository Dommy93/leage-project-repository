using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WillThisWork.Models;

namespace WillThisWork.ViewModels
{
    public class ProfileModel
    {

        public string UserId { get; set; }

        public Dictionary<Champion, List<Hate>> ChampHateDict = new Dictionary<Champion, List<Hate>>();
        public List<Hate> Hates {get; set;}

        public List<Champion> Champions { get; set; }

    }
}