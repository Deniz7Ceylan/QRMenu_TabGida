using QRMenu_TabGida.Models;

namespace QRMenu_TabGida.Data
{
    public class DbInitializer
    {
        public void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // State değerlerini ekleyin (sadece boş olan tabloya)
            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(
                    new Status { Name = "Active" },
                    new Status { Name = "Passive" },
                    new Status { Name = "Deleted" }
                );
                context.SaveChanges();
            }

            // TAB Gıda company'sini oluşturun (sadece bir kez oluşturulmalı)
            if (!context.Companies.Any(c => c.Name == "TAB Gıda"))
            {
                var tabGidaCompany = new Company
                {
                    Name = "TAB Gıda",
                    PostalCode = "34349",
                    Address = "Dikilitaş Mahallesi Emirhan Caddesi No:109 Beşiktaş / İstanbul",
                    Phone = "02123106600",
                    EMail = "tabgida@tabgida.com.tr",
                    RegisterDate = DateTime.Now, // Şu anki tarih
                    WebAddress = "http://www.tabgida.com.tr",
                    Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                };
                context.Companies.Add(tabGidaCompany);
                context.SaveChanges();

                // TAB Gıda company'sine ait bir ApplicationUser oluşturun
                var tabGidaUser = new ApplicationUser
                {
                    UserName = "TabAdmin",
                    Password = "Tabgida.123",
                    Name = "TAB",
                    SurName = "Gıda",
                    Email = "tabgidaadmin@tabgida.com.tr",
                    PhoneNumber = "02123106600",
                    RegisterDate = DateTime.Now,
                    CompanyId = tabGidaCompany.Id,
                    Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                };
                context.ApplicationUsers.Add(tabGidaUser);
                context.SaveChanges();
            }

            if (!context.Brands.Any())
            {
                // Burger King markasının eklenmesi

                var burgerKing = new Brand
                {
                    Name = "Burger King",
                    PostalCode = "12345",
                    Address = "BKAdres",
                    Phone = "1234567890",
                    EMail = "info@burgerking.com",
                    RegisterDate = DateTime.Now,
                    TaxNumber = "1234567890",
                    WebAddress = "https://www.burgerking.com",
                    Company = context.Companies.FirstOrDefault(s => s.Name == "TAB Gıda"),
                    Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                };
                context.Brands.Add(burgerKing);

                //Arby's
                var arbys = new Brand
                {
                    Name = "Arby's",
                    PostalCode = "12345",
                    Address = "ArbysAdres",
                    Phone = "1234567890",
                    EMail = "info@arbys.com",
                    RegisterDate = DateTime.Now,
                    TaxNumber = "1234567890",
                    WebAddress = "https://www.arbys.com",
                    Company = context.Companies.FirstOrDefault(s => s.Name == "TAB Gıda"),
                    Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                };
                context.Brands.Add(arbys);

                //Popeyes
                var popeyes = new Brand
                {
                    Name = "Popeyes",
                    PostalCode = "12345",
                    Address = "PopeyesAdres",
                    Phone = "1234567890",
                    EMail = "info@popeyes.com",
                    RegisterDate = DateTime.Now,
                    TaxNumber = "1234567890",
                    WebAddress = "https://www.popeyes.com",
                    Company = context.Companies.FirstOrDefault(s => s.Name == "TAB Gıda"),
                    Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                };
                context.Brands.Add(popeyes);

                //Usta Dönerci
                var ustaDonerci = new Brand
                {
                    Name = "Usta Dönerci",
                    PostalCode = "12345",
                    Address = "UDAdres",
                    Phone = "1234567890",
                    EMail = "info@ustadonerci.com",
                    RegisterDate = DateTime.Now,
                    TaxNumber = "1234567890",
                    WebAddress = "https://www.ustadonerci.com",
                    Company = context.Companies.FirstOrDefault(s => s.Name == "TAB Gıda"),
                    Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                };
                context.Brands.Add(ustaDonerci);

                //Sbarro
                var sbarro = new Brand
                {
                    Name = "Sbarro",
                    PostalCode = "12345",
                    Address = "SbarroAdres",
                    Phone = "1234567890",
                    EMail = "info@sbarro.com",
                    RegisterDate = DateTime.Now,
                    TaxNumber = "1234567890",
                    WebAddress = "https://www.sbarro.com",
                    Company = context.Companies.FirstOrDefault(s => s.Name == "TAB Gıda"),
                    Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                };
                context.Brands.Add(sbarro);

                //Usta Pideci
                var ustaPideci = new Brand
                {
                    Name = "Usta Pideci",
                    PostalCode = "12345",
                    Address = "UstaPideciAdres",
                    Phone = "1234567890",
                    EMail = "info@ustapideci.com",
                    RegisterDate = DateTime.Now,
                    TaxNumber = "1234567890",
                    WebAddress = "https://www.ustapideci.com",
                    Company = context.Companies.FirstOrDefault(s => s.Name == "TAB Gıda"),
                    Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                };
                context.Brands.Add(ustaPideci);

                //Subway
                var subway = new Brand
                {
                    Name = "Subway",
                    PostalCode = "12345",
                    Address = "SubwayAdres",
                    Phone = "1234567890",
                    EMail = "info@subway.com",
                    RegisterDate = DateTime.Now,
                    TaxNumber = "1234567890",
                    WebAddress = "https://www.subway.com",
                    Company = context.Companies.FirstOrDefault(s => s.Name == "TAB Gıda"),
                    Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                };
                context.Brands.Add(subway);


                if (!context.BrandUsers.Any())
                {
                    var burgerKingAdmin = new BrandUser
                    {
                        UserName = "BurgerKingAdmin",
                        Password = "Burgerking.123",
                        Name = "Admin",
                        SurName = "Burger King",
                        Email = "burgerkingadmin@example.com",
                        PhoneNumber = "1234567890",
                        RegisterDate = DateTime.Now,
                        BrandId = burgerKing.Id,
                        Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                    };
                    context.BrandUsers.Add(burgerKingAdmin);

                    var arbysAdmin = new BrandUser
                    {
                        UserName = "ArbysAdmin",
                        Password = "Arbys.123",
                        Name = "Admin",
                        SurName = "arbys",
                        Email = "arbysadmin@example.com",
                        PhoneNumber = "1234567890",
                        RegisterDate = DateTime.Now,
                        BrandId = arbys.Id,
                        Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                    };
                    context.BrandUsers.Add(arbysAdmin);

                    var popeyesAdmin = new BrandUser
                    {
                        UserName = "PopeyesAdmin",
                        Password = "Popeyes.123",
                        Name = "Admin",
                        SurName = "popeyes",
                        Email = "popeyesadmin@example.com",
                        PhoneNumber = "1234567890",
                        RegisterDate = DateTime.Now,
                        BrandId = popeyes.Id,
                        Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                    };
                    context.BrandUsers.Add(popeyesAdmin);

                    var ustaDonerciAdmin = new BrandUser
                    {
                        UserName = "UstaDonerciAdmin",
                        Password = "Ustadonerci.123",
                        Name = "Admin",
                        SurName = "ustadonerci",
                        Email = "ustadonerciadmin@example.com",
                        PhoneNumber = "1234567890",
                        RegisterDate = DateTime.Now,
                        BrandId = ustaDonerci.Id,
                        Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                    };
                    context.BrandUsers.Add(ustaDonerciAdmin);

                    var sbarroAdmin = new BrandUser
                    {
                        UserName = "SbarroAdmin",
                        Password = "Sbarro.123",
                        Name = "Admin",
                        SurName = "sbarro",
                        Email = "sbarroadmin@example.com",
                        PhoneNumber = "1234567890",
                        RegisterDate = DateTime.Now,
                        BrandId = sbarro.Id,
                        Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                    };
                    context.BrandUsers.Add(sbarroAdmin);

                    var ustaPideciAdmin = new BrandUser
                    {
                        UserName = "UstaPideciAdmin",
                        Password = "Ustapideci.123",
                        Name = "Admin",
                        SurName = "ustapideci",
                        Email = "ustapideciadmin@example.com",
                        PhoneNumber = "1234567890",
                        RegisterDate = DateTime.Now,
                        BrandId = ustaPideci.Id,
                        Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                    };
                    context.BrandUsers.Add(ustaPideciAdmin);

                    var subwayAdmin = new BrandUser
                    {
                        UserName = "SubwayAdmin",
                        Password = "Subway.123",
                        Name = "Admin",
                        SurName = "subway",
                        Email = "subwayadmin@example.com",
                        PhoneNumber = "1234567890",
                        RegisterDate = DateTime.Now,
                        BrandId = ustaPideci.Id,
                        Status = context.Statuses.FirstOrDefault(s => s.Name == "Active")
                    };
                    context.BrandUsers.Add(subwayAdmin);
                }
            }
            context.SaveChanges();
        }
    }
}
