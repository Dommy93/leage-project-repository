using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WillThisWork.Models
{
    public class SummonerChampion
    {
        public int championId { get; set; }
        public int championLevel { get; set; }
        public int championPoints { get; set; }
        public long lastPlayTime { get; set; }
        public int championPointsSinceLastLevel { get; set; }
        public int championPointsUntilNextLevel { get; set; }
        public bool chestGranted { get; set; }
        public int tokensEarned { get; set; }
        public string summonerId { get; set; }


    }
}