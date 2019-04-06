using System.Threading.Tasks;
using MongoDB.Driver;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.Infrastructure.Data
{
    public class DocumentRepository : IDocumentWriteRepository
    {
        private IMongoContext _context;

        public DocumentRepository(IMongoContext context)
        {
            _context = context;
        }
        public async Task ReplaceOneAsync<T>(T entity, string collectionName) 
            where T : BaseEntity
        {
            var collection = _context.MongoDatabase.GetCollection<T>(collectionName);
            
            await collection.ReplaceOneAsync(
                doc => doc.Id == entity.Id, entity,
                new UpdateOptions { IsUpsert = true });
        }
        public async Task InsertOneAsync<T>(T entity, string collectionName) 
            where T : BaseEntity
        {
            var collection = _context.MongoDatabase.GetCollection<T>(collectionName);

            await collection.InsertOneAsync(entity);
        }
    }
}