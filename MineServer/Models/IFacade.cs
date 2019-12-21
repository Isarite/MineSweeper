using System.Threading.Tasks;
using MineServer.Resources;

namespace MineServer.Models
{
    public interface IFacade
    {
        Task<bool> CreatePlayer(string name, string pass);

        Task<byte[]> GetToken(PlayerData player);

        Task<Result> DoMove(Move move, int id, string userId);

        Task<Result> Surrender(int id, string userId);

        Task<GameData> StartGame(string userId);

        Task<Result> Update(string userId, int id);

        Task<string> GetPlayers();
        
        //Task<Result> ResetState(int id, string userId);
    }
}
