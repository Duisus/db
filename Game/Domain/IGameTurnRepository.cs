using System;
using System.Collections.Generic;

namespace Game.Domain
{
    public interface IGameTurnRepository
    {
        GameTurnEntity Insert(GameTurnEntity turn);
        GameTurnEntity FindById(Guid id);
        List<GameTurnEntity> GetLastTurns(Guid gameId, int limit = 5);
    }
}