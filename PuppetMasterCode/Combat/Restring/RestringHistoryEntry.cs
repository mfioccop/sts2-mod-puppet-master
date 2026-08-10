using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;

namespace PuppetMaster.PuppetMasterCode.Combat.Restring;

public class RestringHistoryEntry : CombatHistoryEntry
{
    public Creature? Applier { get; }
    public Decimal Amount { get; }
    public CardPlay? CardPlay { get; }

    public override string Description
    {
        get
        {
            if (Applier != null)
            {
                var ish = new DefaultInterpolatedStringHandler(14, 4);
                ish.AppendFormatted(Applier.ModelId.Entry);
                ish.AppendLiteral(" restrung ");
                ish.AppendFormatted(Actor.ModelId.Entry);
                ish.AppendLiteral(" by ");
                ish.AppendFormatted(Amount);
                return ish.ToStringAndClear();
            }

            var ishNoApplier = new DefaultInterpolatedStringHandler(14, 4);
            ishNoApplier.AppendFormatted(Actor.ModelId.Entry);
            ishNoApplier.AppendLiteral(" was restrung by ");
            ishNoApplier.AppendFormatted(Amount);
            return ishNoApplier.ToStringAndClear();
        }
    }

    public RestringHistoryEntry(
        Creature? applier,
        Creature actor,
        Decimal amount,
        CardPlay? cardPlay,
        int roundNumber,
        CombatSide currentSide,
        CombatHistory history,
        IEnumerable<Player> players)
        : base(actor, roundNumber, currentSide, history, players)
    {
        Applier = applier;
        Amount = amount;
        CardPlay = cardPlay;
    }
}