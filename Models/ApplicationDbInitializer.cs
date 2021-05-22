using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;

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
                champion.ChampionId = Int32.Parse(champ.Value.key); // fix for championId not auto increment
                context.Champions.Add(champion);
            }

            IdentityRole admin = new IdentityRole() { Name = "Administrator" };
            IdentityRole moderator = new IdentityRole() { Name = "Moderator" };
            IdentityRole user = new IdentityRole() { Name = "User" };

            context.Roles.Add(admin);
            context.Roles.Add(moderator);
            context.Roles.Add(user);

           // Champion champe = context.Champions.Where(a => a.ChampionId == 266).FirstOrDefault();


            for(int i = 0; i < 10; i++)
            {
               /* Hate hate = new Hate() { Number = i, ImagePath = "~/Images/test1.gif", Title = "How to penta with Yasuo", Description = "Easy peasy OTP Yasuo! :D", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 2000, ChampionId = 266, isWaitingRoom = false };*/
                //context.Hates.Add(hate);
            }

          
    

          
            context.SaveChanges();

        }

    }
}