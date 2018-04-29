using Eco.World;
using Eco.World.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Util
{
    public static class BlockUtils
    {
        public static Type GetBlockType(string pBlockName)
        {
            pBlockName = pBlockName.ToLower();

            if (pBlockName == "air")
                return typeof(EmptyBlock);

            Type blockType = BlockManager.BlockTypes.FirstOrDefault(t => t.Name.ToLower() == pBlockName + "floorblock");

            if (blockType != null)
                return blockType;

            blockType = BlockManager.BlockTypes.FirstOrDefault(t => t.Name.ToLower() == pBlockName);

            if (blockType != null)
                return blockType;

            return BlockManager.BlockTypes.FirstOrDefault(t => t.Name.ToLower() == pBlockName + "block");
        }
    }
}
