using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Cards;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class PreshowPower : PuppetMasterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Player != Owner.Player || cardPlay.Card is not PuppetCard)
        {
            return;
        }

        var target = CombatState.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
        if (target == null)
        {
            return;
        }

        await CreatureCmd.Damage(choiceContext, target, Amount, ValueProp.Unpowered, Owner);
    }
}