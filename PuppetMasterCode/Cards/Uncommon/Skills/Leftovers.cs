using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PuppetMaster.PuppetMasterCode.Extensions;
using PuppetMaster.PuppetMasterCode.Powers;

namespace PuppetMaster.PuppetMasterCode.Cards.Uncommon.Skills;

public class Leftovers() : PuppetMasterCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..MakeCalculatedVar("Thread", 2, (card, _) => card.Owner.Creature.Puppets().Count(), 3)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        if (play.Target == null)
        {
            return;
        }

        await PowerCmd.Apply<ThreadPower>(choiceContext, play.Target, DynamicVars["Thread"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}