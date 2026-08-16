using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Vars;

namespace PuppetMaster.PuppetMasterCode.Cards.Uncommon.Attacks;

public class BreakALeg() : PuppetMasterCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(9, ValueProp.Move).WithUpgrade(3),
        new RestringVar(1),
        new PowerVar<VulnerablePower>(1).WithUpgrade(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (await TryRestring(choiceContext, play.Target) > 0)
        {
            await CommonActions.Apply<VulnerablePower>(choiceContext, this, play);
        }
    }
}