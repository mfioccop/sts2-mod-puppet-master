using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PuppetMaster.PuppetMasterCode.Powers;
using PuppetMaster.PuppetMasterCode.Vars;

namespace PuppetMaster.PuppetMasterCode.Cards.Uncommon;

public class Unravel() : PuppetMasterCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new RestringVar(),
        ..MakeCalculatedDamage(5, (_, target) => target?.GetPowerAmount<ThreadPower>() ?? 0, 3)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        if (play.Target == null)
        {
            return;
        }

        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await TryRestring(choiceContext, play.Target);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(3M);
        DynamicVars.ExtraDamage.UpgradeValueBy(2M);
    }
}