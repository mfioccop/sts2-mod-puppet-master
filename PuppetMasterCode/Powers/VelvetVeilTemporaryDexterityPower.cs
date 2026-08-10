using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using PuppetMaster.PuppetMasterCode.Cards.Uncommon.Skills;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class VelvetVeilTemporaryDexterityPower : CustomTemporaryPowerModelWrapper<VelvetVeil, DexterityPower>
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<DexterityPower>(),
    ];
}