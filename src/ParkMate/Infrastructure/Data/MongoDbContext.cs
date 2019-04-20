using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.Infrastructure.Data
{
    public class MongoDbContext : IMongoContext
    {
        public MongoDbContext(IOptions<MongoSettings> options, IMongoClient client)
        {
            MongoDatabase = client.GetDatabase(options.Value.Database);
            CreateIndexes();
        }
        public IMongoDatabase MongoDatabase { get; }

        public IMongoCollection<ParkingSpace> ParkingSpaces => 
            MongoDatabase.GetCollection<ParkingSpace>("ParkingSpace");

        public IMongoCollection<Customer> Customers =>
            MongoDatabase.GetCollection<Customer>("Customer");

        public IMongoCollection<ParkingSpaceListingDTO> ParkingSpaceListings =>
            MongoDatabase.GetCollection<ParkingSpaceListingDTO>("ParkingSpaceListings");

        void CreateIndexes()
        {
            var index = new CreateIndexModel<ParkingSpaceListingDTO>(
                new IndexKeysDefinitionBuilder<ParkingSpaceListingDTO>()
                .Geo2DSphere(x => x.Location));

            ParkingSpaceListings.Indexes.CreateOne(index);
        }
    }
}
