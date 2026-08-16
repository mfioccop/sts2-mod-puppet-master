using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class TemplatesPower : PuppetMasterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override Decimal ModifyHandDraw(Player player, Decimal count)
    {
        if (player != Owner.Player || AmountOnTurnStart == 0)
        {
            return count;
        }

        if (player.Creature.CombatState?.HittableEnemies.Any(c => c.HasPower<ThreadPower>()) ?? false)
        {
            Flash();
            return count + Amount;
        }

        return count;
    }
}