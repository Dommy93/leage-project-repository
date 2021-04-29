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

            /*  Comment comment = new Comment(){ EntityId = 1, RecordId = 1, Text = "asdasdasdas", HateId = 1 };
              Hate hate = new Hate() { Number = 22, ImagePath = "~/Images/test1.gif", Title = "How to penta with Yasuo", Description = "Easy peasy OTP Yasuo! :D", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 2000 };
              Hate hate2 = new Hate() { Number = 26, ImagePath = "~/Images/test2.gif", Title = "The best Orianna ever!", Description = "I just one tapped them with my pro skills", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 5000 };
              Hate hate3 = new Hate() { Number = 28, Title = "Test 3", Description = "Blah blah blah weeeeeeeeeeeeeeeeeeeeeeeee!!!", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 1000 };

              context.Hates.Add(hate);
              context.Hates.Add(hate2);
              context.Hates.Add(hate3);



              context.SaveChanges();

  */

           // DirectoryInfo directory = new DirectoryInfo(@"C:\Users\Dominator\source\repos\WillThisWork\WillThisWork\Images\champion.json");

            var fileName = @"C:\Users\Dominator\source\repos\WillThisWork\WillThisWork\Images\champion.json";
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

            context.SaveChanges();

        }

    }
}