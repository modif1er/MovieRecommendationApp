using MRA.Domain.Entities;
using MRA.Domain.Enum;

namespace MRA.Persistence.Context.Seeds.Identity
{
    public static class DefaultUser
    {
        public static List<ApplicationUser> IdentityBasicUserList()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    Id = Constants.SuperAdminUser,
                    UserName = "superadmin",
                    Email = "superadmin@mra.com",
                    FirstName = "Ramiz K.",
                    LastName = "Bicakci",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    // Password@123
                    PasswordHash = "AQAAAAEAACcQAAAAEBLjouNqaeiVWbN0TbXUS3+ChW3d7aQIk/BQEkWBxlrdRRngp14b0BIH0Rp65qD6mA==",
                    NormalizedEmail= "SUPERADMIN@MRA.COM",
                    NormalizedUserName="SUPERADMIN"
                },
                new ApplicationUser
                {
                    Id = Constants.BasicUser,
                    UserName = "basicuser",
                    Email = "basicuser@mra.com",
                    FirstName = "John",
                    LastName = "Doe",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    // Password@123
                    PasswordHash = "AQAAAAEAACcQAAAAEBLjouNqaeiVWbN0TbXUS3+ChW3d7aQIk/BQEkWBxlrdRRngp14b0BIH0Rp65qD6mA==",
                    NormalizedEmail= "BASICUSER@MRA.COM",
                    NormalizedUserName="BASICUSER"
                },
            };
        }
    }
}
