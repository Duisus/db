using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Game.Domain
{
    public class GameTurnEntity
    {
        [BsonElement]
        public readonly Guid Id;

        [BsonElement]
        public readonly Guid GameId;

        [BsonElement]
        public readonly int TurnNumber;

        [BsonElement]
        public readonly PlayerDecision FirstPlayerDecision;

        [BsonElement]
        public readonly PlayerDecision SecondPlayerDecision;

        [BsonElement]
        public readonly Guid WinnerId;

        [BsonElement]
        public readonly bool Draw;

        [BsonConstructor]
        public GameTurnEntity(
            Guid gameId, int turnNumber, PlayerDecision firstPlayerDecision, PlayerDecision secondPlayerDecision, Guid winnerId)
        {
            Id = Guid.NewGuid();
            GameId = gameId;
            TurnNumber = turnNumber;
            FirstPlayerDecision = firstPlayerDecision;
            SecondPlayerDecision = secondPlayerDecision;
            WinnerId = winnerId;
            if (winnerId == Guid.Empty)
                Draw = true;
        }

        public override string ToString()
        {
            return $"Turn number: {TurnNumber}\n" +
                   $"First player decision: {FirstPlayerDecision}\n" +
                   $"Second player decision: {SecondPlayerDecision}\n" +
                   $"Is draw? {Draw}\n" +
                   $"Winner ID: {WinnerId}\n\n";
        }
    }
}