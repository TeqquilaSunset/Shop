using AuthenticatorAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticatorAPI.Data
{
    /// <summary>
    /// Контекст базы данныых для работы с identity
    /// </summary>
    public class AuthContext : IdentityDbContext<User>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Запись ролей в базу данных, при её отсутсвии
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Надо как то по умнее сделать
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "DeliveryMan", NormalizedName = "DELIVERYMAN" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER" });
        }
    }
}
