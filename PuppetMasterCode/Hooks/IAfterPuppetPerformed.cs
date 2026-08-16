using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PuppetMaster.PuppetMasterCode.Powers;

namespace PuppetMaster.PuppetMasterCode.Hooks;

public interface IAfterPuppetPerformed
{
    Task AfterPuppetPerformed(PlayerChoiceContext choiceContext, PuppetPower puppet);
}