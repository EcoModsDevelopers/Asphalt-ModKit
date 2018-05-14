using Eco.Gameplay.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEasterEggsMod
{
    public class TestSkill : Skill
    {
        public TestSkill()
        {
            SearchEasterEggsMod.ConfigStorage.Get("test");
        }

    }
}
