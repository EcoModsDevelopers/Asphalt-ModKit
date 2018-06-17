using Asphalt.Service;
using Asphalt.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable.Items
{
    public class SkillStoreable
    {
        public SkillLevelStoreable[] Levels;

        public SkillStoreable(SkillLevelStoreable[] levels)
        {
            Levels = levels;
        }

        public T[] GetValues<T>(string pfName)
        {
            T[] result = new T[Levels.Length + 1];
            for (int i = 0; i < Levels.Length; i++)
            {
                result[i] = ReflectionUtil.GetPropertyFieldValue<T>(Levels[i], pfName);
            }
            return result;
        }

        public int[] GetSkillPointCost()
        {
            int[] result = new int[Levels.Length];
            for (int i = 0; i < Levels.Length; i++)
            {
                result[i] = Levels[i].SkillPointCost;
            }
            return result;
        }
    }

    public abstract class SkillLevelStoreable
    {
        public int SkillPointCost { get; set; }

        public SkillLevelStoreable(int skillPointCost)
        {
            SkillPointCost = skillPointCost;
        }
    }
}
