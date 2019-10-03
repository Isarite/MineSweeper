

using System;
/**
* @(#) RevealCell.cs
*/
namespace MineServer.Models
{
	public class RevealCell : PlayerStrategy
	{
		public override void OnActivation(string data, ref Game game)
		{
            int i, j;
            string[] temp = data.Split(';');
            i = Int32.Parse(temp[0]);
            j = Int32.Parse(temp[1]);
            game.gameMap.RevealCell(i, j);
            //TODO receive map data
            //TODO loss condition
		}
	}
	
}
