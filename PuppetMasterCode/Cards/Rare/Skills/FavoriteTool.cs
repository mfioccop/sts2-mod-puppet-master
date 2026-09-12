using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace PuppetMaster.PuppetMasterCode.Cards.Rare.Skills;

public class FavoriteTool() : PuppetMasterCard(-1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override bool HasEnergyCostX => true;
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var cards = await CardPileCmd.Draw(choiceContext, ResolveEnergyXValue(), Owner);
        if (!cards.Any())
        {
            // Safeguard to prevent using the X-cost card at 0 energy to draw no cards but still gain the cost reduction effect.
            return;
        }

        var card = await CommonActions.SelectSingleCard(this, SelectionScreenPrompt, choiceContext, PileType.Hand);
        if (card == null)
        {
            return;
        }

        if (IsUpgraded)
        {
            card.SetToFreeThisCombat();
        }
        else
        {
            card.SetToFreeThisTurn();
        }
    }
}