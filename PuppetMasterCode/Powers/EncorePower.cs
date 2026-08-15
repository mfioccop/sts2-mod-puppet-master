using MegaCrit.Sts2.Core.Entities.Powers;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class EncorePower : PuppetMasterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}