using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable.Items
{
    public class ToolStoreable
    {
        public float durability;
        public float calories;
        public CraftIngredients repair;
        public CraftingStoreable craft;

        public ToolStoreable()
        {
        }

        public ToolStoreable(float _durability, float _calories, CraftIngredients _repair, CraftingStoreable _craft)
        {
            durability = _durability;
            calories = _calories;
            repair = _repair;
            craft = _craft;
        }
    }
}
