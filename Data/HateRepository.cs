using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WillThisWork.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace WillThisWork.Data
{
    public class HateRepository : Repository
    {

        HttpClient client = new HttpClient();
        public HateRepository(ApplicationDbContext context) : base(context)
        {

        }

        public HateLikeModel GetList()
        {
            HateLikeModel model = new HateLikeModel();
            model.Hates = _context.Hates.
                Include(a => a.Champion).
                Where(h => h.isWaitingRoom == false).
                OrderByDescending(h => h.Id).ToList();

            model.Likes = _context.Likes.ToList();
            model.Dislikes = _context.Dislikes.ToList();

            return model;
        }

        public HateLikeModel GetListPage(int? page)
        {
            if(page == null)
            {
                page = 0;
            }
            HateLikeModel model = new HateLikeModel();
            model.Hates = _context.Hates.
                Include(a => a.Champion).
                Where(h => h.isWaitingRoom == false).
                OrderByDescending(h => h.Id).Skip(page.Value * 10).Take(10).ToList();

            model.Likes = _context.Likes.ToList();
            model.Dislikes = _context.Dislikes.ToList();

            return model;
        }
        public HateLikeModel GetListWR()
        {
            HateLikeModel model = new HateLikeModel();
            model.Hates = _context.Hates.
                Include(c => c.Comments).
                Include(a => a.Champion).
                Where(h => h.isWaitingRoom == true).ToList();
            model.Likes = _context.Likes.ToList();
            model.Dislikes = _context.Dislikes.ToList();

            return model;
        }

        public Hate Get(int? id)
        {
            var hates = _context.Hates.
                Include(a => a.Champion).
                Include(u => u.User).
                Include(l => l.LikeList).
                Include(d => d.DislikeList).AsQueryable();
           
            return hates.Where(h => h.Id == id).SingleOrDefault();
        }

        public void Add(Hate hate)
        {
            _context.Hates.Add(hate);
            _context.SaveChanges();
        }
      
        public bool LeaveComment(Comment comment)
        {

            _context.Comments.Add(comment);

            return _context.SaveChanges() > 0;
        }
        public List<Comment> GetComments(int entityId, int hateId)
        {
            List<Comment> comments = new List<Comment>();


            comments = _context.Comments.Where(b => b.EntityId == entityId && b.HateId == hateId).ToList();

            return comments;
        }

        public void Like(int id)
        {


            var hate = _context.Hates.Where(a => a.Id == id).SingleOrDefault();
            Likes likes = new Likes { HateId = id, UserName = System.Web.HttpContext.Current.User.Identity.Name };
            var userLikesOfHate = _context.Likes.
                Where(u => u.HateId == id).
                Where(a => a.UserName == likes.UserName).ToList();


            if (userLikesOfHate.Count == 0)
            {
                hate.Likes++;
                hate.LikeList.Add(likes);
                _context.SaveChanges();

            }
            else
            {
                hate.Likes--;
                _context.Likes.Remove(userLikesOfHate.SingleOrDefault());
                _context.SaveChanges();
            }

        }

        public void Dislike(int id)
        {
            var hate = _context.Hates.Where(a => a.Id == id).SingleOrDefault();
            Dislikes dislikes = new Dislikes { HateId = id, UserName = System.Web.HttpContext.Current.User.Identity.Name };
            var userLikesOfHate = _context.Dislikes.
                 Where(u => u.HateId == id).
                 Where(a => a.UserName == dislikes.UserName).ToList();

            if (userLikesOfHate.Count == 0)
            {
                hate.Dislikes++;
                hate.DislikeList.Add(dislikes);
                _context.SaveChanges();

            }
            else
            {
                hate.Dislikes--;
                _context.Dislikes.Remove(userLikesOfHate.SingleOrDefault());
                _context.SaveChanges();
            }
        }

        public void Update(UpdateHateViewModel hate)
        {
            Hate hate2 = _context.Hates.Find(hate.Id);
            _context.Entry(hate2).CurrentValues.SetValues(hate);
            _context.SaveChanges();
        }

         public void AddToMain(int? id)
        {

            _context.Hates.Where(h => h.Id == id).SingleOrDefault().isWaitingRoom = false;
            _context.SaveChanges();

        }


        public Dictionary<Champion, List<Hate>> getChampHates(string userId)
        {
            Dictionary<Champion, List<Hate>> dict = new Dictionary<Champion, List<Hate>>();

            List<Champion> champs = _context.Hates.Where(h => h.UserId == userId).Select(s => s.Champion).Distinct().ToList();
            //List<Champion> champs2 = _context.Hates.GroupBy(ch => ch.ChampionId).Select(grp => grp.First()).ToList();
            var hates = _context.Hates.
                Where(h => h.UserId == userId).
                Include(c => c.Comments).ToList();


            

            foreach (var champ in champs) {
                var list = new List<Hate>();    
                foreach (var hate in hates)
                {
                    if(champ.Name == hate.Champion.Name)
                    {
                        list.Add(hate);
                    }


                }
                dict.Add(champ, list);
            }

            return dict;
        }


        public async Task<List<SummonerChampion>> GetSummonerChampions(string summonerName)
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-f7fd2c73-5098-43a2-b767-1a048b41e99b");
                var summonerName2 = "Gillberg";
                HttpResponseMessage response = await client.GetAsync($"https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName2}");

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                RiotSummoner summoner = JsonConvert.DeserializeObject<RiotSummoner>(responseBody);
                string summonerId = summoner.id;


                HttpResponseMessage response3 = await client.GetAsync($"https://eun1.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{summonerId}");
                string response3Body = await response3.Content.ReadAsStringAsync();


                List<SummonerChampion> summonerChampions = JsonConvert.DeserializeObject<List<SummonerChampion>>(response3Body);

                return summonerChampions;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public async Task<SummonerChampion> GetSummonerChampion(int id)
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-f7fd2c73-5098-43a2-b767-1a048b41e99b");
                var summonerName2 = "Gillberg";
                HttpResponseMessage response = await client.GetAsync($"https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName2}");

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                RiotSummoner summoner = JsonConvert.DeserializeObject<RiotSummoner>(responseBody);
                string summonerId = summoner.id;


                HttpResponseMessage response3 = await client.GetAsync($"https://eun1.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{summonerId}/by-champion/{id}");
                string response3Body = await response3.Content.ReadAsStringAsync();

                SummonerChampion champion = JsonConvert.DeserializeObject<SummonerChampion>(response3Body);

                return champion;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


    }
}