using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace PuppetMaster.PuppetMasterCode.Powers;

public abstract class PuppetPower : PuppetMasterPower
{
    public abstract Task Perform(PlayerChoiceContext choiceContext);
}