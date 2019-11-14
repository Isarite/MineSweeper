using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isminuotojai.Resources
{
    public class GameData
    {
        public string GameId { get; set; }
        public MoveSet Role { get; set; }

        public override bool Equals(object obj)
        {
            var data = obj as GameData;
            return data != null &&
                   GameId == data.GameId &&
                   Role == data.Role;
        }

        public override int GetHashCode()
        {
            var hashCode = -1521566573;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GameId);
            hashCode = hashCode * -1521134295 + Role.GetHashCode();
            return hashCode;
        }
    }
}
