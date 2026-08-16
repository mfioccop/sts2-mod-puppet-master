using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Saves.Runs;
using PuppetMaster.PuppetMasterCode.Cards;

namespace PuppetMaster.PuppetMasterCode.Relics;

public class BoxOfScraps : PuppetMasterRelic
{
    private const int PuppetsThreshold = 5;
    private bool _isActivating;
    private int _puppetsPlayed;

    public override RelicRarity Rarity => RelicRarity.Uncommon;
    public override bool ShowCounter => true;
    public override int DisplayAmount => !IsActivating ? PuppetsPlayed % PuppetsThreshold : PuppetsThreshold;

    [SavedProperty]
    public int PuppetsPlayed
    {
        get => _puppetsPlayed;
        set
        {
            AssertMutable();
            _puppetsPlayed = value % PuppetsThreshold;
            UpdateDisplay();
        }
    }

    private bool IsActivating
    {
        get => _isActivating;
        set
        {
            AssertMutable();
            _isActivating = value;
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        if (IsActivating)
        {
            Status = RelicStatus.Normal;
        }
        else
        {
            Status = PuppetsPlayed % PuppetsThreshold == PuppetsThreshold - 1 ? RelicStatus.Active : RelicStatus.Normal;
        }

        InvokeDisplayAmountChanged();
    }

    private async Task DoActivateVisuals()
    {
        IsActivating = true;
        Flash();
        await Cmd.Wait(1f);
        IsActivating = false;
    }

    public void NotifyPuppetPlayed()
    {
        ++PuppetsPlayed;
        if (PuppetsPlayed != 0)
            return;
        TaskHelper.RunSafely(DoActivateVisuals());
    }

    public override bool TryModifyEnergyCostInCombatLate(CardModel card, decimal originalCost, out decimal modifiedCost)
    {
        modifiedCost = originalCost;

        if (card.Owner != Owner || card is not PuppetCard)
        {
            return false;
        }

        var cardPile = card.Pile;
        if (cardPile is not { Type: PileType.Hand or PileType.Play })
        {
            return false;
        }

        if (PuppetsPlayed != PuppetsThreshold - 1)
        {
            return false;
        }

        modifiedCost = 0;
        return true;
    }

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        var card = cardPlay.Card;
        if (card.Owner != Owner || card is not PuppetCard)
        {
            return;
        }

        var cardPile = card.Pile;
        if (cardPile is not { Type: PileType.Hand or PileType.Play })
        {
            return;
        }

        NotifyPuppetPlayed();
    }
}