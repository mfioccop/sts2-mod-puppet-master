using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PuppetMaster.PuppetMasterCode.Hooks;

namespace PuppetMaster.PuppetMasterCode.Relics;

public class AutoSpooler : PuppetMasterRelic, IAfterRestring
{
    public override RelicRarity Rarity => RelicRarity.Common;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(1),
    ];

    private bool HasRestrungThisTurn
    {
        get => _hasRestrungThisTurn;
        set
        {
            AssertMutable();
            _hasRestrungThisTurn = value;
        }
    }

    private bool _hasRestrungThisTurn;

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        HasRestrungThisTurn = false;
    }

    public async Task AfterRestring(ICombatState? combatState, PlayerChoiceContext ctx, Creature? applier, Creature? target, int amount, CardPlay? cardPlay)
    {
        if (applier != Owner.Creature || HasRestrungThisTurn)
        {
            return;
        }

        Flash();
        HasRestrungThisTurn = true;
        await CardPileCmd.Draw(ctx, DynamicVars.Cards.IntValue, Owner);
    }
}