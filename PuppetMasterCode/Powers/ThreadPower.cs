using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class ThreadPower() : PuppetMasterPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public static IHoverTip HoverTip => HoverTipFactory.FromPower<ThreadPower>();
}