using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using PuppetMaster.PuppetMasterCode.Hooks;
using PuppetMaster.PuppetMasterCode.Vars;

namespace PuppetMaster.PuppetMasterCode.Cards.Rare.Skills;

public class ReleaseTension() : PuppetMasterCard(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<StrengthPower>("StrengthLoss", 2),
        new RestringVar(),
        new DynamicVar("ThreadPerStrength", 3).WithUpgrade(-1),
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust,
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        if (play.Target == null)
        {
            return;
        }

        var strengthLoss = DynamicVars["StrengthLoss"].BaseValue;
        var restringAmount = await TryRestring(choiceContext, play.Target);
        if (restringAmount > 0)
        {
            var extraStrengthLoss = restringAmount / DynamicVars["ThreadPerStrength"].IntValue;
            strengthLoss += extraStrengthLoss;
        }

        await CommonActions.Apply<StrengthPower>(choiceContext, play.Target, this, -strengthLoss);

        if (restringAmount > 0)
        {
            await RestringHooks.AfterRestring(CombatState, choiceContext, Owner.Creature, play.Target, restringAmount, play);
        }
    }
}