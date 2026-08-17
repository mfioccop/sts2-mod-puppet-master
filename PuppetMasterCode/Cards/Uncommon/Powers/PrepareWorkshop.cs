using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace PuppetMaster.PuppetMasterCode.Cards.Uncommon.Powers;

public class PrepareWorkshop() : PuppetMasterCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<StrengthPower>(1),
        new PowerVar<VigorPower>(6).WithUpgrade(6),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.ApplySelf<StrengthPower>(choiceContext, this);
        await CommonActions.ApplySelf<VigorPower>(choiceContext, this);
    }
}