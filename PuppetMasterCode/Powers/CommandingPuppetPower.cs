using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class CommandingPuppetPower : PuppetPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    protected override async Task DoPerform(PlayerChoiceContext choiceContext)
    {
        Flash();
        await Cmd.CustomScaledWait(0.2f, 0.4f);
        await PowerCmd.Apply<CommandingPuppetTemporaryStrengthPower>(choiceContext, Owner, Amount, Owner, null);
        var targets = CombatState.HittableEnemies.Where(c => c.HasPower<ThreadPower>());
        await PowerCmd.Apply<CommandingPuppetTemporaryStrengthPower>(choiceContext, targets, -Amount, Owner, null);
    }

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner))
        {
            return;
        }

        await Perform(choiceContext);
        await RemovePuppet();
    }
}