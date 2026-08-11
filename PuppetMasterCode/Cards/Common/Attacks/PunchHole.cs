using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Vars;

namespace PuppetMaster.PuppetMasterCode.Cards.Common.Attacks;

public class PunchHole() : PuppetMasterCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(5, ValueProp.Move),
        new RestringVar(1),
        new DamageVar("UnblockableDamage", 3, ValueProp.Move | ValueProp.Unblockable).WithUpgrade(3),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (await TryRestring(choiceContext, play.Target) > 0)
        {
            var unblockableDamage = (DamageVar)DynamicVars["UnblockableDamage"];
            await CommonActions.CardAttack(this, play, play.Target, unblockableDamage.BaseValue, unblockableDamage.Props).Execute(choiceContext);
        }
    }
}