using BaseLib.Extensions;
using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Powers;

namespace PuppetMaster.PuppetMasterCode.Cards.Common.Attacks;

public class ThreadSeeker() : PuppetMasterCard(0, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(6, ValueProp.Move).WithUpgrade(3),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var targets = Owner.Creature.CombatState?.HittableEnemies.Where(c => c.HasPower<ThreadPower>()) ?? [];
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).TargetingFiltered(targets).Execute(choiceContext);
    }
}