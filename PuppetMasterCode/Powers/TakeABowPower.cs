using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Hooks;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class TakeABowPower : PuppetMasterPower, IAfterPuppetPerformed
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public async Task AfterPuppetPerformed(PlayerChoiceContext choiceContext, PuppetPower puppet)
    {
        if (puppet.Owner != Owner)
        {
            return;
        }

        await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
    }
}