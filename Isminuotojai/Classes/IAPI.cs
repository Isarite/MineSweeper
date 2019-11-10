using Isminuotojai.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isminuotojai.Classes
{
    interface IAPI
    {
        Task<MoveSet> StartGameAsync();
        Task<bool> GetTokenAsync(PlayerData player);
        Task<MineResult> SurrenderAsync();
        Task<MineResult> UpdateAsync();
        Task<MineResult> DoMoveAsync(Move move);
        Task<bool> CreatePlayerAsync(PlayerData player);
    }
}
