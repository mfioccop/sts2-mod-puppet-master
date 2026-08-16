using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PuppetMaster.PuppetMasterCode.Cards;

namespace PuppetMaster.PuppetMasterCode.Powers;

public class MaestroFormPower : PuppetMasterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner))
        {
            return;
        }

        var player = Owner.Player;
        if (player == null)
        {
            return;
        }

        var allPuppets = player.Character.CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint).Where(c => c is PuppetCard);
        var puppet = CardFactory.GetDistinctForCombat(player, allPuppets, 1, player.RunState.Rng.CombatCardGeneration).FirstOrDefault();

        if (puppet == null)
        {
            return;
        }

        Flash();
        CardCmd.Upgrade(puppet);
        puppet.ExhaustOnNextPlay = true;
        await CardCmd.AutoPlay(new ThrowingPlayerChoiceContext(), puppet, null);
    }
}