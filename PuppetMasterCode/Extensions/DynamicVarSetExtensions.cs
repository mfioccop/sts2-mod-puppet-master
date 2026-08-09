using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PuppetMaster.PuppetMasterCode.Vars;

namespace PuppetMaster.PuppetMasterCode.Extensions;

public static class DynamicVarSetExtensions
{
    public static RestringVar? Restring(this DynamicVarSet varSet)
    {
        return varSet[RestringVar.Key] as RestringVar;
    }
}