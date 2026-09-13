using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PuppetMaster.PuppetMasterCode.Powers;

namespace PuppetMaster.PuppetMasterCode.Relics;

public class PinCushion : PuppetMasterRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ThreadPower>(1),
    ];

    public override async Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command)
    {
        if (command.Attacker != Owner.Creature)
        {
            return;
        }

        var targets = command.Results.SelectMany(r => r).Select(r => r.Receiver).Distinct().ToList();
        if (targets.Count == 0)
        {
            return;
        }

        Flash();
        await PowerCmd.Apply<ThreadPower>(choiceContext, targets, DynamicVars.Power<ThreadPower>().BaseValue, Owner.Creature, null);
    }
}