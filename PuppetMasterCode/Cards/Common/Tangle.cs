using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using PuppetMaster.PuppetMasterCode.Extensions;
using PuppetMaster.PuppetMasterCode.Vars;

namespace PuppetMaster.PuppetMasterCode.Cards.Common;

public class Tangle() : PuppetMasterCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<WeakPower>(1),
        new RestringVar(2).WithUpgrade(-1),
        new PowerVar<WeakPower>("ExtraWeak", 1),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        if (play.Target == null)
        {
            return;
        }

        var weakAmount = DynamicVars.Weak.BaseValue;
        if (await TryRestring(choiceContext, play.Target, DynamicVars.Restring()) > 0)
        {
            weakAmount += DynamicVars["ExtraWeak"].BaseValue;
        }

        await CommonActions.Apply<WeakPower>(choiceContext, play.Target, this, weakAmount);
    }
}