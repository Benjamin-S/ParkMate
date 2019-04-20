using System;
using MongoDB.Driver;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Interfaces
{
    public interface IMongoContext
    {
        IMongoDatabase MongoDatabase { get; }
        IMongoCollection<ParkingSpaceViewModel> ParkingSpaces { get; }
        IMongoCollection<Customer> Customers { get; }
    }
}
