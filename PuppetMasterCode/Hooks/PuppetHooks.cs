using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PuppetMaster.PuppetMasterCode.Powers;

namespace PuppetMaster.PuppetMasterCode.Hooks;

public class PuppetHooks
{
    public static Task AfterPuppetPerformed(ICombatState? combatState, PlayerChoiceContext ctx, PuppetPower puppet)
    {
        return HookUtils.Dispatch<IAfterPuppetPerformed>(combatState, ctx, m => m.AfterPuppetPerformed(ctx, puppet));
    }
}