using PolyOne.Collision;
using PolyOne.Utility;

namespace RTTP.Platforms
{
    public class LevelTiles : Solid
    {
        public Grid Grid { get; private set; }

        public LevelTiles(bool[,] solidData)
        {
            this.Active = false;
            this.Collider = (this.Grid = new Grid(TileInformation.TileWidth, TileInformation.TileHeight, solidData));
        }
    }
}
