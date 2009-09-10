using System;
using System.Collections.Generic;
using System.Reflection;
using Tibia.Objects;

namespace Tibia.Constants
{
    public static class TileLists
    {

        #region Up
        public static List<uint> Up = new List<uint>
            {
            Tiles.Up.Stairs, 
            Tiles.Up.Ramp, 
            Tiles.Up.Ramp1, 
            Tiles.Up.Ramp2, 
            Tiles.Up.Ramp3, 
            Tiles.Up.WoodenStairs, 
            Tiles.Up.Ramp4, 
            Tiles.Up.Ramp5, 
            Tiles.Up.Ramp6, 
            Tiles.Up.Ramp7, 
            Tiles.Up.Ramp8, 
            Tiles.Up.Ramp9, 
            Tiles.Up.Ramp10, 
            Tiles.Up.Ramp11, 
            Tiles.Up.Ramp12, 
            Tiles.Up.Ramp13, 
            Tiles.Up.Ramp14, 
            Tiles.Up.Ramp15, 
            Tiles.Up.StoneStairs, 
            Tiles.Up.StoneStairs1, 
            Tiles.Up.Stairs1, 
            Tiles.Up.Stairs2, 
            Tiles.Up.Stairs3, 
            Tiles.Up.Ramp16, 
            Tiles.Up.Ramp17, 
            Tiles.Up.Ramp18, 
            Tiles.Up.Ramp19, 
            Tiles.Up.StoneStairs2, 
            Tiles.Up.StoneStairs3, 
            Tiles.Up.Ramp20, 
            Tiles.Up.Ramp21, 
            Tiles.Up.Ramp22, 
            Tiles.Up.Ramp23, 
            Tiles.Up.WoodenStairs1, 
            Tiles.Up.Ramp24, 
            Tiles.Up.StoneStairs4, 
            Tiles.Up.Roof, 
            Tiles.Up.Roof1, 
            Tiles.Up.Roof2, 
            Tiles.Up.Roof3, 
            Tiles.Up.CorkscrewStairs, 
            Tiles.Up.Ramp25, 
            Tiles.Up.Ramp26, 
            };
        #endregion

        #region Down
        public static List<uint> Down = new List<uint>
            {
            Tiles.Down.Grass, 
            Tiles.Down.Pitfall, 
            Tiles.Down.Trapdoor, 
            Tiles.Down.Trapdoor1, 
            Tiles.Down.Hole, 
            Tiles.Down.Hole1, 
            Tiles.Down.Trapdoor2, 
            Tiles.Down.Trapdoor3, 
            Tiles.Down.Stairs4, 
            Tiles.Down.Stairs5, 
            Tiles.Down.Stairs6, 
            Tiles.Down.Trapdoor4, 
            Tiles.Down.Ladder, 
            Tiles.Down.Trapdoor5, 
            Tiles.Down.Stairs7, 
            Tiles.Down.Stairs8, 
            Tiles.Down.Stairs9, 
            Tiles.Down.OpenTrapdoor, 
            Tiles.Down.Hole2, 
            Tiles.Down.Hole3, 
            Tiles.Down.Hole4, 
            Tiles.Down.Hole5, 
            Tiles.Down.Hole6, 
            Tiles.Down.Hole7, 
            Tiles.Down.Hole8, 
            Tiles.Down.Hole9, 
            Tiles.Down.Hole10, 
            Tiles.Down.Hole11, 
            Tiles.Down.Trapdoor6, 
            Tiles.Down.Ladder1, 
            Tiles.Down.Ladder2, 
            Tiles.Down.Trapdoor7, 
            Tiles.Down.Stairway, 
            Tiles.Down.Stairs10, 
            Tiles.Down.Stairs11, 
            Tiles.Down.Pitfall1, 
            Tiles.Down.Pitfall2, 
            Tiles.Down.EarthHole, 
            Tiles.Down.Stairs12, 
            Tiles.Down.Stairs13, 
            Tiles.Down.Stairs14, 
            Tiles.Down.Stairs15, 
            Tiles.Down.Stairs16, 
            Tiles.Down.Trapdoor8, 
            Tiles.Down.Trapdoor9, 
            Tiles.Down.Hole12, 
            Tiles.Down.Trapdoor10, 
            Tiles.Down.Ramp27, 
            Tiles.Down.Ramp28, 
            Tiles.Down.Ramp29, 
            Tiles.Down.Ramp30, 
            Tiles.Down.Trapdoor11, 
            Tiles.Down.Trapdoor12, 
            Tiles.Down.IceHut, 
            Tiles.Down.IceHut1, 
            Tiles.Down.IceHut2, 
            Tiles.Down.Ramp31, 
            Tiles.Down.Ramp32, 
            Tiles.Down.Ramp33, 
            Tiles.Down.Ramp34, 
            Tiles.Down.Ramp35, 
            Tiles.Down.Ramp36, 
            Tiles.Down.Ramp37, 
            Tiles.Down.Ramp38, 
            Tiles.Down.Trapdoor13, 
            Tiles.Down.WoodenCoffin, 
            Tiles.Down.WoodenCoffin1, 
            Tiles.Down.LargeHole, 
            Tiles.Down.Hole13, 
            Tiles.Down.LargeHole1, 
            Tiles.Down.Stairs17, 
            Tiles.Down.CaveEntrance, 
            Tiles.Down.CaveEntrance1, 
            Tiles.Down.CaveEntrance2, 
            Tiles.Down.CaveEntrance3, 
            Tiles.Down.CaveEntrance4, 
            Tiles.Down.CaveEntrance5, 
            Tiles.Down.Hole14, 
            Tiles.Down.Hole15, 
            Tiles.Down.Hole16, 
            Tiles.Down.Hole17, 
            Tiles.Down.SomethingCrawling, 
            Tiles.Down.Hole18, 
            Tiles.Down.Hole19, 
            Tiles.Down.LargeHole2, 
            Tiles.Down.Trapdoor14, 
            Tiles.Down.Trapdoor15, 
            Tiles.Down.Trapdoor16, 
            Tiles.Down.Trapdoor17, 
            Tiles.Down.Trapdoor18, 
            Tiles.Down.Stairs18, 
            Tiles.Down.Stairs19, 
            Tiles.Down.Trapdoor19, 
            Tiles.Down.Trapdoor20, 
            Tiles.Down.Trapdoor21, 
            Tiles.Down.Hole20, 
            Tiles.Down.TunnelEntrance, 
            Tiles.Down.Ramp39, 
            Tiles.Down.Ramp40, 
            Tiles.Down.Ramp41, 
            Tiles.Down.Ramp42, 
            Tiles.Down.Ramp43, 
            Tiles.Down.Ramp44, 
            Tiles.Down.Ramp45, 
            Tiles.Down.Ramp46, 
            Tiles.Down.Hole21, 
            Tiles.Down.Hole22, 
            Tiles.Down.Trapdoor22, 
            Tiles.Down.Trapdoor23, 
            Tiles.Down.WaterVortex, 
            Tiles.Down.LargeHole3, 
            Tiles.Down.CorkscrewStairs1, 
            Tiles.Down.Stairs20, 
            Tiles.Down.OpenTrapdoor1, 
            Tiles.Down.CorkscrewStairs2, 
            };
        #endregion

        #region UpUse
        public static List<uint> UpUse = new List<uint>
            {
            Tiles.UpUse.Ladder3, 
            Tiles.UpUse.Ladder4, 
            Tiles.UpUse.RopeLadder, 
            Tiles.UpUse.Ladder5, 
            Tiles.UpUse.Ladder6, 
            };
        #endregion

        #region Rope
        public static List<uint> Rope = new List<uint>
            {
            Tiles.Rope.DirtFloor, 
            Tiles.Rope.StoneTile, 
            Tiles.Rope.DirtFloor1, 
            Tiles.Rope.DirtFloor2, 
            };
        #endregion

        #region DownUse
        public static List<uint> DownUse = new List<uint>
            {
            Tiles.DownUse.SewerGrate, 
            Tiles.DownUse.SewerGrate1, 
            };
        #endregion

        #region Shovel
        public static List<uint> Shovel = new List<uint>
            {
            Tiles.Shovel.StonePile, 
            Tiles.Shovel.LooseStonePile, 
            Tiles.Shovel.LooseIcePile, 
            Tiles.Shovel.LooseStonePile1, 
            Tiles.Shovel.Hole23, 
            };
        #endregion
    }
}

