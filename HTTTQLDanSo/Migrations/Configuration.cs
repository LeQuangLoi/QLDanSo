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
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName="NGUYỄN THỊ",
                    UserName = "946002401",
                    Email = "thucnguyenthi@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002401"
                },
                new ApplicationUser()
                {
                    FirstName="HIỆP",
                    WorkerId = 12,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName="VŨ THỊ",
                    UserName = "946002402",
                    Email = "vuthihiep@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002402"
                },
                new ApplicationUser()
                {
                    FirstName = "THỦY",
                    WorkerId = 1,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "NGUYỄN HỒNG",
                    UserName = "946002403",
                    Email = "nguyenhongthu@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002403"
                },
                new ApplicationUser()
                {
                    FirstName = "OANH",
                    WorkerId = 17,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "TRẦN THỊ KIỀU",
                    UserName = "946002404",
                    Email = "trandockieuoanh@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002404"
                },
                new ApplicationUser()
                {
                    FirstName = "THÔNG",
                    WorkerId = 11,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "BÙI THỊ",
                    UserName = "946002405",
                    Email = "buithithong@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002405"
                },
                new ApplicationUser()
                {
                    FirstName = "NAM",
                    WorkerId = 10,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "PHẠM THỊ",
                    UserName = "946002406",
                    Email = "phamthinam@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002406"
                },
                new ApplicationUser()
                {
                    FirstName = "THỊNH",
                    WorkerId = 20,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "NGUYỄN THỊ",
                    UserName = "946002407",
                    Email = "nguyenthithinh@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002407"
                },
                new ApplicationUser()
                {
                    FirstName = "HÀ",
                    WorkerId = 21,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "NGUYỄN THỊ NGÂN",
                    UserName = "946002408",
                    Email = "nguyenthinganha@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002408"
                },
                new ApplicationUser()
                {
                    FirstName = "THÚY",
                    WorkerId = 15,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "TRẦN THỊ",
                    UserName = "946002409",
                    Email = "tranthithuy@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002409"
                },
                new ApplicationUser()
                {
                    FirstName = "NHŨN",
                    WorkerId = 9,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "NGUYỄN THỊ",
                    UserName = "946002410",
                    Email = "nguyenthinhu@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002410"
                },
                new ApplicationUser()
                {
                    FirstName = "HƯỜNG",
                    WorkerId = 19,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "LƯU THỊ",
                    UserName = "946002411",
                    Email = "luuthihuong@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002411"
                },
                new ApplicationUser()
                {
                    FirstName = "HIỀN",
                    WorkerId = 2,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "BÙI THỊ",
                    UserName = "946002412",
                    Email = "vuongthihien@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002412"
                },
                new ApplicationUser()
                {
                    FirstName = "LIÊN",
                    WorkerId = 7,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "TRỊNH KIM",
                    UserName = "946002413",
                    Email = "trinhkimlien@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002413"
                },
                new ApplicationUser()
                {
                    FirstName = "THU",
                    WorkerId = 18,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "NGUYỄN THỊ SỬU",
                    UserName = "946002414",
                    Email = "nguyenthisuuthu@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002414"
                },
                new ApplicationUser()
                {
                    FirstName = "TUYẾT",
                    WorkerId = 5,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "ĐỖ THỊ",
                    UserName = "946002415",
                    Email = "dothituyet@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002415"
                },
                new ApplicationUser()
                {
                    FirstName = "TÍNH",
                    WorkerId = 16,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "TRẦN THỊ",
                    UserName = "946002416",
                    Email = "tranthitinh@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002416"
                },
                new ApplicationUser()
                {
                    FirstName = "HOA",
                    WorkerId = 6,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "HÀ THỊ",
                    UserName = "946002417",
                    Email = "hathihoa@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002417"
                },
                new ApplicationUser()
                {
                    FirstName = "OANH",
                    WorkerId= 14,
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    LastName = "TẠ THỊ KIM",
                    UserName = "946002418",
                    Email = "tathikimoanh@demo.com",
                    EmailConfirmed = true,
                    RegionID = "0100300100",
                    PhoneNumber = "946002418"
                },
                new ApplicationUser()
                {
                    FirstName = "THỊNH",
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    WorkerId = 4,
                    LastName = "ĐỖ THỊ",
                    UserName = "946002419",
                    Email = "dothithinh@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002419"
                },
                new ApplicationUser()
                {
                    FirstName = "THÁI",
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    WorkerId = 8,
                    LastName = "HÀ THỊ",
                    UserName = "946002420",
                    Email = "hathithai@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002420"
                },
                new ApplicationUser()
                {
                    FirstName = "LINH",
                    ProvinId="0100000000",
                    DistrictId="0100300000",
                    WorkerId = 13,
                    LastName = "TRAN THI THUY",
                    UserName = "946002421",
                    Email = "tranthithuylinh@demo.com",
                    EmailConfirmed = false,
                    RegionID = "0100300100",
                    PhoneNumber = "946002421"
                }
            };
            var adminUsers = new List<ApplicationUser>
            {
                new ApplicationUser()
                {
                    FirstName = "Dai",
                    WorkerId = null,
                    LastName = "Duong",
                    UserName = "0886861267",
                    Email = "daiduong@gmail.com",
                    EmailConfirmed = false,
                    RegionID=string.Empty,
                    PhoneNumber = "0886861267"
                }
            };
            var supperAdmins = new List<ApplicationUser>
            {
                new ApplicationUser()
                {
                    FirstName = "Le",
                    WorkerId = null,
                    LastName = "Loi",
                    UserName = "0975347490",
                    Email = "lequangloi0909@gmail.com",
                    EmailConfirmed = false,
                    RegionID=string.Empty,
                    PhoneNumber = "0975347490"
                }
            };

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "CTV" });
            }

            foreach (var user in applicationUsers)
            {
                if (manager.Users.Count(x => x.PhoneNumber == user.PhoneNumber) == 0)
                {
                    var result = manager.Create(user, "Abc@123");
                    if (result.Succeeded)
                    {
                        var ctvUser = manager.Users.FirstOrDefault(x => x.PhoneNumber == user.PhoneNumber);

                        // Initi
                        context.Database.ExecuteSqlCommand("INSERT INTO UserWorkers (WorkerId, UserId) VALUES ({0}, {1})", user.WorkerId, ctvUser.Id);
                        if (ctvUser != null)
                        {
                            manager.AddToRoles(ctvUser.Id, new string[] { "CTV" });
                        }
                    }
                }
            }

            foreach (var user in adminUsers)
            {
                if (manager.Users.Count(x => x.PhoneNumber == user.PhoneNumber) == 0)
                {
                    var result = manager.Create(user, "Abc@999");
                    if (result.Succeeded)
                    {
                        var adminUser = manager.Users.FirstOrDefault(x => x.PhoneNumber == user.PhoneNumber);
                        if (adminUser != null)
                        {
                            manager.AddToRoles(adminUser.Id, new string[] { "Admin" });
                        }
                    }
                }
            }

            foreach (var user in supperAdmins)
            {
                if (manager.Users.Count(x => x.PhoneNumber == user.PhoneNumber) == 0)
                {
                    var result = manager.Create(user, "Abc@999");
                    if (result.Succeeded)
                    {
                        var adminUser = manager.Users.FirstOrDefault(x => x.PhoneNumber == user.PhoneNumber);
                        if (adminUser != null)
                        {
                            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin" });
                        }
                    }
                }
            }

            context.SaveChanges();
        }
    }
}