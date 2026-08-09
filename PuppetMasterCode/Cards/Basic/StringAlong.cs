using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Extensions;
using PuppetMaster.PuppetMasterCode.Vars;

namespace PuppetMaster.PuppetMasterCode.Cards.Basic;

public class StringAlong() : PuppetMasterCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(3, ValueProp.Move),
        new RestringVar(3),
        new PowerVar<StrengthPower>(1),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        if (play.Target == null)
        {
            return;
        }

        await CommonActions.CardAttack(this, play).Execute(choiceContext);

        if (await TryRestring(choiceContext, play.Target, DynamicVars.Restring()) > 0)
        {
            await CommonActions.Apply<StrengthPower>(choiceContext, play.Target, this, -DynamicVars.Strength.BaseValue);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}