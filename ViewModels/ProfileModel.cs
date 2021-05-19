using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WillThisWork.Data;
using WillThisWork.Models;

namespace WillThisWork.ViewModels
{
    public class ProfileModel
    {
        HttpClient client = new HttpClient();
        public string UserId { get; set; }

        public Dictionary<Champion, List<Hate>> ChampHateDict = new Dictionary<Champion, List<Hate>>();
        public List<Hate> Hates {get; set;}
        
        public List<Champion> Champions { get; set; }

        public List<SummonerChampion> SummonerChampions { get; set; }

        public SummonerChampion summonerChampion { get; set; }

      /*  public void getSC(int championId)
        {

            //SummonerChampion sc = await Task.Run(() => GetSummonerChampion(championId).Result);
            SummonerChampion sc = Task.Run(() => GetSummonerChampion(championId));
            summonerChampion = sc;
        }*/

     
    }

}