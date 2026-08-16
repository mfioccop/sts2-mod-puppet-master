using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using PuppetMaster.PuppetMasterCode.Hooks;
using PuppetMaster.PuppetMasterCode.Relics;

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
    protected abstract Task DoPerform(PlayerChoiceContext choiceContext);

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        var encore = Owner.GetPowerAmount<EncorePower>();
        if (encore > 0)
        {
            AddExtraTurns(encore);
        }
    }

    public async Task Perform(PlayerChoiceContext choiceContext)
    {
        await DoPerform(choiceContext);
        await PuppetHooks.AfterPuppetPerformed(Owner.CombatState, choiceContext, this);
        if (Owner.Player?.TryGetRelic<ClockworkUnderstudy>(out var relic) ?? false)
        {
            relic.Flash();
            await DoPerform(choiceContext);
            await PuppetHooks.AfterPuppetPerformed(Owner.CombatState, choiceContext, this);
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