using MegaCrit.Sts2.Core.Entities.Powers;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class ThreadPower() : PuppetMasterPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
}