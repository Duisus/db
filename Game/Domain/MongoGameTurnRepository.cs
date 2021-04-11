using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Game.Domain
{
    public class MongoGameTurnRepository : IGameTurnRepository
    {
        private readonly IMongoCollection<GameTurnEntity> turnsCollection;
        public const string CollectionName = "turns";

        public MongoGameTurnRepository(IMongoDatabase database)
        {
            turnsCollection = database.GetCollection<GameTurnEntity>(CollectionName);

            turnsCollection.Indexes.CreateOne(new CreateIndexModel<GameTurnEntity>(
                Builders<GameTurnEntity>.IndexKeys
                    .Ascending(t => t.GameId)
                    .Descending(t => t.TurnNumber)));
        }

        public GameTurnEntity Insert(GameTurnEntity turn)
        {
            turnsCollection.InsertOne(turn);
            return turn;
        }

        public GameTurnEntity FindById(Guid id)
        {
            return turnsCollection.Find(turn => turn.Id == id).FirstOrDefault();
        }

        public List<GameTurnEntity> GetLastTurns(Guid gameId, int limit = 5)
        {
            return turnsCollection.Find(turn => turn.GameId == gameId)
                .SortByDescending(u => u.TurnNumber)
                .Limit(limit)
                .ToList();
        }
    }
}