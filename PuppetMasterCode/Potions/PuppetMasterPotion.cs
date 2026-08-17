using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using PuppetMaster.PuppetMasterCode.Character;
using PuppetMaster.PuppetMasterCode.Extensions;

namespace PuppetMaster.PuppetMasterCode.Potions;

[Pool(typeof(PuppetMasterPotionPool))]
public abstract class PuppetMasterPotion : CustomPotionModel
{
    public override string? CustomPackedImagePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();
}