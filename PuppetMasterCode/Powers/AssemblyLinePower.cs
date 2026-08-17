using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using PuppetMaster.PuppetMasterCode.Hooks;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class AssemblyLinePower : PuppetMasterPower, IAfterRestring
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public async Task AfterRestring(ICombatState? combatState, PlayerChoiceContext ctx, Creature? applier, Creature? target, int amount, CardPlay? cardPlay)
    {
        if (applier != Owner || Owner.Player == null)
        {
            return;
        }

        var rng = Owner.Player.RunState.Rng.CombatCardSelection;

        // Track cards we've already set to free, to avoid choosing the same card if we have multiple stacks of this power
        var chosen = new List<CardModel>();

        for (var i = 0; i < Amount; i++)
        {
            var cardsInHand = PileType.Hand.GetPile(Owner.Player).Cards.Where(c => !chosen.Contains(c)).ToList();
            var cardsWithCost = cardsInHand.Where(c => c.EnergyCost.GetWithModifiers(CostModifiers.None) > 0 || c.BaseStarCost > 0).ToList();

            // Prioritize which card to make free
            var card =
                (
                    (
                        // 1. Card that has a base cost and cost after modifiers
                        rng.NextItem(cardsWithCost.Where(c => c.CostsEnergyOrStars(true)))

                        // 2. Card that has a cost after modifiers
                        ?? rng.NextItem(cardsInHand.Where(c => c.CostsEnergyOrStars(true)))
                    )
                    // 3. Card that has a base cost but is free now
                    ?? rng.NextItem(cardsWithCost)
                )
                // 4. Fallback: card that is free
                ?? rng.NextItem(cardsInHand);

            if (card != null)
            {
                chosen.Add(card);
                card.SetToFreeThisTurn();
            }
        }
    }
}