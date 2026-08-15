using MegaCrit.Sts2.Core.Entities.Creatures;
using PuppetMaster.PuppetMasterCode.Powers;

namespace PuppetMaster.PuppetMasterCode.Extensions;

public static class CreatureExtensions
{
    public static IEnumerable<PuppetPower> Puppets(this Creature creature)
    {
        return creature.Powers.OfType<PuppetPower>();
    }

    public static bool HasPuppet(this Creature creature)
    {
        return Puppets(creature).Any();
    }
}