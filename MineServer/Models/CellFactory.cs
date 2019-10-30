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
                    return new ExplodedTnt();
                case "Marker":
                    return new Marker();
                case "WrongTNT":
                    return new WrongTnt();
                case "TNT":
                    return new Tnt();
                case "Revealed":
                    return new Revealed();
                default:
                    return new Unknown();
            }
		}
		
	}
	
}
