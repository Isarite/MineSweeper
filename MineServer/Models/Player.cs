

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
/**
* @(#) Player.cs
*/
namespace MineServer.Models
{
	public class Player : IdentityUser
	{
		int name;
		
		Game currentGame;

        List<PlayerStrategy> moves = new List<PlayerStrategy>();

        public void AddMoves(string name)
        {
            switch (name)
            {
                case "mineSetter":
                    moves.Add(new SetMine());
                    moves.Add(new UnsetMine());
                    break;
                case "mineSweeper":
                    moves.Add(new RevealCell());
                    moves.Add(new MarkCell());
                    break;
            }
            
        }

        public void DoAction(string name, string data)
        {
            switch (name)
            {
                case "reveal":
                    foreach(PlayerStrategy move in moves)
                    {
                        if (move is RevealCell)
                        {
                            move.OnActivation(data, ref currentGame);
                            break;
                        }
                    }
                    break;
                case "mark":
                    foreach (PlayerStrategy move in moves)
                    {
                        if (move is MarkCell)
                        {
                            move.OnActivation(data, ref currentGame);
                            break;
                        }
                    }
                    break;
                case "set":
                    foreach (PlayerStrategy move in moves)
                    {
                        if (move is SetMine)
                        {
                            move.OnActivation(data, ref currentGame);
                            break;
                        }
                    }
                    break;
                case "unset":
                    foreach (PlayerStrategy move in moves)
                    {
                        if (move is UnsetMine)
                        {
                            move.OnActivation(data, ref currentGame);
                            break;
                        }
                    }
                    break;
            }
        }
		
	}
	
}
