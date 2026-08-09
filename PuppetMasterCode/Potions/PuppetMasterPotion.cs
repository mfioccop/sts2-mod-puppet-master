using BaseLib.Abstracts;
using BaseLib.Utils;
using PuppetMaster.PuppetMasterCode.Character;

namespace PuppetMaster.PuppetMasterCode.Potions;

[Pool(typeof(PuppetMasterPotionPool))]
public abstract class PuppetMasterPotion : CustomPotionModel;