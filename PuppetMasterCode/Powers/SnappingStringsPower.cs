using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Hooks;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class SnappingStringsPower : PuppetMasterPower, IAfterRestring
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    private bool _hasRestrungThisTurn;

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        _hasRestrungThisTurn = false;
    }

    public async Task AfterRestring(ICombatState? combatState, PlayerChoiceContext ctx, Creature? applier, Creature? target, int amount, CardPlay? cardPlay)
    {
        if (applier != Owner || _hasRestrungThisTurn)
        {
            return;
        }

        _hasRestrungThisTurn = true;
        await CreatureCmd.Damage(ctx, combatState?.HittableEnemies ?? [], Amount, ValueProp.Unpowered, Owner);
    }
}