using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace PuppetMaster.PuppetMasterCode.Powers;

public abstract class PuppetPower : PuppetMasterPower
{
    public abstract Task Perform(PlayerChoiceContext choiceContext);

    protected async Task RemovePuppet()
    {
        if (!SkipNextDurationTick)
        {
            await PowerCmd.Remove(this);
        }

        SkipNextDurationTick = false;
    }
}