using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PuppetMaster.PuppetMasterCode.Cards;

namespace PuppetMaster.PuppetMasterCode.Potions;

public class PotionPuppet : PuppetMasterPotion
{
    public override PotionRarity Rarity => PotionRarity.Rare;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        var allPuppets = Owner.Character.CardPool.GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint).OfType<PuppetCard>().ToList();
        var rng = Owner.RunState.Rng.CombatCardGeneration;

        var commonPuppets = allPuppets.Where(c => c.Rarity == CardRarity.Common);
        var common = CardFactory.GetDistinctForCombat(Owner, commonPuppets, 1, rng).FirstOrDefault();
        if (common != null)
        {
            common.SetToFreeThisTurn();
            await CardPileCmd.AddGeneratedCardToCombat(common, PileType.Hand, Owner);
        }

        var uncommonPuppets = allPuppets.Where(c => c.Rarity == CardRarity.Uncommon);
        var uncommon = CardFactory.GetDistinctForCombat(Owner, uncommonPuppets, 1, rng).FirstOrDefault();
        if (uncommon != null)
        {
            uncommon.SetToFreeThisTurn();
            await CardPileCmd.AddGeneratedCardToCombat(uncommon, PileType.Hand, Owner);
        }

        var rarePuppets = allPuppets.Where(c => c.Rarity == CardRarity.Rare);
        var rare = CardFactory.GetDistinctForCombat(Owner, rarePuppets, 1, rng).FirstOrDefault();
        if (rare != null)
        {
            rare.SetToFreeThisTurn();
            await CardPileCmd.AddGeneratedCardToCombat(rare, PileType.Hand, Owner);
        }
    }
}