using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace PuppetMaster.PuppetMasterCode.Vars;

public class RestringVar : DynamicVar
{
    public const string Key = "Restring";

    public RestringVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}