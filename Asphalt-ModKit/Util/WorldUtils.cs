using Eco.Shared.Math;
using Eco.Simulation.Settings;
using Eco.Simulation.Types;
using Eco.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Util
{
    public static class WorldUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlantSpecies GetPlantSpecies(Type pBlockType)
        {
            return EcoDef.Obj.Species.OfType<PlantSpecies>().First(ps => ps.BlockType.Type == pBlockType);
        }

        public static IEnumerable<Block> GetTopBlocks(Vector3i pPosition, int pRange)
        {
            for (int x = pPosition.X - pRange; x < pPosition.X + pRange; x++)
                for (int z = pPosition.Z - pRange; z < pPosition.Z + pRange; z++)
                {
                    yield return World.GetTopBlock(new Vector2i(x, z));
                }
        }

        public static int RemoveTopBlocksInCircle(Vector3i position, int range)
        {
            int count = 0;


            return count;
        }

        //https://codereview.stackexchange.com/questions/70540/get-all-points-on-a-uniform-discrete-grid-inside-a-circles-radius
        private static IEnumerable<Vector2> GetPointsInCircle(Vector2 pCircleCenter, float pRadius, Vector2 pGridCenter, Vector2? pGridStep = null)
        {
            Vector2 gridStep = pGridStep ?? Vector2.one;

            if (pRadius <= 0)
                throw new ArgumentOutOfRangeException("radius", "Argument must be positive.");

            if (gridStep.x <= 0 || gridStep.y <= 0)
                throw new ArgumentOutOfRangeException("gridStep", "Argument must contain positive components only.");
            
            // Loop bounds for X dimension:
            int i1 = (int)Math.Ceiling((pCircleCenter.x - pGridCenter.x - pRadius) / gridStep.x);
            int i2 = (int)Math.Floor((pCircleCenter.x - pGridCenter.x + pRadius) / gridStep.x);

            // Constant square of the radius:
            float radius2 = pRadius * pRadius;

            for (int i = i1; i <= i2; i++)
            {
                // X-coordinate for the points of the i-th circle segment:
                float x = pGridCenter.x + i * gridStep.x;

                // Local radius of the circle segment (half-length of chord) calulated in 3 steps.
                // Step 1. Offset of the (x, *) from the (circleCenter.x, *):
                float localRadius = pCircleCenter.x - x;
                // Step 2. Square of it:
                localRadius *= localRadius;
                // Step 3. Local radius of the circle segment:
                localRadius = (float)Math.Sqrt(radius2 - localRadius);

                // Loop bounds for Y dimension:
                int j1 = (int)Math.Ceiling((pCircleCenter.y - pGridCenter.y - localRadius) / gridStep.y);
                int j2 = (int)Math.Floor((pCircleCenter.y - pGridCenter.y + localRadius) / gridStep.y);

                for (int j = j1; j <= j2; j++)
                {
                    yield return new Vector2(x, pGridCenter.y + j * gridStep.y);
                }
            }
        }

        /*
        public static void GetPulpAroundPoint(User user, Vector3i position, int range)
        {
                for (int i = -range; i < range; i++)
                {
                    for (int j = -range; j < range; j++)
                    {
                        if (i != 0 || j != 0)
                        {
                            Vector3i val = World.GetTopPos(new Vector2i(position.x + i, position.z + j)) + Vector3i.Up;
                            Block blockProbablyTop = World.GetBlockProbablyTop(val);
                            if (blockProbablyTop.Is<TreeDebris>() && val != position && Vector3i.Distance(val, position) < (float)range && (bool)user.Inventory.TryAddItems<WoodPulpItem>(5, null))
                            {
                                World.DeleteBlock(val);
                            }
                        }
                    }
                }
        }*/

    }
}
