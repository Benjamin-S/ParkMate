using System;
using System.Threading.Tasks;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Interfaces
{
    public interface IDocumentWriteRepository
    {
        Task InsertOneAsync<T>(T entity) where T : BaseEntity;
        Task ReplaceOneAsync<T>(T entity) where T : BaseEntity;
    }
}
