/**
 * @(#) CellFactory.cs
 */

namespace MineServer.Models
{
	public class CellFactory : Factory
	{
		public override Cell  Create( string type )
		{
            switch (type)
            {
                case "ExplodedTNT":
                    return new ExplodedTNT();
                case "Marker":
                    return new Marker();
                case "WrongTNT":
                    return new WrongTNT();
                case "TNT":
                    return new TNT();
                case "Revealed":
                    return new Revealed();
                default:
                    return new Unknown();
            }
		}
		
	}
	
}
