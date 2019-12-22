using System.Collections.Generic;
using System.Linq;

namespace MineServer.Models
{
    public class PlayerList
    {
        protected readonly IList<Player> List;
        public List<string> Lines;

        public PlayerList(IList<Player> list)
        {
            this.List = list;
        }

        public List<string> GetList()
        {
            return Lines;
        }

        public void BuildList()
        {
            BuildLines();
        }

        protected virtual void BuildLines()
        {
            Lines = new List<string>();
            foreach (var player in List)
                Lines.Add(TransformPlayer(player));
            SortLines();
            Lines.Insert(0,HeaderLine());
        }

        protected virtual string HeaderLine()
        {
            return "Player name\tCurrent Role";
        }

        protected virtual string TransformPlayer(Player player)
        {
            return player.ToString();
        }

        protected virtual void SortLines()
        {
            Lines.Sort();
        }

        public override string ToString()
        {
            string result = "";
            Lines.ForEach(l => result += l + "\n");
            return result;
        }
    }
}