using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player unlearns a skill
    /// </summary>
    public class PlayerUnlearnSkillEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public Skill Skill { get; set; }

        public PlayerUnlearnSkillEvent(Player pPlayer, Skill pSkill) : base()
        {
            this.Player = pPlayer;
            this.Skill = pSkill;
        }
    }

    internal class PlayerUnlearnSkillEventHelper
    {
        public IAtomicAction CreateAtomicAction(Player player, Skill skill)
        {
            PlayerUnlearnSkillEvent cEvent = new PlayerUnlearnSkillEvent(player, skill);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return CreateAtomicAction_original(cEvent.Player, cEvent.Skill);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(Player player, Skill skill)
        {
            throw new InvalidOperationException();
        }
    }
}
