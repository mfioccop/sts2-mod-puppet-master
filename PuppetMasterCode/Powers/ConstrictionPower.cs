using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class ConstrictionPower : PuppetMasterPower
{
    public const int ThreadThreshold = 6;

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner))
        {
            return;
        }

        var targets = combatState.HittableEnemies.Where(c => c.GetPowerAmount<ThreadPower>() > ThreadThreshold);
        await PowerCmd.Apply<WeakPower>(new ThrowingPlayerChoiceContext(), targets, Amount, Owner, null);
    }
}