using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using PuppetMaster.PuppetMasterCode.Cards;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class FreePuppetPower : PuppetMasterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool TryModifyEnergyCostInCombatLate(CardModel card, decimal originalCost, out decimal modifiedCost)
    {
        modifiedCost = originalCost;
        if (card.Owner.Creature != Owner || card is not PuppetCard)
        {
            return false;
        }

        var cardPile = card.Pile;
        if (cardPile is not { Type: PileType.Hand or PileType.Play })
        {
            return false;
        }

        modifiedCost = 0;
        return true;
    }

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        var card = cardPlay.Card;
        if (card.Owner.Creature != Owner || card is not PuppetCard)
        {
            return;
        }

        var cardPile = card.Pile;
        if (cardPile is not { Type: PileType.Hand or PileType.Play })
        {
            return;
        }

        await PowerCmd.Decrement(this);
    }
}