using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PuppetMaster.PuppetMasterCode.Character;
using PuppetMaster.PuppetMasterCode.Extensions;
using PuppetMaster.PuppetMasterCode.Powers;
using PuppetMaster.PuppetMasterCode.Vars;

namespace PuppetMaster.PuppetMasterCode.Cards;

[Pool(typeof(PuppetMasterCardPool))]
public abstract class PuppetMasterCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    CustomCardModel(cost, type, rarity, target)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..CanonicalVars.Any(v => v is PowerVar<ThreadPower> or RestringVar) ? new[] { HoverTipFactory.FromPower<ThreadPower>() } : []
    ];

    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();

    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();


    protected virtual async Task<int> TryRestring(PlayerChoiceContext choiceContext, Creature? target, bool allowPartial = false)
    {
        return await TryRestring(choiceContext, target, DynamicVars.Restring(), allowPartial);
    }

    protected virtual async Task<int> TryRestring(PlayerChoiceContext choiceContext, Creature? target, RestringVar? restring, bool allowPartial = false)
    {
        if (restring == null)
        {
            return 0;
        }

        return await TryRestring(choiceContext, target, restring.BaseValue, allowPartial);
    }

    protected virtual async Task<int> TryRestring(PlayerChoiceContext choiceContext, Creature? target, decimal amount, bool allowPartial = false)
    {
        return await TryRestring(choiceContext, target, (int)amount, allowPartial);
    }

    protected virtual async Task<int> TryRestring(PlayerChoiceContext choiceContext, Creature? target, int amount, bool allowPartial = false)
    {
        var thread = target?.GetPower<ThreadPower>();
        if (thread == null)
        {
            return 0;
        }

        var startingThread = thread.Amount;

        if (amount <= 0)
        {
            // Consume all thread
            await PowerCmd.Remove(thread);
            return startingThread;
        }

        if (!allowPartial && thread.Amount < amount)
        {
            return 0;
        }

        var leftoverThread = await PowerCmd.ModifyAmount(choiceContext, thread, -amount, Owner.Creature, this);
        return startingThread - leftoverThread;
    }
}