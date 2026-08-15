using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Extensions;

namespace PuppetMaster.PuppetMasterCode.Cards.Uncommon.Attacks;

public class Choreograph() : PuppetMasterCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(10, ValueProp.Move),
        new RepeatVar(1).WithUpgrade(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var hits = 1;
        hits += Owner.Creature.HasPuppet() ? DynamicVars.Repeat.IntValue : 0;
        await CommonActions.CardAttack(this, play).WithHitCount(hits).Execute(choiceContext);
    }
}