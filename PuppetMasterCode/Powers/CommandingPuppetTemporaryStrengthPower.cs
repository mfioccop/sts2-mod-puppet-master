using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using PuppetMaster.PuppetMasterCode.Cards.Rare.Skills;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class CommandingPuppetTemporaryStrengthPower : CustomTemporaryPowerModelWrapper<CommandingPuppet, StrengthPower>
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>(),
    ];
}