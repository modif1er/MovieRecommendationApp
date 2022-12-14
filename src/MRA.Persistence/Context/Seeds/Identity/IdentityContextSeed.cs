using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MRA.Domain.Entities;

namespace MRA.Persistence.Context.Seeds.Identity
{
    public static class IdentityContextSeed
    {
        public static void IdentitySeed(this ModelBuilder modelBuilder)
        {
            CreateRoles(modelBuilder);
            CreateBasicUsers(modelBuilder);
            MapUserRole(modelBuilder);
        }

        private static void CreateRoles(ModelBuilder modelBuilder)
        {
            List<IdentityRole> roles = DefaultRoles.IdentityRoleList();
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

        private static void CreateBasicUsers(ModelBuilder modelBuilder)
        {
            List<ApplicationUser> users = DefaultUser.IdentityBasicUserList();
            modelBuilder.Entity<ApplicationUser>().HasData(users);
        }

        private static void MapUserRole(ModelBuilder modelBuilder)
        {
            var identityUserRoles = MappingUserRole.IdentityUserRoleList();
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(identityUserRoles);
        }
    }
}
