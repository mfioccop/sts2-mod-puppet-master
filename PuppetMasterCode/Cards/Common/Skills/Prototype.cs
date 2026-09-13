using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace PuppetMaster.PuppetMasterCode.Cards.Common.Skills;

public class Prototype() : PuppetMasterCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(1),
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Retain,
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var card = await CommonActions.SelectSingleCard(this, SelectionScreenPrompt, choiceContext, PileType.Hand, c => c is PuppetCard);
        card?.EnergyCost.AddThisTurnOrUntilPlayed(-DynamicVars.Energy.IntValue);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}