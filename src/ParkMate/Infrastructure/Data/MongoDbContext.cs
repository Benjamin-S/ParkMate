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

        public IMongoCollection<ParkingSpaceViewModel> ParkingSpaces => 
            MongoDatabase.GetCollection<ParkingSpaceViewModel>("ParkingSpaces");

        public IMongoCollection<CustomerViewModel> Customers =>
            MongoDatabase.GetCollection<CustomerViewModel>("Customers");


        void CreateIndexes()
        {
            var index = new CreateIndexModel<ParkingSpaceViewModel>(
                new IndexKeysDefinitionBuilder<ParkingSpaceViewModel>()
                .Geo2DSphere(x => x.Location));

            ParkingSpaces.Indexes.CreateOne(index);
        }
    }
}
