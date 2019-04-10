using System;
using MongoDB.Driver;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Interfaces
{
    public interface IMongoContext
    {
        IMongoDatabase MongoDatabase { get; }
        IMongoCollection<ParkingSpace> ParkingSpaces { get; }
        IMongoCollection<Customer> Customers { get; }
    }
}
