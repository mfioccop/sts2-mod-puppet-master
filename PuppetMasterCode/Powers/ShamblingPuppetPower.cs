using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class ShamblingPuppetPower : PuppetPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    public override async Task Perform(PlayerChoiceContext choiceContext)
    {
        Flash();
        await Cmd.CustomScaledWait(0.2f, 0.4f);
        var targets = CombatState.HittableEnemies.Where(c => c.HasPower<ThreadPower>());
        await PowerCmd.Apply<WeakPower>(choiceContext, targets, Amount, Owner, null);
    }

    public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
        {
            return;
        }

        await Perform(choiceContext);
        await PowerCmd.Remove(this);
    }
}