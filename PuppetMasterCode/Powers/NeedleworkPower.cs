using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class NeedleworkPower : PuppetMasterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command)
    {
        if (command.Attacker != Owner)
        {
            return;
        }

        foreach (var creature in command.Results.SelectMany(r => r).Select(r => r.Receiver).Distinct())
        {
            var thread = creature.GetPowerAmount<ThreadPower>();
            if (thread > 0)
            {
                await CreatureCmd.Damage(choiceContext, creature, Amount * thread, ValueProp.Unpowered, null, command.CardPlay);
                Flash();
            }
        }
    }
}