using System.Threading.Tasks;
using MongoDB.Driver;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.Infrastructure.Data
{
    public class DocumentRepository : IDocumentWriteRepository
    {
        private IMongoContext _context;

        private IMongoCollection<T> Collection<T>(string name) 
        {
            return _context.MongoDatabase.GetCollection<T>(name);
        }

        public DocumentRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task ReplaceOneAsync<T>(T entity, string name) 
            where T : BaseEntity
        {
            var collection = _context.MongoDatabase.GetCollection<T>(name);
            
            await Collection<T>(name).ReplaceOneAsync(
                doc => doc.Id == entity.Id, 
                entity,
                new UpdateOptions { IsUpsert = true });
        }

        public async Task InsertOneAsync<T>(T entity, string collectionName) 
            where T : BaseEntity
        {
            await Collection<T>(collectionName).InsertOneAsync(entity);
        }

        public async Task DeleteOneAsync<T>(T entity, string name)
            where T : BaseEntity
        {
            await Collection<T>(name).DeleteOneAsync(e => e.Id == entity.Id);
        }
    }
}