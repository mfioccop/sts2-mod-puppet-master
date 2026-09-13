using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using PuppetMaster.PuppetMasterCode.Powers;

namespace PuppetMaster.PuppetMasterCode.Relics;

public class SteamPoweredWheel : PuppetMasterRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ThreadPower>(2),
        new PowerVar<VigorPower>(2),
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ThreadPower.HoverTip,
        HoverTipFactory.FromPower<VigorPower>(),
    ];

    private bool HasSpunThisTurn
    {
        get => _hasSpunThisTurn;
        set
        {
            AssertMutable();
            _hasSpunThisTurn = value;
        }
    }

    private bool _hasSpunThisTurn;

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (side != Owner.Creature.Side)
        {
            return;
        }

        HasSpunThisTurn = false;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Player != Owner || HasSpunThisTurn)
        {
            return;
        }

        HasSpunThisTurn = true;
        await PowerCmd.Apply<ThreadPower>(choiceContext, Owner.Creature.CombatState?.HittableEnemies, DynamicVars.Power<ThreadPower>().BaseValue, Owner.Creature, null);
        await PowerCmd.Apply<VigorPower>(choiceContext, Owner.Creature, DynamicVars.Power<VigorPower>().BaseValue, Owner.Creature, null);
    }
}