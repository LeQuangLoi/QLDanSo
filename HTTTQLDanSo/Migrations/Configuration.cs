namespace HTTTQLDanSo.Migrations
{
    using HTTTQLDanSo.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HTTTQLDanSo.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "HTTTQLDanSo.Models.ApplicationDbContext";
        }

        protected override void Seed(HTTTQLDanSo.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            CreateUser(context);
        }

        private void CreateUser(HTTTQLDanSo.Models.ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var applicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser()
                {
                    FirstName="THỤC",
                    WorkerId = 3,
                    LastName="NGUYỄN THỊ",
                    UserName = "thucnguyenthi@demo.com",
                    Email = "thucnguyenthi@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002401"
                }
                , new ApplicationUser()
                {
                    FirstName="HIỆP",
                    WorkerId = 12,
                    LastName="VŨ THỊ",
                    UserName = "vuthihiep@demo.com",
                    Email = "vuthihiep@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002402"
                },
                 new ApplicationUser()
                {
                    FirstName = "THỦY",
                    WorkerId = 1,
                    LastName = "NGUYỄN HỒNG",
                    UserName = "nguyenhongthu@demo.com",
                    Email = "nguyenhongthu@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002403"
                },
                new ApplicationUser()
                {
                    FirstName = "OANH",
                    WorkerId = 17,
                    LastName = "TRẦN THỊ KIỀU",
                    UserName = "trandockieuoanh@demo.com",
                    Email = "trandockieuoanh@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002404"
                },
                new ApplicationUser()
                {
                    FirstName = "THÔNG",
                    WorkerId = 11,
                    LastName = "BÙI THỊ",
                    UserName = "buithithong@demo.com",
                    Email = "buithithong@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002405"
                },
                new ApplicationUser()
                {
                    FirstName = "NAM",
                    WorkerId = 10,
                    LastName = "PHẠM THỊ",
                    UserName = "phamthinam@demo.com",
                    Email = "phamthinam@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002406"
                },
                new ApplicationUser()
                {
                    FirstName = "THỊNH",
                    WorkerId = 20,
                    LastName = "NGUYỄN THỊ",
                    UserName = "nguyenthithinh@demo.com",
                    Email = "nguyenthithinh@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002407"
                },
                new ApplicationUser()
                {
                    FirstName = "HÀ",
                    WorkerId = 21,
                    LastName = "NGUYỄN THỊ NGÂN",
                    UserName = "nguyenthinganha@demo.com",
                    Email = "nguyenthinganha@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002408"
                },
                new ApplicationUser()
                {
                    FirstName = "THÚY",
                    WorkerId = 15,
                    LastName = "TRẦN THỊ",
                    UserName = "tranthithuy@demo.com",
                    Email = "tranthithuy@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002409"
                },
                new ApplicationUser()
                {
                    FirstName = "NHŨN",
                    WorkerId = 9,
                    LastName = "NGUYỄN THỊ",
                    UserName = "nguyenthinhu@demo.com",
                    Email = "nguyenthinhu@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002410"
                },
                new ApplicationUser()
                {
                    FirstName = "HƯỜNG",
                    WorkerId = 19,
                    LastName = "LƯU THỊ",
                    UserName = "luuthihuong@demo.com",
                    Email = "luuthihuong@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002411"
                },
                new ApplicationUser()
                {
                    FirstName = "HIỀN",
                    WorkerId = 2,
                    LastName = "BÙI THỊ",
                    UserName = "vuongthihien@demo.com",
                    Email = "vuongthihien@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002412"
                },
                new ApplicationUser()
                {
                    FirstName = "LIÊN",
                    WorkerId = 7,
                    LastName = "TRỊNH KIM",
                    UserName = "trinhkimlien@demo.com",
                    Email = "trinhkimlien@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002413"
                },
                new ApplicationUser()
                {
                    FirstName = "THU",
                    WorkerId = 18,
                    LastName = "NGUYỄN THỊ SỬU",
                    UserName = "nguyenthisuuthu@demo.com",
                    Email = "nguyenthisuuthu@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002414"
                },
                new ApplicationUser()
                {
                    FirstName = "TUYẾT",
                    WorkerId = 5,
                    LastName = "ĐỖ THỊ",
                    UserName = "dothituyet@demo.com",
                    Email = "dothituyet@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002415"
                },
                new ApplicationUser()
                {
                    FirstName = "TÍNH",
                    WorkerId = 16,
                    LastName = "TRẦN THỊ",
                    UserName = "tranthitinh@demo.com",
                    Email = "tranthitinh@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002416"
                },
                new ApplicationUser()
                {
                    FirstName = "HOA",
                    WorkerId = 6,
                    LastName = "HÀ THỊ",
                    UserName = "hathihoa@demo.com",
                    Email = "hathihoa@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002417"
                },
                new ApplicationUser()
                {
                    FirstName = "OANH",
                    WorkerId= 14,
                    LastName = "TẠ THỊ KIM",
                    UserName = "tathikimoanh@demo.com",
                    Email = "tathikimoanh@demo.com",
                    EmailConfirmed = true,
                    RegionID = "0100300100",
                    PhoneNumber = "946002418"
                },
                new ApplicationUser()
                {
                    FirstName = "THỊNH",
                    WorkerId = 4,
                    LastName = "ĐỖ THỊ",
                    UserName = "THINH",
                    Email = "dothithinh@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002419"
                },
                new ApplicationUser()
                {
                    FirstName = "THÁI",
                    WorkerId = 8,
                    LastName = "HÀ THỊ",
                    UserName = "hathithai@demo.com",
                    Email = "hathithai@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002420"
                },
                new ApplicationUser()
                {
                    FirstName = "LINH",
                    WorkerId = 13,
                    LastName = "TRAN THI THUY",
                    UserName = "tranthithuylinh@demo.com",
                    Email = "tranthithuylinh@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002421"
                }
            };

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "CTV" });
            }

            foreach (var user in applicationUsers)
            {
                if (manager.Users.Count(x => x.Email == user.Email) == 0)
                {
                    var result = manager.Create(user, "Abc@123");
                    if (result.Succeeded)
                    {
                        var adminUser = manager.FindByEmail(user.Email);

                        manager.AddToRoles(adminUser.Id, new string[] { "CTV" });
                    }
                }
            }
            context.SaveChanges();
        }
    }
}