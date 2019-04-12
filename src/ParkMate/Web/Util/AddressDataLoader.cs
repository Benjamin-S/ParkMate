using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.Infrastructure.Data;

namespace ParkMate.Web.Util
{
    public static class AddressDataLoader
    {
        public static void LoadAddressData(IHostingEnvironment environment, ParkMateDbContext context)
        {


            if (context.SearchAddresses.Any())
            {
                return;
            }

            var filePath = Path.Combine(environment.WebRootPath, "data", "AddressData.json");
            using (var reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                List<SearchAddressDTO> addresses = JsonConvert.DeserializeObject<List<SearchAddressDTO>>(json);
                context.SearchAddresses.AddRange(addresses);
                context.SaveChanges();
            }
        }
    }
}
