using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class AncientSchematicsPower : PuppetMasterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    private const int EnergyIncrement = 4;

    public override int DisplayAmount => EnergyIncrement - GetInternalData<Data>().EnergySpent % EnergyIncrement;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.ForEnergy(this),
    ];

    protected override object InitInternalData() => new Data();

    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(EnergyIncrement),
    ];

    public override async Task AfterEnergySpent(CardModel card, int amount)
    {
        if (card.Owner.Creature != Owner || Owner.Player == null || amount <= 0)
        {
            return;
        }

        var data = GetInternalData<Data>();
        data.EnergySpent += amount;
        var triggers = data.EnergySpent / EnergyIncrement - data.TriggerCount;
        if (triggers > 0)
        {
            Flash();
            var cardsDrawn = await CardPileCmd.Draw(new ThrowingPlayerChoiceContext(), Amount * triggers, Owner.Player);
            foreach (var c in cardsDrawn)
            {
                c.SetToFreeThisTurn();
            }

            data.TriggerCount += triggers;
        }

        InvokeDisplayAmountChanged();
    }

    private class Data
    {
        public int EnergySpent;
        public int TriggerCount;
    }
}