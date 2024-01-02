using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Identity;

namespace PharmaStore.Data.Seeder
{
    public static class DataSeeder
    {
        private static Guid adminRoleId;
        private static Guid customerRoleId;
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedRole(modelBuilder);
            SeedDefaultAdminUser(modelBuilder);
            SeedDefaultCustomerUser(modelBuilder);
        }
        public static void SeedRole(ModelBuilder modelBuilder)
        {
            adminRoleId = Guid.NewGuid();
            customerRoleId = Guid.NewGuid();
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole()
                {
                    Id = adminRoleId.ToString(),
                    Name = "Admin",
                    ConcurrencyStamp = "1",
                    NormalizedName = "ADMIN"
                },
                new ApplicationRole()
                {
                    Id = customerRoleId.ToString(),
                    Name = "Customer",
                    ConcurrencyStamp = "2",
                    NormalizedName = "CUSTOMER"
                }
            );
        }
        public static void SeedDefaultAdminUser(ModelBuilder modelBuilder)
        {
            var userId = Guid.NewGuid();
            ApplicationUser user = new ApplicationUser()
            {
                Id = userId.ToString(),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                LockoutEnabled = true,
                PhoneNumber = "9815939112",
                Address = "BTM"
            };
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");
            modelBuilder.Entity<ApplicationUser>().HasData(user);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    UserId = userId.ToString(),
                    RoleId = adminRoleId.ToString()
                }
            );
        }
        public static void SeedDefaultCustomerUser(ModelBuilder modelBuilder)
        {
            var userId = Guid.NewGuid();
            ApplicationUser user = new ApplicationUser()
            {
                Id = userId.ToString(),
                UserName = "Customer",
                NormalizedUserName = "CUSTOMER",
                Email = "customer@customer.com",
                LockoutEnabled = true,
                PhoneNumber = "9815939112",
                Address = "KVT"
            };
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Customer@123");
            modelBuilder.Entity<ApplicationUser>().HasData(user);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    UserId = userId.ToString(),
                    RoleId = customerRoleId.ToString()
                }
            );
        }
    }
}
