using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Hooks;
using PuppetMaster.PuppetMasterCode.Vars;

namespace PuppetMaster.PuppetMasterCode.Cards.Common.Attacks;

public class Clothesline() : PuppetMasterCard(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(6, ValueProp.Move).WithUpgrade(2),
        new RestringVar(1),
        new PowerVar<VulnerablePower>(1).WithUpgrade(1),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        foreach (var target in this.GetTargets())
        {
            var restringAmount = await TryRestring(choiceContext, target);
            if (restringAmount > 0)
            {
                await CommonActions.Apply<VulnerablePower>(choiceContext, target, this);
                await RestringHooks.AfterRestring(CombatState, choiceContext, Owner.Creature, target, restringAmount, play);
            }
        }
    }
}