using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PuppetMaster.PuppetMasterCode.Hooks;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class ReduceReuseRecyclePower : PuppetMasterPower, IAfterRestring
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public async Task AfterRestring(ICombatState? combatState, PlayerChoiceContext ctx, Creature? applier, Creature? target, int amount, CardPlay? cardPlay)
    {
        if (applier != Owner || target == null || amount == 0)
        {
            return;
        }

        Flash();
        await PowerCmd.Apply<ThreadPower>(ctx, target, Amount, applier, null);
    }
}