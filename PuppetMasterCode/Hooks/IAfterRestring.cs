using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace PuppetMaster.PuppetMasterCode.Hooks;

public interface IAfterRestring
{
    Task AfterRestring(ICombatState? combatState, PlayerChoiceContext ctx, Creature? applier, Creature? target, int amount, CardPlay? cardPlay);
}