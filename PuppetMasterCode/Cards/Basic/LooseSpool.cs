using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PuppetMaster.PuppetMasterCode.Powers;

namespace PuppetMaster.PuppetMasterCode.Cards.Basic;

public class LooseSpool() : PuppetMasterCard(0, CardType.Skill, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ThreadPower>(1).WithUpgrade(1)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        // HoverTipFactory.FromPower<ThreadPower>(),
        ..base.ExtraHoverTips
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.Apply<ThreadPower>(choiceContext, this, play);
    }
}