using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;

namespace PuppetMaster.PuppetMasterCode.Combat.Restring;

public class RestringHistory
{
    public static RestringHistory Instance => new();

    private static readonly ConditionalWeakTable<CombatHistory, RestringHistory> Restrings = [];

    private readonly List<RestringHistoryEntry> _entries = [];

    public event Action? Changed;

    public static IEnumerable<RestringHistoryEntry> Entries()
    {
        return Entries(CombatManager.Instance.History);
    }

    public static IEnumerable<RestringHistoryEntry> Entries(CombatHistory history)
    {
        return Restrings.TryGetValue(history, out var restrings) ? restrings._entries : [];
    }

    public void Restrung(ICombatState combatState, Creature applier, Creature receiver, int amount, CardPlay? cardPlay = null)
    {
        var entry = new RestringHistoryEntry(
            applier,
            receiver,
            amount,
            cardPlay,
            combatState.RoundNumber,
            combatState.CurrentSide,
            CombatManager.Instance.History,
            combatState.Players
        );
        Add(combatState, entry);
    }

    private void Add(ICombatState combatState, RestringHistoryEntry entry)
    {
        if (!combatState.IsLiveCombat())
        {
            return;
        }

        Restrings.GetOrCreateValue(CombatManager.Instance.History)._entries.Add(entry);
        Changed?.Invoke();
    }
}