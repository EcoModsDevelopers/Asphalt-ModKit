using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player unlearns a skill
    /// </summary>
    public class PlayerUnlearnSkillEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public Skill Skill { get; set; }

        public PlayerUnlearnSkillEvent(ref Player pPlayer, ref Skill pSkill) : base()
        {
            this.Player = pPlayer;
            this.Skill = pSkill;
        }
    }

    internal class PlayerUnlearnSkillEventHelper
    {
        public static bool Prefix(ref Player player, ref Skill skill, ref IAtomicAction __result)
        {
            PlayerUnlearnSkillEvent cEvent = new PlayerUnlearnSkillEvent(ref player, ref skill);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (cEvent.IsCancelled())
            {
                __result = new FailedAtomicAction(new LocString());
                return false;
            }

            return true;
        }
    }
}
