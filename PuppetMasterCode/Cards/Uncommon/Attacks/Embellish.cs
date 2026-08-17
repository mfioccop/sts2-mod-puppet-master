using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Hooks;
using PuppetMaster.PuppetMasterCode.Vars;

namespace PuppetMaster.PuppetMasterCode.Cards.Uncommon.Attacks;

public class Embellish() : PuppetMasterCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(10, ValueProp.Move).WithUpgrade(2),
        new RestringVar(1),
        new BlockVar(10, ValueProp.Move).WithUpgrade(2),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        var restringAmount = await TryRestring(choiceContext, play.Target);
        if (restringAmount > 0)
        {
            await CommonActions.CardBlock(this, play);
            await RestringHooks.AfterRestring(CombatState, choiceContext, Owner.Creature, play.Target, restringAmount, play);
        }
    }
}