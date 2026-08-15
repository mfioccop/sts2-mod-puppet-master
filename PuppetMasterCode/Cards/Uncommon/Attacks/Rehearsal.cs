using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Extensions;

namespace PuppetMaster.PuppetMasterCode.Cards.Uncommon.Attacks;

public class Rehearsal() : PuppetMasterCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(14, ValueProp.Move).WithUpgrade(4)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        var puppets = Owner.Creature.Puppets();
        var puppet = Owner.RunState.Rng.Niche.NextItem(puppets);
        if (puppet != null)
        {
            await puppet.Perform(choiceContext);
        }
    }
}