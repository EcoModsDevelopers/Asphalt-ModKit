using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable.Items
{
    public class CraftingStoreable
    {
        public bool enable;
        public string table;
        public float craftMinutesTime;
        public CraftIngredients[] ingredients;

        public CraftingStoreable()
        {
        }

        public CraftingStoreable(bool _enable, string _table, float _craftMinutesTime, CraftIngredients[] _craftCost)
        {
            enable = _enable;
            table = _table;
            craftMinutesTime = _craftMinutesTime;
            ingredients = _craftCost;
        }
    }

    public class CraftIngredients
    {
        public string item;
        public float quantity;

        public CraftIngredients()
        {
        }

        public CraftIngredients(string ingredient, float cost)
        {
            item = ingredient;
            quantity = cost;
        }
    }
}
