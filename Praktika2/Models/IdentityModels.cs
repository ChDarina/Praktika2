using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Praktika2.Models
{
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Illustrations> Illustrations { get; set; }
        public virtual DbSet<Illustrators> Illustrators { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<SocialMediaCustomers> SocialMediaCustomers { get; set; }
        public virtual DbSet<SocialMediaIllustrators> SocialMediaIllustrators { get; set; }
        public virtual DbSet<SocialMedias> SocialMedias { get; set; }
        public virtual DbSet<Technics> Technics { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}