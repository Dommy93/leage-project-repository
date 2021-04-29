using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WillThisWork.Data;

namespace WillThisWork.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Hate> Hates { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Dislikes> Dislikes { get; set; }
        public DbSet<Champion> Champions { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
           // Database.Connection.ConnectionString = @"Data Source=SQL5103.site4now.net;Initial Catalog=DB_A728D2_LHDB;User ID=db_a728d2_lhdb_admin;Password=Database123!;MultipleActiveResultSets=True";
            // Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-WillThisWork-20210401054935;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
            // Data Source=SQL5103.site4now.net;Initial Catalog=DB_A728D2_LHDB;User ID=DB_A728D2_LHDB_admin;Password=Database123!;MultipleActiveResultSets=True
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
            
        }


        public static ApplicationDbContext Create()
        {


            return new ApplicationDbContext();
        }
    }
}