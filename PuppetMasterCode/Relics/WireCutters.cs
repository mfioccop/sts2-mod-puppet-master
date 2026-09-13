using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PuppetMaster.PuppetMasterCode.Hooks;

namespace PuppetMaster.PuppetMasterCode.Relics;

public class WireCutters : PuppetMasterRelic, IAfterRestring
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(4, ValueProp.Unpowered),
    ];

    public async Task AfterRestring(ICombatState? combatState, PlayerChoiceContext ctx, Creature? applier, Creature? target, int amount, CardPlay? cardPlay)
    {
        if (applier != Owner.Creature || target == null)
        {
            return;
        }

        Flash();
        await CreatureCmd.Damage(ctx, target, DynamicVars.Damage, Owner.Creature);
    }
}