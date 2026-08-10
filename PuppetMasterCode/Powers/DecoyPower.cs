using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Combat.Restring;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class DecoyPower : PuppetMasterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
        {
            return;
        }

        var restrungThisTurn = RestringHistory.Entries().Any(e => e.HappenedThisTurn(CombatState) && e.Applier == Owner);
        if (restrungThisTurn)
        {
            Flash();
            await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
        }

        await PowerCmd.Remove(this);
    }
}