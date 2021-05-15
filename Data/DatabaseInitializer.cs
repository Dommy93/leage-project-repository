using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WillThisWork.Models;

namespace WillThisWork.Data
{
    internal class DatabaseInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        
        protected override void Seed(ApplicationDbContext context)
        {
            /*
                        Hate hate = new Hate() { Number = 22, ImagePath = "~/Images/test1.gif", Title = "How to penta with Yasuo", Description = "Easy peasy OTP Yasuo! :D", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 2000 };
                        Hate hate2 = new Hate() { Number = 26, ImagePath = "~/Images/test2.gif", Title = "The best Orianna ever!", Description = "I just one tapped them with my pro skills", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 5000 };
                        Hate hate3 = new Hate() { Number = 28, Title = "Test 3", Description = "Blah blah blah weeeeeeeeeeeeeeeeeeeeeeeee!!!", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 1000 };

                        context.Hates.Add(hate);
                        context.Hates.Add(hate2);
                        context.Hates.Add(hate3);


            */

            
            IdentityRole admin = new IdentityRole() { Name = "Administrator", Id = "69fb1bea-7d57-436f-8e87-b3c3dc6f7449" };
            IdentityRole moderator = new IdentityRole() { Name = "Moderator" };
            IdentityRole user = new IdentityRole() { Name = "User" };

            context.Roles.Add(admin);
            context.Roles.Add(moderator);
            context.Roles.Add(user);



            Hate hate = new Hate() { Number = 22, ImagePath = "~/Images/test1.gif", Title = "How to penta with Yasuo", Description = "Easy peasy OTP Yasuo! :D", HatedSummoner = "Hated Summoners", Dislikes = 100, Likes = 2000 };
           // context.WaitingRoom.Add(hate);
            
            context.SaveChanges();



        }

      


    }
}