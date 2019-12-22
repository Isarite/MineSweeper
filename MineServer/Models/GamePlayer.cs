using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineServer.Models
{
    public class GamePlayer
    {
        public string UserId { get; set; }
        public Player User { get; set; }

        public int? GameId { get; set; }
        public Game Game { get; set; }
    }
}
