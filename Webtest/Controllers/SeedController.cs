using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Webtest.Models;

using System.Globalization;
using Webtest.Data;
using System.Diagnostics.Metrics;

namespace Webtest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(CarmanufacturerContext db, IHostEnvironment environment,
        UserManager<WorldCarsUser> userManager)
        : ControllerBase
    {
       
        private readonly string _pathName = Path.Combine(environment.ContentRootPath, "Data/worldcars.csv");


        [HttpPost("User")]
        public async Task<ActionResult> SeedUsers()
        {
            (string name, string email) = ("user1", "comp584@csun.edu");
            WorldCarsUser user = new()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Check if the user already exists
            //var existingUser = await userManager.FindByNameAsync(name);
            //if (existingUser != null)
            //{
            // User already exists, choose a different username
            //   user.UserName = "user2";
            //}
            if (await userManager.FindByNameAsync(name) is not null)
            {
                user.UserName = "user2";

            }
            _ = await userManager.CreateAsync(user, "P@ssw0rd!")
                ?? throw new InvalidOperationException();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await db.SaveChangesAsync();

            return Ok();

        }


        /*
        [HttpPost("Car")]
        public async Task<ActionResult<Car>> SeedCar()
        {
           
            Dictionary<string, Manufacturer> manufacturer = await db.Manufacturers//.AsNoTracking()
            .ToDictionaryAsync(x => x.ManufacturerName);
            //manufacuterername
         // Console.WriteLine(manufacturer);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            int carCount = 0;
            using (StreamReader reader = new(_pathName))
            using (CsvReader csv = new(reader, config))
            {
                IEnumerable<WorldCarsCSV>? records = csv.GetRecords<WorldCarsCSV>();
                foreach (WorldCarsCSV record in records)
                {

                  if (!manufacturer.TryGetValue(record.manufacturer_name,out Manufacturer? value))
                  {
                     
                 //   //Console.WriteLine(manufacturer);
                      Console.WriteLine($"Not found manufacturer for {record.car_name}");
                      return NotFound(record);
                  }

                    if (string.IsNullOrEmpty(record.car_drivetrain))
                    {
                        Console.WriteLine($"Skipping {record.car_name}");
                        continue;
                    }
                    //                    if (!record.population.HasValue || string.IsNullOrEmpty(record.city_ascii))
                    //                  {
                    //                    Console.WriteLine($"Skipping {record.city}");
                    //                  continue;
                    //            }
                    Car car = new()
                    {
                        CarName = record.car_name,
                        
                        CarYear = record.car_year,
                        CarDrivetrain = record.car_drivetrain,
                        ManufacturerId = record.manufacturer_id,
                        //ManufacturerName = record.manufacturer_name,
                    };
                    db.Cars.Add(car);
                    carCount++;
                }
                await db.SaveChangesAsync();
            }
            return new JsonResult(carCount);
        } */
        [HttpPost("Car")]
        public async Task<ActionResult<Car>> SeedCar()
        {
            Dictionary<string, Manufacturer> manufacturer = await db.Manufacturers
                .ToDictionaryAsync(x => x.ManufacturerName);
            //, StringComparer.OrdinalIgnoreCase
            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            int carCount = 0;

            using (StreamReader reader = new(_pathName))
            using (CsvReader csv = new(reader, config))
            {
                IEnumerable<WorldCarsCSV> records = csv.GetRecords<WorldCarsCSV>();

                foreach (WorldCarsCSV record in records)
                {
                    if (!manufacturer.TryGetValue(record.manufacturer_name, out Manufacturer? value))
                    {
                        Console.WriteLine($"Not found manufacturer for {record.car_name}");
                        return NotFound(record);
                    }

                    if (string.IsNullOrEmpty(record.car_drivetrain))
                    {
                        Console.WriteLine($"Skipping {record.car_name}");
                        continue;
                    }

                    Car car = new()
                    {
                        CarName = record.car_name,
                        CarYear = record.car_year,
                        cardrivetrain = record.car_drivetrain,
                        ManufacturerId = value.ManufacturerId, // Use value from TryGetValue
                    };

                    db.Cars.Add(car);
                    carCount++;
                }

                await db.SaveChangesAsync();
            }

            return new JsonResult(carCount);
        }



        [HttpPost("Manufacturer")]
        public async Task<ActionResult<int>> SeedManufacturer()
        {

            // Ensure that the Class entities exist first
            //await SeedCar();

            Dictionary<string, Manufacturer> manufacturersbyname = await db.Manufacturers
                .ToDictionaryAsync(c => c.ManufacturerName, StringComparer.OrdinalIgnoreCase);

            //               int existingCarCount = await db.Cars.CountAsync();
            //              logger.LogInformation($"Existing Cars: {existingCarCount}");

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };


            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<WorldCarsCSV> records = csv.GetRecords<WorldCarsCSV>().ToList();
            foreach (WorldCarsCSV record in records)
            {   //manufacturer.id
                if (manufacturersbyname.ContainsKey(record.manufacturer_name))
                {
                    continue;
                }
                    Manufacturer manufacturer = new()
                    {
                        ManufacturerName = record.manufacturer_name,
                        ManufacturerCountry = record.manufacturer_country
                        

                    };
                    await db.Manufacturers.AddAsync(manufacturer);
                    manufacturersbyname.Add(record.manufacturer_name, manufacturer);
                    
              
            }
            await db.SaveChangesAsync();

            return new JsonResult(manufacturersbyname.Count);



        }
    }
}