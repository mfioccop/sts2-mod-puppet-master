using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace PuppetMaster.PuppetMasterCode.Cards.Rare.Skills;

public class ScrapWall() : PuppetMasterCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..MakeCalculatedBlock(6, (card, _) => card.Owner.PlayerCombatState?.AllCards.OfType<PuppetCard>().Count() ?? 0, 2)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}