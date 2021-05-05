using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.IO;

namespace WillThisWork.Models
{
    internal class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {

              


  

            // DirectoryInfo directory = new DirectoryInfo(@"C:\Users\Dominator\source\repos\WillThisWork\WillThisWork\Images\champion.json");

            var fileName = @"C:\Users\Dominator\source\repos\WillThisWork\WillThisWork\Images\champion.json";
            //var fileName = @"h:\root\home\gillberg-001\www\lh\images\champion.json";
            var chamion = new Champ();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
            {
                chamion = serializer.Deserialize<Champ>(jsonReader);
            }

            var champToFillList = chamion.ChampsDictionary;


            foreach (var champ in champToFillList)
            {
                Champion champion = new Champion();
                champion.Name = champ.Value.name;
                context.Champions.Add(champion);
            }

            IdentityRole admin = new IdentityRole() { Name = "Administrator" };
            IdentityRole moderator = new IdentityRole() { Name = "Moderator" };
            IdentityRole user = new IdentityRole() { Name = "User" };

            context.Roles.Add(admin);
            context.Roles.Add(moderator);
            context.Roles.Add(user);

            for(int i = 0; i < 50; i++)
            {
                Hate hate = new Hate() { Number = i, ImagePath = "~/Images/test1.gif", Title = "How to penta with Yasuo", Description = "Easy peasy OTP Yasuo! :D", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 2000, ChampionId = 1, isWaitingRoom = false };
                context.Hates.Add(hate);
            }

          
            Hate hate2 = new Hate() { Number = 26, ImagePath = "~/Images/test2.gif", Title = "The best Orianna ever!", Description = "I just one tapped them with my pro skills", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 5000, ChampionId = 1 , isWaitingRoom = false };
            Hate hate3 = new Hate() { Number = 28, Title = "Test 3", Description = "Blah blah blah weeeeeeeeeeeeeeeeeeeeeeeee!!!", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 1000, ChampionId = 1 , isWaitingRoom = false };

          
            context.SaveChanges();

        }

    }
}