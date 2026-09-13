using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class ThreadbarePower : PuppetMasterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
        {
            return;
        }

        var enemiesWithoutThread = CombatState.HittableEnemies.Where(c => !c.HasPower<ThreadPower>());
        var target = CombatState.RunState.Rng.CombatTargets.NextItem(enemiesWithoutThread);
        if (target == null)
        {
            return;
        }

        Flash();
        await PowerCmd.Apply<ThreadPower>(choiceContext, target, Amount, Owner, null);
    }
}