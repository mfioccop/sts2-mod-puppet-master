using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Saves.Runs;
using PuppetMaster.PuppetMasterCode.Powers;

namespace PuppetMaster.PuppetMasterCode.Relics;

public class RecycledCape : PuppetMasterRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(1),
        new DynamicVar("ApplicationsPerEnergy", ThreadApplicationsThreshold),
    ];

    private const int ThreadApplicationsThreshold = 3;
    private bool _isActivating;
    private int _timesThreadApplied;

    public override bool ShowCounter => true;
    public override int DisplayAmount => !IsActivating ? TimesThreadApplied % ThreadApplicationsThreshold : ThreadApplicationsThreshold;

    [SavedProperty]
    public int TimesThreadApplied
    {
        get => _timesThreadApplied;
        set
        {
            AssertMutable();
            _timesThreadApplied = value % ThreadApplicationsThreshold;
            UpdateDisplay();
        }
    }

    private bool IsActivating
    {
        get => _isActivating;
        set
        {
            AssertMutable();
            _isActivating = value;
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        if (IsActivating)
        {
            Status = RelicStatus.Normal;
        }
        else
        {
            Status = TimesThreadApplied % ThreadApplicationsThreshold == ThreadApplicationsThreshold - 1 ? RelicStatus.Active : RelicStatus.Normal;
        }

        InvokeDisplayAmountChanged();
    }

    private async Task DoActivateVisuals()
    {
        IsActivating = true;
        Flash();
        await Cmd.Wait(1f);
        IsActivating = false;
    }

    private async void NotifyThreadApplied()
    {
        ++TimesThreadApplied;
        if (TimesThreadApplied != 0)
        {
            return;
        }

        _ = TaskHelper.RunSafely(DoActivateVisuals());
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (applier != Owner.Creature || power is not ThreadPower || amount <= 0)
        {
            return;
        }

        NotifyThreadApplied();
    }
}