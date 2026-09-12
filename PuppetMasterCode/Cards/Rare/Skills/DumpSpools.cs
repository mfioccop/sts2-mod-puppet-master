using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PuppetMaster.PuppetMasterCode.Powers;

namespace PuppetMaster.PuppetMasterCode.Cards.Rare.Skills;

public class DumpSpools() : PuppetMasterCard(0, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ThreadPower>(12).WithUpgrade(4),
        new EnergyVar(1),
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust,
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var enemies = CombatState!.HittableEnemies;
        var threadPerEnemy = DynamicVars.Power<ThreadPower>().IntValue / enemies.Count;
        var evenSplit = DynamicVars.Power<ThreadPower>().IntValue % enemies.Count == 0;

        await PowerCmd.Apply<ThreadPower>(choiceContext, enemies, threadPerEnemy, Owner.Creature, this);

        if (!evenSplit)
        {
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        }
    }
}