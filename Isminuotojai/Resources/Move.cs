using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isminuotojai.Resources
{
    public class Move
    {
        public MoveType Type;
        public int X;
        public int Y;

        public override bool Equals(object obj)
        {
            var move = obj as Move;
            return move != null &&
                   Type == move.Type &&
                   X == move.X &&
                   Y == move.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = -669044336;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}
