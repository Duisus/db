using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Game.Domain
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> userCollection;
        public const string CollectionName = "users";

        public MongoUserRepository(IMongoDatabase database)
        {
            userCollection = database.GetCollection<UserEntity>(CollectionName);

            userCollection.Indexes.CreateOne(
                new BsonDocument("Login", 1),
                new CreateIndexOptions { Unique = true});
        }

        public UserEntity Insert(UserEntity user)
        {
            userCollection.InsertOne(user);
            return user;
        }

        public UserEntity FindById(Guid id)
        {
            return userCollection.Find(user => user.Id == id).FirstOrDefault();
        }

        public UserEntity GetOrCreateByLogin(string login)
        {
            var user = userCollection.Find(user => user.Login == login);
            if(user.Count() > 0)
                return user.First();
            var newUser = new UserEntity(Guid.NewGuid());
            newUser.Login = login;
            return Insert(newUser);
        }

        public void Update(UserEntity user)
        {
            userCollection.ReplaceOne(u => u.Id == user.Id, user);
        }

        public void Delete(Guid id)
        {
            userCollection.DeleteOne(user => user.Id == id);
        }

        // Для вывода списка всех пользователей (упорядоченных по логину)
        // страницы нумеруются с единицы
        public PageList<UserEntity> GetPage(int pageNumber, int pageSize)
        {
            var skipCount = (pageNumber - 1) * pageSize;
            var totalCount = userCollection.CountDocuments(new BsonDocument());
            
            var elements = userCollection.Find(new BsonDocument()).SortBy(u => u.Login)
                .Skip(skipCount)
                .Limit(pageSize)
                .ToList();
            
            return new PageList<UserEntity>(elements, totalCount, pageNumber, pageSize);
        }

        // Не нужно реализовывать этот метод.
        public void UpdateOrInsert(UserEntity user, out bool isInserted)
        {
            throw new NotImplementedException();
        }
    }
}