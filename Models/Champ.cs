using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WillThisWork.Models
{
    public class Champ
    {
        [JsonProperty(PropertyName = "data")]
        public Dictionary<string, Champs> ChampsDictionary = new Dictionary<string, Champs>();



    }
    public class Champs
    {
        public string version { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string blurb { get; set; }
        public Info info { get; set; }
        public Image image { get; set; }
        public List<string> tags { get; set; }
        public string partype { get; set; }
        public Stats stats { get; set; }
    }




    public class Info
    {
        public int attack { get; set; }
        public int defense { get; set; }
        public int magic { get; set; }
        public int difficulty { get; set; }
    }

    public class Image
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Stats
    {

    }

}
