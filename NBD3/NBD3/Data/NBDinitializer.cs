using Microsoft.CodeAnalysis;
using NBD3.Models;
using System.Diagnostics;
using System.Numerics;

namespace NBD3.Data
{
    public class NBDinitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            NBDContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<NBDContext>();

            try
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

               



                // Seed Clients
                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(
                        new Client
                        {
                            ClientCommpanyName = "Key Venture",
                            ClientStreetAddress = "7th Floor, West Tower",
                            ClientCityAddress = "Toronto",
                            ClientCountryAddress = "ON",
                            ClientPostalCode = "M8X 2X2",
                            ClientPhone = "800-316-2759",
                            ClientEmail = "design@keyventure.ca",
                            ClientFirstName = "Sandra",
                            ClientMiddleName = "",
                            ClientLastName = "Smith"
                        },
                       new Client
                       {
                           ClientCommpanyName = "Roo Construction",
                           ClientStreetAddress = "Building 225-4N-14",
                           ClientCityAddress = "St. Paul",
                           ClientCountryAddress = "MN",
                           ClientPostalCode = "M1M 1M1", 
                           ClientPhone = "800-328-0067",
                           ClientEmail = "marketing@rooconstruction.ca",
                           ClientFirstName = "John",
                           ClientMiddleName = "",
                           ClientLastName = "Chevalier"
                       },
new Client
{
    ClientCommpanyName = "Modern Structure",
    ClientStreetAddress = "8833 Mansfield Ave.",
    ClientCityAddress = "Morton Grove",
    ClientCountryAddress = "IL",
    ClientPostalCode = "K2K 2K2",
    ClientPhone = "505-265-3591",
    ClientEmail = "sales@modernstructures.ca",
    ClientFirstName = "George",
    ClientMiddleName = "",
    ClientLastName = "Hamilton"
},
new Client
{
    ClientCommpanyName = "Omega Design",
    ClientStreetAddress = "101 Wolf Dr.",
    ClientCityAddress = "Thorofare",
    ClientCountryAddress = "NJ",
    ClientPostalCode = "L3L 3L3",
    ClientPhone = "800-776-6939",
    ClientEmail = "sales@omegadesign.ca",
    ClientFirstName = "Charles",
    ClientMiddleName = "",
    ClientLastName = "Baute"
},
new Client
{
    ClientCommpanyName = "Diamond Shine",
    ClientStreetAddress = "721 Clinton Ave. Suite 11",
    ClientCityAddress = "Huntsville",
    ClientCountryAddress = "AL",
    ClientPostalCode = "V4V 4V4",
    ClientPhone = "800-866-9797",
    ClientEmail = "biosis.custserv@thomson.com",
    ClientFirstName = "Thomas",
    ClientMiddleName = "",
    ClientLastName = "Vachon"
});

                    context.SaveChanges();
                    // Locations.
                    if (!context.Locations.Any())
                    {
                        context.Locations.AddRange(
                            new Models.Location
                            {
                                LocationName = "Burnaby, BC",
                                LocationStreetAddress = "123 Main St",
                                LocationPostalCode = "V5G 2J3",
                                LocationCityAddress = "Burnaby",
                                LocationCountryAddress = "Canada",
                                LocationPhone = "1234567890",
                                LocationContactPer = "John Doe"
                            },
                            new Models.Location
                            {
                                LocationName = "Highway 401",
                                LocationStreetAddress = "456 Highway Ave",
                                LocationPostalCode = "M9W 5M3",
                                LocationCityAddress = "Toronto",
                                LocationCountryAddress = "Canada",
                                LocationPhone = "1234567890",
                                LocationContactPer = "Jane Smith"
                            },
                            new Models.Location
                            {
                                LocationName = "Blanchard House",
                                LocationStreetAddress = "789 Blanchard St",
                                LocationPostalCode = "V8W 2G1",
                                LocationCityAddress = "Victoria",
                                LocationCountryAddress = "Canada",
                                LocationPhone = "1234567890",
                                LocationContactPer = "Chris Johnson"
                            },
                            new Models.Location
                            {
                                LocationName = "N. Vancouver, BC",
                                LocationStreetAddress = "321 Mountain Blvd",
                                LocationPostalCode = "V7R 2M1",
                                LocationCityAddress = "North Vancouver",
                                LocationCountryAddress = "Canada",
                                LocationPhone = "1234567890",
                                LocationContactPer = "Emily Davis"
                            });
                        context.SaveChanges();
                    }


                    // Seed Projects if there aren't any.
                    if (!context.Projects.Any())
                    {
                        context.Projects.AddRange(
                        new Models.Project
                        {
                            ProjectName = "Botanical Wonderland",
                            ProjectDescription = "Lurie Garden is Millennium Park’s ‘secret garden’. This naturalistic garden is a place of rest and renewal for humans and wildlife alike. ",
                            ProjectStartDate = DateOnly.Parse("2024-09-01"),
                            ProjectEndDate = DateOnly.Parse("2024-11-01"),
                            ClientId = 1,
                            LocationId = 2

                        },
                        new Models.Project
                        {
                            ProjectName = "Kenrokuen Garden",
                            ProjectDescription = "he name Kenrokuen literally means \"Garden of the six sublimities\", referring to spaciousness, seclusion, artificiality, antiquity, abundant water and broad views, which according to Chinese landscape theory are the six essential attributes that make up a perfect garden. ",
                            ProjectStartDate = DateOnly.Parse("2024-09-01"),
                            ProjectEndDate = DateOnly.Parse("2024-11-01"),
                            ClientId = 2,
                            LocationId = 4
                        },
                        new Models.Project
                        {
                            ProjectName = "High Line Park",
                            ProjectDescription = "he High Line is almost entirely supported by people like you. As a nonprofit, we need your support to keep this public space free—and extraordinary—for everyone.",
                            ProjectStartDate = DateOnly.Parse("2024-09-01"),
                            ProjectEndDate = DateOnly.Parse("2024-11-01"),
                            ClientId = 1,
                            LocationId = 3
                        },
                        new Models.Project
                        {
                            ProjectName = "Garden of Australian Dreams",
                            ProjectDescription = "laza Euskadi connects the nineteenth century section of the city called “El Ensanche” to the new section of Bilbao, Deusto university campus, the Guggenheim Museum, and the Nervión River. The Plaza is a pivot, unifying diverse elements of the city.",
                            ProjectStartDate = DateOnly.Parse("2024-09-01"),
                            ProjectEndDate = DateOnly.Parse("2024-11-01"),
                            ClientId = 5,
                            LocationId = 1
                        },
                        new Models.Project
                        {
                            ProjectName = "Prospect Park",
                            ProjectDescription = "Prospect Park Alliance is the non-profit organization that sustains, restores and advances Brooklyn's Backyard, in partnership with the City of New York.",
                            ProjectStartDate = DateOnly.Parse("2024-09-01"),
                            ProjectEndDate = DateOnly.Parse("2024-11-01"),
                            ClientId = 4,
                            LocationId = 2
                        });
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
