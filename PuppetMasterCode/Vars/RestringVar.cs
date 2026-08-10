using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace PuppetMaster.PuppetMasterCode.Vars;

public class RestringVar : DynamicVar
{
    public const string Key = "Restring";

    public bool ConsumeAll => BaseValue <= 0;

    public RestringVar(decimal baseValue = 0) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}