using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using System;
using System.Linq;

namespace Asphalt.Util
{
    public static class SkillsUtil
    {
        public static bool HasSkillLevel(User user, Type skillType, int level)
        {
            Skill[] skills = user.Skillset.Skills;
            return skills.Any(s => s.Type == skillType && s.Level >= level);
        }

        public static int GetSkillLevel(User user, Type skillType)
        {
            Skill[] skills = user.Skillset.Skills;

            foreach (Skill skill in skills)
            {
                if (skill.Type == skillType)
                {
                    return skill.Level;
                }
            }
            return 0;
        }
    }

}
