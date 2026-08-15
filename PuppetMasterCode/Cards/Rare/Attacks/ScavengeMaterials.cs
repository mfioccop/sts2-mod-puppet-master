using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace PuppetMaster.PuppetMasterCode.Cards.Rare.Attacks;

public class ScavengeMaterials() : PuppetMasterCard(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(10, ValueProp.Move)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);

        var allPuppets = Owner.Character.CardPool.GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint).Where(c => c is PuppetCard);
        var puppetChoices = CardFactory.GetDistinctForCombat(Owner, allPuppets, 3, Owner.RunState.Rng.CombatCardGeneration).ToList();

        if (IsUpgraded)
        {
            CardCmd.Upgrade(puppetChoices, CardPreviewStyle.HorizontalLayout);
        }

        var puppet = await CardSelectCmd.FromChooseACardScreen(choiceContext, puppetChoices, Owner);
        if (puppet == null)
        {
            return;
        }

        puppet.SetToFreeThisTurn();
        await CardPileCmd.AddGeneratedCardToCombat(puppet, PileType.Hand, Owner);
    }
}