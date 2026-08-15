using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace PuppetMaster.PuppetMasterCode.Powers;

public abstract class PuppetPower : PuppetMasterPower, IHasSecondAmount
{
    protected int TurnsLeft;

    public void AddExtraTurns(int amount)
    {
        AssertMutable();
        if (amount < 0)
        {
            return;
        }

        TurnsLeft += amount;
        InvokeDisplayAmountChanged();
    }

    public string GetSecondAmount() => (TurnsLeft + 1).ToString();
    public abstract Task Perform(PlayerChoiceContext choiceContext);

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        var encore = Owner.GetPowerAmount<EncorePower>();
        if (encore > 0)
        {
            AddExtraTurns(encore);
        }
    }

    protected async Task RemovePuppet()
    {
        if (TurnsLeft == 0)
        {
            await PowerCmd.Remove(this);
        }

        TurnsLeft--;
        InvokeDisplayAmountChanged();
    }
}