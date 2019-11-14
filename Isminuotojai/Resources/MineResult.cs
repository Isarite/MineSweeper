using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isminuotojai.Resources
{
    public class MineResult
    {
        public bool success;
        public GameStatus status;
        public bool turn;
        public char[,] map;

        public MineResult()
        {
            success = false;
            status = GameStatus.Ongoing;
            turn = true;
            map = new char[10, 10];
        }

        public override bool Equals(object obj)
        {
            var result = obj as MineResult;
            return result != null &&
                   success == result.success &&
                   status == result.status &&
                   turn == result.turn &&
                   EqualityComparer<char[,]>.Default.Equals(map, result.map);
        }

        public override int GetHashCode()
        {
            var hashCode = -182050774;
            hashCode = hashCode * -1521134295 + success.GetHashCode();
            hashCode = hashCode * -1521134295 + status.GetHashCode();
            hashCode = hashCode * -1521134295 + turn.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<char[,]>.Default.GetHashCode(map);
            return hashCode;
        }
    }
}
