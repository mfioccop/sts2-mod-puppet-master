using BaseLib.Abstracts;
using PuppetMaster.PuppetMasterCode.Extensions;
using Godot;

namespace PuppetMaster.PuppetMasterCode.Character;

public class PuppetMasterRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => PuppetMaster.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}